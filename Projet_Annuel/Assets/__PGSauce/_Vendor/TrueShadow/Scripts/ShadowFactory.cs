using System.Collections.Generic;
using LeTai.Effects;
using UnityEngine;
using UnityEngine.Rendering;

namespace LeTai.TrueShadow
{
public class ShadowFactory
{
    private static ShadowFactory instance;
    public static  ShadowFactory Instance => instance ?? (instance = new ShadowFactory());

    readonly Dictionary<int, ShadowContainer> shadowCache =
        new Dictionary<int, ShadowContainer>();

    readonly CommandBuffer         cmd;
    readonly MaterialPropertyBlock materialProps;
    readonly ScalableBlur          blurProcessor;
    readonly ScalableBlurConfig    blurConfig;

    Material cutoutMaterial;
    Material imprintPostProcessMaterial;

    Material CutoutMaterial =>
        cutoutMaterial ? cutoutMaterial : cutoutMaterial = new Material(Shader.Find("Hidden/TrueShadow/Cutout"));

    Material ImprintPostProcessMaterial =>
        imprintPostProcessMaterial
            ? imprintPostProcessMaterial
            : imprintPostProcessMaterial = new Material(Shader.Find("Hidden/TrueShadow/ImprintPostProcess"));

    private ShadowFactory()
    {
        cmd           = new CommandBuffer {name = "Shadow Commands"};
        materialProps = new MaterialPropertyBlock();
        materialProps.SetVector(ShaderId.CLIP_RECT,
                                new Vector4(float.NegativeInfinity, float.NegativeInfinity,
                                            float.PositiveInfinity, float.PositiveInfinity));
        materialProps.SetInt(ShaderId.COLOR_MASK, (int) ColorWriteMask.All); // Render shadow even if mask hide graphic

        ShaderProperties.Init(8);
        blurConfig           = ScriptableObject.CreateInstance<ScalableBlurConfig>();
        blurConfig.hideFlags = HideFlags.HideAndDontSave;
        blurProcessor        = new ScalableBlur();
        blurProcessor.Configure(blurConfig);
    }

    ~ShadowFactory()
    {
        cmd.Dispose();
        Utility.SafeDestroy(blurConfig);
        Utility.SafeDestroy(cutoutMaterial);
        Utility.SafeDestroy(imprintPostProcessMaterial);
    }

#if LETAI_TRUESHADOW_DEBUG
    RenderTexture debugTexture;
#endif

    // public int createdContainerCount;
    // public int releasedContainerCount;

    public void Get(ShadowRenderingRequest request, ref ShadowContainer container)
    {
        if (float.IsNaN(request.dimensions.x) || request.dimensions.x < 1 ||
            float.IsNaN(request.dimensions.y) || request.dimensions.y < 1)
        {
            ReleaseContainer(container);
            return;
        }

#if LETAI_TRUESHADOW_DEBUG
        RenderTexture.ReleaseTemporary(debugTexture);
        if (request.shadow.alwaysRender)
            debugTexture = RenderShadow(request);
#endif

        // Each request need a coresponding shadow texture
        // Texture may be shared by multiple elements
        // Texture are released when no longer used by any element
        // ShadowContainer keep track of texture and their usage


        int requestHash = request.GetHashCode();

        // Case: requester can keep the same texture
        if (container?.requestHash == requestHash)
            return;

        ReleaseContainer(container);

        if (shadowCache.TryGetValue(requestHash, out var existingContainer))
        {
            // Case: requester got texture from someone else
            existingContainer.RefCount++;
            container = existingContainer;
        }
        else
        {
            // Case: requester got new unique texture
            container = shadowCache[requestHash] = new ShadowContainer(RenderShadow(request), request);
            // Debug.Log($"Created new container for request\t{requestHash}\tTotal Created: {++createdContainerCount}\t Alive: {createdContainerCount - releasedContainerCount}");
        }
    }

    internal void ReleaseContainer(ShadowContainer container)
    {
        if (container == null)
            return;

        if (--container.RefCount > 0)
            return;

        RenderTexture.ReleaseTemporary(container.Texture);
        shadowCache.Remove(container.requestHash);

        // Debug.Log($"Released container for request\t{container.requestHash}\tTotal Released: {++releasedContainerCount}\t Alive: {createdContainerCount - releasedContainerCount}");
    }

    static readonly Rect UNIT_RECT                        = new Rect(0, 0, 1, 1);
    static readonly int  IMPRINT_TEX_ID                   = Shader.PropertyToID("True Shadow Imprint Buffer");
    static readonly int  IMPRINT_AFTER_POSTPROCESS_TEX_ID = Shader.PropertyToID("True Shadow Bleach Buffer");

