// Copyright 2014-2019 Elringus (Artyom Sovetnikov). All Rights Reserved.

using UnityEngine;

namespace BlendModes
{
    [ExtendedComponent(typeof(Camera))]
    public class CameraExtension : ComponentExtension
    {
        public Vector4 ScreenUV { get; set; } = new Vector4(1, 0, 0, 1);

        private static readonly int uvTransform = Shader.PropertyToID("_UV_Transform");
        private static ShaderProperty[] cachedDefaultProperties;
        private Material renderMaterial = null;

        public override void OnEffectEnabled ()
        {
            BlendModeEffect.RenderMode = RenderMode.TextureWithSelf;
        }

        public override string[] GetSupportedShaderFamilies ()
        {
            return new[] {
                "Camera",
                "CameraHsbc"
            };
        }

        public override ShaderProperty[] GetDefaultShaderProperties ()
        {
            return cachedDefaultProperties ?? (cachedDefaultProperties = new[] {
                    new ShaderProperty("_Hue", ShaderPropertyType.Float, 0),
                    new ShaderProperty("_Saturation", ShaderPropertyType.Float, 0),
                    new ShaderProperty("_Brightness", ShaderPropertyType.Float, 0),
                    new ShaderProperty("_Contrast", ShaderPropertyType.Float, 0)
                });
        }

        public override Material GetRenderMaterial ()
        {
            return renderMaterial;
        }

        public override void SetRenderMaterial (Material renderMaterial)
        {
            this.renderMaterial = renderMaterial;
        }

        public override bool AllowMaterialSharing () { return false; }

        public override void OnEffectRenderImage (RenderTexture source, RenderTexture destination)
        {
            #if UNITY_WP8
	        // WP8 has no OS support for rotating screen with device orientation,
	        // so we do those transformations ourselves.
		    if (Screen.orientation == ScreenOrientation.LandscapeLeft)
                ScreenUV = new Vector4(0, -1, 1, 0);
		    if (Screen.orientation == ScreenOrientation.LandscapeRight)
                ScreenUV = new Vector4(0, 1, -1, 0);
		    if (Screen.orientation == ScreenOrientation.PortraitUpsideDown)
                ScreenUV = new Vector4(-1, 0, 0, -1);
            #endif

            if (renderMaterial)
            {
                renderMaterial.SetVector(uvTransform, ScreenUV);
                Graphics.Blit(source, destination, renderMaterial);
            }
            else Graphics.Blit(source, destination);
        }
    }
}
