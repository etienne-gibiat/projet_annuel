//////////////////////////////////////////////////////
// MK Glow URP Renderer Feature	    		        //
//					                                //
// Created by Michael Kremmel                       //
// www.michaelkremmel.de                            //
// Copyright © 2021 All rights reserved.            //
//////////////////////////////////////////////////////

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace MK.Glow.URP
{
    public class MKGlowRendererFeature : ScriptableRendererFeature
    {
        class MKGlowRenderPass : ScriptableRenderPass, MK.Glow.ICameraData
        {
            private MK.Glow.URP.MKGlow _mKGlowVolumeComponent = null;
            private MK.Glow.URP.MKGlow mKGlowVolumeComponent
            {
                get
                {
                    _mKGlowVolumeComponent = VolumeManager.instance.stack.GetComponent<MK.Glow.URP.MKGlow>();
                    return _mKGlowVolumeComponent;
                }
            }

            internal Effect effect = new Effect();
            internal ScriptableRenderer scriptableRenderer;
            private RenderTarget sourceRenderTarget, destinationRenderTarget;
            private RenderingData _renderingData;
            private MK.Glow.ISettings _settingsURP = null;
            private RenderTextureDescriptor _sourceDescriptor;
            private readonly int _rendererBufferID = Shader.PropertyToID("_MKGlowScriptableRendererOutput");
            private readonly string _profilerName = "MKGlow";

            public MKGlowRenderPass()
            {
                this.renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            }

            public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
            {
                _sourceDescriptor = cameraTextureDescriptor;
            }

            public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
            {
                _settingsURP = mKGlowVolumeComponent;

                if(mKGlowVolumeComponent == null || renderingData.cameraData.camera == null)
                    return;
                
                if(!mKGlowVolumeComponent.IsActive())
                    return;

                CommandBuffer cmd = CommandBufferPool.Get(_profilerName);
                _renderingData = renderingData;

                #if UNITY_2022_1_OR_NEWER
                    sourceRenderTarget.renderTargetIdentifier = scriptableRenderer.cameraColorTargetHandle;
                #else
                    sourceRenderTarget.renderTargetIdentifier = scriptableRenderer.cameraColorTarget;
                #endif
                destinationRenderTarget.identifier = _rendererBufferID;

                #if UNITY_2018_2_OR_NEWER
                destinationRenderTarget.renderTargetIdentifier = new RenderTargetIdentifier(destinationRenderTarget.identifier, 0, CubemapFace.Unknown, -1);
                #else
                destinationRenderTarget.renderTargetIdentifier = new RenderTargetIdentifier(destinationRenderTarget.identifier);
                #endif

                #if UNITY_2020_2_OR_NEWER
                if(renderingData.cameraData.cameraType == CameraType.SceneView || renderingData.cameraData.cameraType == CameraType.Game && renderingData.cameraData.camera.targetTexture || !GetStereoEnabled())
                {
                    cmd.GetTemporaryRT(destinationRenderTarget.identifier, _sourceDescriptor, FilterMode.Bilinear);
                    Blit(cmd, sourceRenderTarget.renderTargetIdentifier, destinationRenderTarget.renderTargetIdentifier);
                    effect.Build(destinationRenderTarget, sourceRenderTarget, _settingsURP, cmd, this, renderingData.cameraData.camera);
                    cmd.ReleaseTemporaryRT(destinationRenderTarget.identifier);
                }
                else
                {
                    effect.Build(sourceRenderTarget, sourceRenderTarget, _settingsURP, cmd, this, renderingData.cameraData.camera);
                }
                #else
                cmd.GetTemporaryRT(destinationRenderTarget.identifier, _sourceDescriptor, FilterMode.Bilinear);
                Blit(cmd, sourceRenderTarget.renderTargetIdentifier, destinationRenderTarget.renderTargetIdentifier);
                effect.Build(destinationRenderTarget, sourceRenderTarget, _settingsURP, cmd, this, renderingData.cameraData.camera);
                cmd.ReleaseTemporaryRT(destinationRenderTarget.identifier);
                #endif

                context.ExecuteCommandBuffer(cmd);

                cmd.Clear();
                CommandBufferPool.Release(cmd);
            }

            /////////////////////////////////////////////////////////////////////////////////////////////
            // Camera Data
            /////////////////////////////////////////////////////////////////////////////////////////////
            public int GetCameraWidth()
            {
                return _renderingData.cameraData.cameraTargetDescriptor.width;
            }
            public int GetCameraHeight()
            {
                return _renderingData.cameraData.cameraTargetDescriptor.height;
            }
            public bool GetStereoEnabled()
            {
                #if UNITY_2020_2_OR_NEWER
                    return _renderingData.cameraData.xrRendering;
                #else
                    return _renderingData.cameraData.isStereoEnabled;
                #endif
            }
            public float GetAspect()
            {
                return _renderingData.cameraData.camera.aspect;
            }
            public Matrix4x4 GetWorldToCameraMatrix()
            {
                return _renderingData.cameraData.camera.worldToCameraMatrix;
            }
            public bool GetOverwriteDescriptor()
            {
                return false;
            }
            public UnityEngine.Rendering.TextureDimension GetOverwriteDimension()
            {
                return UnityEngine.Rendering.TextureDimension.Tex2D;
            }
            public int GetOverwriteVolumeDepth()
            {
                return 1;
            }
        }

        private MKGlowRenderPass _mkGlowRenderPass;
        private readonly string _componentName = "MKGlow";

        public override void Create()
        {
            _mkGlowRenderPass = new MKGlowRenderPass();
            _mkGlowRenderPass.effect.Enable(RenderPipeline.SRP);
        }

        private void OnDisable()
        {
            _mkGlowRenderPass.effect.Disable();
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            name = _componentName;

            if(renderingData.cameraData.postProcessEnabled)
            {
                _mkGlowRenderPass.scriptableRenderer = renderer;
                renderer.EnqueuePass(_mkGlowRenderPass);
            }
        }
    }
}