    RenderTexture RenderShadow(ShadowRenderingRequest request)
    {
        // return GenColoredTexture(request.GetHashCode());

        cmd.Clear();
        cmd.BeginSample("TrueShadow:Capture");

        var bounds = request.shadow.SpriteMesh.bounds;

        var misalignmentMin = ((Vector2) bounds.min).Frac();
        var misalignmentMax = Vector2.one - ((Vector2) bounds.max).Frac();
        if (misalignmentMax.x > 1 - 1e-5)
            misalignmentMax.x = 0;
        if (misalignmentMax.y > 1 - 1e-5)
            misalignmentMax.y = 0;

        var padding      = Mathf.CeilToInt(request.shadowSize);
        var imprintViewW = Mathf.CeilToInt(bounds.size.x + misalignmentMin.x + misalignmentMax.x);
        var imprintViewH = Mathf.CeilToInt(bounds.size.y + misalignmentMin.y + misalignmentMax.y);
        var tw           = imprintViewW + padding * 2;
        var th           = imprintViewH + padding * 2;
        var shadowTex    = RenderTexture.GetTemporary(tw, th, 0, RenderTextureFormat.Default);
        cmd.GetTemporaryRT(IMPRINT_TEX_ID, tw, th, 0, FilterMode.Bilinear, RenderTextureFormat.Default);

        bool needPostProcess = request.shadow.IgnoreCasterColor || request.shadow.Inset;
        if (needPostProcess)
        {
            cmd.GetTemporaryRT(IMPRINT_AFTER_POSTPROCESS_TEX_ID, tw, th, 0, FilterMode.Bilinear,
                               RenderTextureFormat.Default);
        }

        var texture = request.shadow.Content;
        if (texture)
            materialProps.SetTexture(ShaderId.MAIN_TEX, texture);
        else
            materialProps.SetTexture(ShaderId.MAIN_TEX, Texture2D.whiteTexture);

        cmd.SetRenderTarget(IMPRINT_TEX_ID);
        cmd.ClearRenderTarget(true, true, request.shadow.ClearColor);

        cmd.SetViewport(new Rect(padding, padding, imprintViewW, imprintViewH));

        var imprintBoundMin = (Vector2) bounds.min - misalignmentMin;
        var imprintBoundMax = (Vector2) bounds.max + misalignmentMax;
        cmd.SetViewProjectionMatrices(
            Matrix4x4.identity,
            Matrix4x4.Ortho(imprintBoundMin.x, imprintBoundMax.x,
                            imprintBoundMin.y, imprintBoundMax.y,
                            -1, 1)
        );

        request.shadow.ModifyShadowCastingMesh(request.shadow.SpriteMesh);
        request.shadow.ModifyShadowCastingMaterialProperties(materialProps);
        cmd.DrawMesh(request.shadow.SpriteMesh,
                     Matrix4x4.identity,
                     request.shadow.GetShadowCastingMaterial(),
                     0, 0,
                     materialProps);

        if (needPostProcess)
        {
            ImprintPostProcessMaterial.SetKeyword("BLEACH", request.shadow.IgnoreCasterColor);
            ImprintPostProcessMaterial.SetKeyword("INSET",  request.shadow.Inset);

            cmd.Blit(IMPRINT_TEX_ID, IMPRINT_AFTER_POSTPROCESS_TEX_ID, ImprintPostProcessMaterial);
        }

        cmd.EndSample("TrueShadow:Capture");


        cmd.BeginSample("TrueShadow:Cast");
        int source = needPostProcess ? IMPRINT_AFTER_POSTPROCESS_TEX_ID : IMPRINT_TEX_ID;
        if (request.shadowSize < 1e-2)
        {
            cmd.Blit(source, shadowTex);
        }
        else
        {
            blurConfig.Strength = request.shadowSize;
            blurProcessor.Blur(cmd, source, UNIT_RECT, shadowTex);
        }

        cmd.EndSample("TrueShadow:Cast");

        if (request.shadow.Cutout)
        {
            cmd.BeginSample("TrueShadow:Cutout");
            CutoutMaterial.SetVector(ShaderId.OFFSET, new Vector2(request.shadowOffset.x / tw,
                                                                  request.shadowOffset.y / th));
            CutoutMaterial.SetFloat(ShaderId.OVERFLOW_ALPHA, request.shadow.Inset ? 1 : 0);
            cmd.SetViewport(UNIT_RECT);
            cmd.Blit(source, shadowTex, CutoutMaterial);
            cmd.EndSample("TrueShadow:Cutout");
        }

        cmd.ReleaseTemporaryRT(source);

        Graphics.ExecuteCommandBuffer(cmd);

        return shadowTex;
    }

    RenderTexture GenColoredTexture(int hash)
    {
        var tex = new Texture2D(1, 1);
        tex.SetPixels32(new[] {new Color32((byte) (hash >> 8), (byte) (hash >> 16), (byte) (hash >> 24), 255)});
        tex.Apply();

        var rt = RenderTexture.GetTemporary(1, 1);
        Graphics.Blit(tex, rt);

        return rt;
    }
}
}
