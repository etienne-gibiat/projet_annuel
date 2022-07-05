Shader "NatureManufacture/URP/Lava River/Lava River Mobile Flowmap"
{
    Properties
    {
        _GlobalTiling("Global Tiling", Range(0.001, 100)) = 1
        [ToggleUI]_UVVDirection1UDirection0("UV Direction - V(T) U(F)", Float) = 1
        _ColdLavaMainSpeed("Cold Lava Main Speed", Vector) = (-0.3, 0.3, 0, 0)
        _MediumLavaMainSpeed("Medium Lava Main Speed", Vector) = (-0.3, 0.3, 0, 0)
        _HotLavaMainSpeed("Hot Lava Main Speed", Vector) = (-0.3, 0.3, 0, 0)
        [NoScaleOffset]_ColdLavaAlbedo_SM("Cold Lava Albedo_SM", 2D) = "white" {}
        _ColdLavaAlbedoColor("Cold Lava Albedo Color", Color) = (1, 1, 1, 0)
        _ColdLavaAlbedoColorMultiply("Cold Lava Albedo Color Multiply ", Float) = 1
        _ColdLavaTiling("Cold Lava Tiling", Vector) = (1, 1, 0, 0)
        _ColdLavaSmoothness("Cold Lava Smoothness", Range(0, 1)) = 1
        [NoScaleOffset]_ColdLavaNormal("Cold Lava Normal", 2D) = "bump" {}
        _ColdLavaNormalScale("Cold Lava Normal Scale", Float) = 1
        [NoScaleOffset]_ColdLavaMT_AO_H_EM("Cold Lava MT_AO_H_EM", 2D) = "black" {}
        _ColdLavaMetalic("Cold Lava Metalic", Range(0, 1)) = 1
        _ColdLavaAO("Cold Lava AO", Range(0, 1)) = 1
        _MediumLavaAngle("Medium Lava Angle", Range(0.001, 90)) = 4
        _MediumLavaAngleFalloff("Medium Lava Angle Falloff", Range(0, 80)) = 0.7
        _MediumLavaHeightBlendTreshold("Medium Lava Height Blend Treshold", Range(0, 10)) = 3.76
        _MediumLavaHeightBlendStrenght("Medium Lava Height Blend Strenght", Range(0, 20)) = 2.75
        _MediumLavaAlbedoColor("Medium Lava Albedo Color", Color) = (1, 1, 1, 0)
        _MediumLavaAlbedoColorMultiply("Medium Lava Albedo Color Multiply ", Float) = 1
        _MediumLavaTiling("Medium Lava Tiling", Vector) = (2, 2, 0, 0)
        _MediumLavaSmoothness("Medium Lava Smoothness", Range(0, 1)) = 1
        _MediumLavaNormalScale("Medium Lava Normal Scale", Float) = 1
        _MediumLavaMetallic("Medium Lava Metallic", Range(0, 1)) = 1
        _MediumLavaAO("Medium Lava AO", Range(0, 1)) = 1
        _HotLavaAngle("Hot Lava Angle", Range(0.001, 90)) = 9.8
        _HotLavaAngleFalloff("Hot Lava Angle Falloff", Range(0, 80)) = 1.5
        _HotLavaHeightBlendTreshold("Hot Lava Height Blend Treshold", Range(0, 10)) = 3.09
        _HotLavaHeightBlendStrenght("Hot Lava Height Blend Strenght", Range(0, 20)) = 2.37
        _HotLavaAlbedoColor("Hot Lava Albedo Color", Color) = (1, 1, 1, 0)
        _HotLavaAlbedoColorMultiply("Hot Lava Albedo Color Multiply ", Float) = 1
        _HotLavaTiling("Hot Lava Tiling", Vector) = (1, 1, 0, 0)
        _HotLavaSmoothness("Hot Lava Smoothness", Range(0, 1)) = 1
        _HotLavaNormalScale("Hot Lava Normal Scale", Float) = 1
        _HotLavaMetallic("Hot Lava Metallic", Range(0, 1)) = 1
        _HotLavaAO("Hot Lava AO", Range(0, 1)) = 1
        _ColdLavaFlowUVRefresSpeed("Cold Lava Flow UV Refresh Speed", Range(0, 1)) = 0.1
        _MediumLavaFlowUVRefreshSpeed("Medium Lava Flow UV Refresh Speed", Range(0, 1)) = 0.1
        _HotLavaFlowUVRefreshSpeed("Hot Lava Flow UV Refresh Speed", Range(0, 1)) = 0.1
        _EmissionColor("Emission Color", Color) = (1, 0.1862055, 0, 0)
        _ColdLavaEmissionMaskIntensivity("Cold Lava Emission Mask Intensivity", Range(0, 100)) = 1.9
        _ColdLavaEmissionMaskTreshold("Cold Lava Emission Mask Treshold", Float) = 2.55
        _MediumLavaEmissionMaskIntesivity("Medium Lava Emission Mask Intesivity", Range(0, 100)) = 3.8
        _MediumLavaEmissionMaskTreshold("Medium Lava Emission Mask Treshold", Float) = 3.15
        _HotLavaEmissionMaskIntensivity("Hot Lava Emission Mask Intensivity", Range(0, 100)) = 2
        _HotLavaEmissionMaskTreshold("Hot Lava Emission Mask Treshold", Float) = 9.52
        [NoScaleOffset]_Noise("Noise", 2D) = "white" {}
        _NoiseTiling("Noise Tiling", Vector) = (1, 1, 0, 0)
        _NoiseSpeed("Noise Speed", Vector) = (0.5, 0.5, 0, 0)
        _ColdLavaNoisePower("Cold Lava Noise Power", Range(0, 10)) = 6.45
        _MediumLavaNoisePower("Medium Lava Noise Power", Range(0, 10)) = 2.47
        _HotLavaNoisePower("Hot Lava Noise Power", Range(0, 10)) = 5.48
        [HideInInspector][NoScaleOffset]unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset]unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "UniversalMaterialType" = "Lit"
            "Queue"="Geometry"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
        #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ SHADOWS_SHADOWMASK
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_FORWARD
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv3 : TEXCOORD3;
            float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 positionWS;
            float3 normalWS;
            float4 tangentWS;
            float4 texCoord0;
            float4 texCoord3;
            float4 color;
            float3 viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            float2 lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            float3 sh;
            #endif
            float4 fogFactorAndVertexLight;
            float4 shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float3 TangentSpaceNormal;
            float4 uv0;
            float4 uv3;
            float4 VertexColor;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float3 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            float4 interp4 : TEXCOORD4;
            float4 interp5 : TEXCOORD5;
            float3 interp6 : TEXCOORD6;
            #if defined(LIGHTMAP_ON)
            float2 interp7 : TEXCOORD7;
            #endif
            #if !defined(LIGHTMAP_ON)
            float3 interp8 : TEXCOORD8;
            #endif
            float4 interp9 : TEXCOORD9;
            float4 interp10 : TEXCOORD10;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord3;
            output.interp5.xyzw =  input.color;
            output.interp6.xyz =  input.viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            output.interp7.xy =  input.lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.interp8.xyz =  input.sh;
            #endif
            output.interp9.xyzw =  input.fogFactorAndVertexLight;
            output.interp10.xyzw =  input.shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord3 = input.interp4.xyzw;
            output.color = input.interp5.xyzw;
            output.viewDirectionWS = input.interp6.xyz;
            #if defined(LIGHTMAP_ON)
            output.lightmapUV = input.interp7.xy;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.interp8.xyz;
            #endif
            output.fogFactorAndVertexLight = input.interp9.xyzw;
            output.shadowCoord = input.interp10.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

        void Unity_NormalStrength_half(half3 In, half Strength, out half3 Out)
        {
            Out = half3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half(half A, half B, half T, out half Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Clamp_half4(half4 In, half4 Min, half4 Max, out half4 Out)
        {
            Out = clamp(In, Min, Max);
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
            half3 NormalTS;
            half3 Emission;
            half Metallic;
            half Smoothness;
            half Occlusion;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            UnityTexture2D _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0);
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_R_4 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.r;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_G_5 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.g;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_B_6 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.b;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_A_7 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.a;
            half _Property_30c36922aabc618192374556ee8ce299_Out_0 = _ColdLavaNormalScale;
            half3 _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2);
            half4 _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0);
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_R_4 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.r;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_G_5 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.g;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_B_6 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.b;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_A_7 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.a;
            half3 _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2);
            half3 _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3;
            Unity_Lerp_half3(_NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxx), _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3);
            UnityTexture2D _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0);
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_R_4 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.r;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_G_5 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.g;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_B_6 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.b;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_A_7 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.a;
            half _Property_c488bf556481e28d8a97898896b5cdec_Out_0 = _MediumLavaNormalScale;
            half3 _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2);
            half4 _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0);
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_R_4 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.r;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_G_5 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.g;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_B_6 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.b;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_A_7 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.a;
            half3 _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2);
            half3 _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3;
            Unity_Lerp_half3(_NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxx), _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3);
            half3 _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3;
            Unity_Lerp_half3(_Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3, _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3);
            UnityTexture2D _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0);
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_R_4 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.r;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_G_5 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.g;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_B_6 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.b;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_A_7 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.a;
            half _Property_48f492f6311fa887a8666bf46f288d9d_Out_0 = _HotLavaNormalScale;
            half3 _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2);
            half4 _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0);
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_R_4 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.r;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_G_5 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.g;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_B_6 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.b;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_A_7 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.a;
            half3 _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2);
            half3 _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3;
            Unity_Lerp_half3(_NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxx), _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3);
            half3 _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            Unity_Lerp_half3(_Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3, _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_85790e354b8b8189bf7442246db27aca_Out_3);
            half _Split_b898e81d55670f89944c32a8494f1543_R_1 = IN.VertexColor[0];
            half _Split_b898e81d55670f89944c32a8494f1543_G_2 = IN.VertexColor[1];
            half _Split_b898e81d55670f89944c32a8494f1543_B_3 = IN.VertexColor[2];
            half _Split_b898e81d55670f89944c32a8494f1543_A_4 = IN.VertexColor[3];
            half _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3;
            Unity_Clamp_half(_Split_b898e81d55670f89944c32a8494f1543_A_4, 0, 1, _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3);
            half _Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0 = _ColdLavaMetalic;
            half _Multiply_086367572588138ebc1240441f2f400d_Out_2;
            Unity_Multiply_half(_Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0, _Split_94cd24e718391e889d186d14735a5f81_R_1, _Multiply_086367572588138ebc1240441f2f400d_Out_2);
            half _Property_13629a8ac7e261869193e0848c215a76_Out_0 = _ColdLavaAO;
            half _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2;
            Unity_Subtract_half(1, _Property_13629a8ac7e261869193e0848c215a76_Out_0, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2);
            half _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3;
            Unity_Clamp_half(_Split_94cd24e718391e889d186d14735a5f81_G_2, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2, 1, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3);
            half _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3;
            Unity_Lerp_half(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3);
            half _Property_d965da3019019f838869a1fb407d698d_Out_0 = _ColdLavaEmissionMaskIntensivity;
            half _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2;
            Unity_Multiply_half(_Lerp_60114071ddee158f9af9b873a6d840d1_Out_3, _Property_d965da3019019f838869a1fb407d698d_Out_0, _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2);
            half _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1;
            Unity_Absolute_half(_Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2, _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1);
            half _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0 = _ColdLavaEmissionMaskTreshold;
            half _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2;
            Unity_Power_half(_Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1, _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2);
            half4 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4;
            half3 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5;
            half2 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6;
            Unity_Combine_half(_Multiply_086367572588138ebc1240441f2f400d_Out_2, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2, 0, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6);
            half _Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0 = _MediumLavaMetallic;
            half _Multiply_75ae743d3359148487052919f04e48b2_Out_2;
            Unity_Multiply_half(_Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0, _Split_503d896cedc1148aa1567e911ed3614b_R_1, _Multiply_75ae743d3359148487052919f04e48b2_Out_2);
            half _Property_71564d60d870518cbf142ff71794419d_Out_0 = _MediumLavaAO;
            half _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2;
            Unity_Subtract_half(1, _Property_71564d60d870518cbf142ff71794419d_Out_0, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2);
            half _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3;
            Unity_Clamp_half(_Split_503d896cedc1148aa1567e911ed3614b_G_2, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2, 1, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3);
            half _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3;
            Unity_Clamp_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, 0, 1, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3);
            half _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3;
            Unity_Lerp_half(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3, _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3);
            half _Property_459a51ffc4728c8ca2926024707897c6_Out_0 = _MediumLavaEmissionMaskIntesivity;
            half _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2;
            Unity_Multiply_half(_Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3, _Property_459a51ffc4728c8ca2926024707897c6_Out_0, _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2);
            half _Absolute_6e05a38014175a829a84304e3f621745_Out_1;
            Unity_Absolute_half(_Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2, _Absolute_6e05a38014175a829a84304e3f621745_Out_1);
            half _Property_493c2ccb3a27c580ab437efe58937c35_Out_0 = _MediumLavaEmissionMaskTreshold;
            half _Power_899ae999721ad384b72c681599af42de_Out_2;
            Unity_Power_half(_Absolute_6e05a38014175a829a84304e3f621745_Out_1, _Property_493c2ccb3a27c580ab437efe58937c35_Out_0, _Power_899ae999721ad384b72c681599af42de_Out_2);
            half4 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4;
            half3 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5;
            half2 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6;
            Unity_Combine_half(_Multiply_75ae743d3359148487052919f04e48b2_Out_2, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3, _Power_899ae999721ad384b72c681599af42de_Out_2, 0, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6);
            half3 _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3;
            Unity_Lerp_half3(_Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3);
            half _Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0 = _HotLavaMetallic;
            half4 _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_R_4 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.r;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_G_5 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.g;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_B_6 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.b;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.a;
            half4 _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_R_4 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.r;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_G_5 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.g;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_B_6 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.b;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.a;
            half4 _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0, _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxxx), _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3);
            half _Split_94d707688f4dff88abea8f5931660ff1_R_1 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[0];
            half _Split_94d707688f4dff88abea8f5931660ff1_G_2 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[1];
            half _Split_94d707688f4dff88abea8f5931660ff1_B_3 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[2];
            half _Split_94d707688f4dff88abea8f5931660ff1_A_4 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[3];
            half _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2;
            Unity_Multiply_half(_Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0, _Split_94d707688f4dff88abea8f5931660ff1_R_1, _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2);
            half _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0 = _HotLavaAO;
            half _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2;
            Unity_Subtract_half(1, _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2);
            half _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3;
            Unity_Clamp_half(_Split_94d707688f4dff88abea8f5931660ff1_G_2, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2, 1, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3);
            half _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3;
            Unity_Lerp_half(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7, _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3);
            half _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0 = _HotLavaEmissionMaskIntensivity;
            half _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2;
            Unity_Multiply_half(_Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3, _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0, _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2);
            half _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1;
            Unity_Absolute_half(_Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2, _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1);
            half _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0 = _HotLavaEmissionMaskTreshold;
            half _Power_0e477cda8e66268c882e8889cb195d72_Out_2;
            Unity_Power_half(_Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1, _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0, _Power_0e477cda8e66268c882e8889cb195d72_Out_2);
            half4 _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4;
            half3 _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5;
            half2 _Combine_a55e0256baa5a682b416f12d9adff678_RG_6;
            Unity_Combine_half(_Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3, _Power_0e477cda8e66268c882e8889cb195d72_Out_2, 0, _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, _Combine_a55e0256baa5a682b416f12d9adff678_RG_6);
            half3 _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3;
            Unity_Lerp_half3(_Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3);
            half _Split_be0161af2d147e82901bbdfc190c174f_R_1 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[0];
            half _Split_be0161af2d147e82901bbdfc190c174f_G_2 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[1];
            half _Split_be0161af2d147e82901bbdfc190c174f_B_3 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[2];
            half _Split_be0161af2d147e82901bbdfc190c174f_A_4 = 0;
            half4 _Property_b3443969e143738086170ebbcf185caa_Out_0 = _EmissionColor;
            half4 _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2;
            Unity_Multiply_half((_Split_be0161af2d147e82901bbdfc190c174f_B_3.xxxx), _Property_b3443969e143738086170ebbcf185caa_Out_0, _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2);
            UnityTexture2D _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            half _Property_4273893c41e28e8f83e15df255cfe5c4_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1fbdca361af74f8791981d29932b1108_Out_0 = _NoiseSpeed;
            half2 _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0 = _NoiseTiling;
            half2 _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2;
            Unity_Multiply_half(_Property_1fbdca361af74f8791981d29932b1108_Out_0, _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2);
            half4 _UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0 = IN.uv3;
            half2 _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2;
            Unity_Multiply_half(_Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2, (_UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0.xy), _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2);
            half _Split_3b56d52b260c438bbdce3d9a7263a123_R_1 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[0];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_G_2 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[1];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_B_3 = 0;
            half _Split_3b56d52b260c438bbdce3d9a7263a123_A_4 = 0;
            half2 _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0 = half2(_Split_3b56d52b260c438bbdce3d9a7263a123_G_2, _Split_3b56d52b260c438bbdce3d9a7263a123_R_1);
            half2 _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3;
            Unity_Branch_half2(_Property_4273893c41e28e8f83e15df255cfe5c4_Out_0, _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2, _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0, _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3);
            half _Fraction_461c199aeefb17858283be24648f92b2_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_461c199aeefb17858283be24648f92b2_Out_1);
            half2 _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_461c199aeefb17858283be24648f92b2_Out_1.xx), _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2);
            half _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0 = _GlobalTiling;
            half _Divide_e5638bd7513498828942af638e25e433_Out_2;
            Unity_Divide_half(1, _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0, _Divide_e5638bd7513498828942af638e25e433_Out_2);
            half4 _UV_a5163f4adfe6828bab8fe0f10836a494_Out_0 = IN.uv0;
            half2 _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2;
            Unity_Multiply_half(_Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, (_UV_a5163f4adfe6828bab8fe0f10836a494_Out_0.xy), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2);
            half2 _Multiply_72744c49357fca8c93e11635730250a0_Out_2;
            Unity_Multiply_half((_Divide_e5638bd7513498828942af638e25e433_Out_2.xx), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2);
            half2 _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2;
            Unity_Add_half2(_Multiply_4c650d4119378583a5ecc8db4c483008_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half4 _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_R_4 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.r;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_G_5 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.g;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_B_6 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.b;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.a;
            half _Fraction_b466830e1befc687924722e8a7039fd6_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_b466830e1befc687924722e8a7039fd6_Out_1);
            half2 _Multiply_7b0ff2a5179865869152be767c03f255_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_b466830e1befc687924722e8a7039fd6_Out_1.xx), _Multiply_7b0ff2a5179865869152be767c03f255_Out_2);
            half2 _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2;
            Unity_Add_half2(_Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Multiply_7b0ff2a5179865869152be767c03f255_Out_2, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half4 _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_R_4 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.r;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_G_5 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.g;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_B_6 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.b;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.a;
            half _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3;
            Unity_Lerp_half(_SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7, _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3);
            half _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1;
            Unity_Absolute_half(_Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3, _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1);
            half _Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0 = _ColdLavaNoisePower;
            half _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0 = _MediumLavaNoisePower;
            half _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3;
            Unity_Lerp_half(_Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0, _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3);
            half _Property_8a756da4fda1058f80ca49df1937f450_Out_0 = _HotLavaNoisePower;
            half _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3;
            Unity_Lerp_half(_Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3, _Property_8a756da4fda1058f80ca49df1937f450_Out_0, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3);
            half _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2;
            Unity_Power_half(_Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3, _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2);
            half _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2;
            Unity_Multiply_half(_Power_71ad5b7cab8b348ead45c623e2311de3_Out_2, 20, _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2);
            half _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3;
            Unity_Clamp_half(_Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2, 0.05, 1.2, _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3);
            half4 _Multiply_329a32a74e6e858696ca0b345435de30_Out_2;
            Unity_Multiply_half(_Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2, (_Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3.xxxx), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2);
            half4 _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3;
            Unity_Clamp_half4(_Multiply_329a32a74e6e858696ca0b345435de30_Out_2, half4(0, 0, 0, 0), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2, _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3);
            half4 _Multiply_6d53084a8041428287e1516d476b8861_Out_2;
            Unity_Multiply_half((_Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3.xxxx), _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3, _Multiply_6d53084a8041428287e1516d476b8861_Out_2);
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_R_1 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[0];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_G_2 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[1];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_B_3 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[2];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_A_4 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[3];
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            surface.NormalTS = _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            surface.Emission = (_Multiply_6d53084a8041428287e1516d476b8861_Out_2.xyz);
            surface.Metallic = _Split_be0161af2d147e82901bbdfc190c174f_R_1;
            surface.Smoothness = _Split_ab2d68297c00de8bb6f4fe8fc688cd05_A_4;
            surface.Occlusion = _Split_be0161af2d147e82901bbdfc190c174f_G_2;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.VertexColor =                 input.color;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRForwardPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "GBuffer"
            Tags
            {
                "LightMode" = "UniversalGBuffer"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ _MIXED_LIGHTING_SUBTRACTIVE
        #pragma multi_compile _ _GBUFFER_NORMALS_OCT
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_GBUFFER
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv3 : TEXCOORD3;
            float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 positionWS;
            float3 normalWS;
            float4 tangentWS;
            float4 texCoord0;
            float4 texCoord3;
            float4 color;
            float3 viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            float2 lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            float3 sh;
            #endif
            float4 fogFactorAndVertexLight;
            float4 shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float3 TangentSpaceNormal;
            float4 uv0;
            float4 uv3;
            float4 VertexColor;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float3 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            float4 interp4 : TEXCOORD4;
            float4 interp5 : TEXCOORD5;
            float3 interp6 : TEXCOORD6;
            #if defined(LIGHTMAP_ON)
            float2 interp7 : TEXCOORD7;
            #endif
            #if !defined(LIGHTMAP_ON)
            float3 interp8 : TEXCOORD8;
            #endif
            float4 interp9 : TEXCOORD9;
            float4 interp10 : TEXCOORD10;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord3;
            output.interp5.xyzw =  input.color;
            output.interp6.xyz =  input.viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            output.interp7.xy =  input.lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.interp8.xyz =  input.sh;
            #endif
            output.interp9.xyzw =  input.fogFactorAndVertexLight;
            output.interp10.xyzw =  input.shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord3 = input.interp4.xyzw;
            output.color = input.interp5.xyzw;
            output.viewDirectionWS = input.interp6.xyz;
            #if defined(LIGHTMAP_ON)
            output.lightmapUV = input.interp7.xy;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.interp8.xyz;
            #endif
            output.fogFactorAndVertexLight = input.interp9.xyzw;
            output.shadowCoord = input.interp10.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

        void Unity_NormalStrength_half(half3 In, half Strength, out half3 Out)
        {
            Out = half3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half(half A, half B, half T, out half Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Clamp_half4(half4 In, half4 Min, half4 Max, out half4 Out)
        {
            Out = clamp(In, Min, Max);
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
            half3 NormalTS;
            half3 Emission;
            half Metallic;
            half Smoothness;
            half Occlusion;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            UnityTexture2D _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0);
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_R_4 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.r;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_G_5 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.g;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_B_6 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.b;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_A_7 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.a;
            half _Property_30c36922aabc618192374556ee8ce299_Out_0 = _ColdLavaNormalScale;
            half3 _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2);
            half4 _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0);
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_R_4 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.r;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_G_5 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.g;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_B_6 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.b;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_A_7 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.a;
            half3 _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2);
            half3 _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3;
            Unity_Lerp_half3(_NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxx), _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3);
            UnityTexture2D _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0);
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_R_4 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.r;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_G_5 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.g;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_B_6 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.b;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_A_7 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.a;
            half _Property_c488bf556481e28d8a97898896b5cdec_Out_0 = _MediumLavaNormalScale;
            half3 _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2);
            half4 _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0);
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_R_4 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.r;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_G_5 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.g;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_B_6 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.b;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_A_7 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.a;
            half3 _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2);
            half3 _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3;
            Unity_Lerp_half3(_NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxx), _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3);
            half3 _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3;
            Unity_Lerp_half3(_Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3, _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3);
            UnityTexture2D _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0);
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_R_4 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.r;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_G_5 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.g;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_B_6 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.b;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_A_7 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.a;
            half _Property_48f492f6311fa887a8666bf46f288d9d_Out_0 = _HotLavaNormalScale;
            half3 _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2);
            half4 _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0);
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_R_4 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.r;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_G_5 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.g;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_B_6 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.b;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_A_7 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.a;
            half3 _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2);
            half3 _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3;
            Unity_Lerp_half3(_NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxx), _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3);
            half3 _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            Unity_Lerp_half3(_Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3, _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_85790e354b8b8189bf7442246db27aca_Out_3);
            half _Split_b898e81d55670f89944c32a8494f1543_R_1 = IN.VertexColor[0];
            half _Split_b898e81d55670f89944c32a8494f1543_G_2 = IN.VertexColor[1];
            half _Split_b898e81d55670f89944c32a8494f1543_B_3 = IN.VertexColor[2];
            half _Split_b898e81d55670f89944c32a8494f1543_A_4 = IN.VertexColor[3];
            half _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3;
            Unity_Clamp_half(_Split_b898e81d55670f89944c32a8494f1543_A_4, 0, 1, _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3);
            half _Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0 = _ColdLavaMetalic;
            half _Multiply_086367572588138ebc1240441f2f400d_Out_2;
            Unity_Multiply_half(_Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0, _Split_94cd24e718391e889d186d14735a5f81_R_1, _Multiply_086367572588138ebc1240441f2f400d_Out_2);
            half _Property_13629a8ac7e261869193e0848c215a76_Out_0 = _ColdLavaAO;
            half _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2;
            Unity_Subtract_half(1, _Property_13629a8ac7e261869193e0848c215a76_Out_0, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2);
            half _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3;
            Unity_Clamp_half(_Split_94cd24e718391e889d186d14735a5f81_G_2, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2, 1, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3);
            half _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3;
            Unity_Lerp_half(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3);
            half _Property_d965da3019019f838869a1fb407d698d_Out_0 = _ColdLavaEmissionMaskIntensivity;
            half _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2;
            Unity_Multiply_half(_Lerp_60114071ddee158f9af9b873a6d840d1_Out_3, _Property_d965da3019019f838869a1fb407d698d_Out_0, _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2);
            half _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1;
            Unity_Absolute_half(_Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2, _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1);
            half _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0 = _ColdLavaEmissionMaskTreshold;
            half _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2;
            Unity_Power_half(_Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1, _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2);
            half4 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4;
            half3 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5;
            half2 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6;
            Unity_Combine_half(_Multiply_086367572588138ebc1240441f2f400d_Out_2, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2, 0, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6);
            half _Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0 = _MediumLavaMetallic;
            half _Multiply_75ae743d3359148487052919f04e48b2_Out_2;
            Unity_Multiply_half(_Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0, _Split_503d896cedc1148aa1567e911ed3614b_R_1, _Multiply_75ae743d3359148487052919f04e48b2_Out_2);
            half _Property_71564d60d870518cbf142ff71794419d_Out_0 = _MediumLavaAO;
            half _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2;
            Unity_Subtract_half(1, _Property_71564d60d870518cbf142ff71794419d_Out_0, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2);
            half _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3;
            Unity_Clamp_half(_Split_503d896cedc1148aa1567e911ed3614b_G_2, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2, 1, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3);
            half _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3;
            Unity_Clamp_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, 0, 1, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3);
            half _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3;
            Unity_Lerp_half(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3, _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3);
            half _Property_459a51ffc4728c8ca2926024707897c6_Out_0 = _MediumLavaEmissionMaskIntesivity;
            half _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2;
            Unity_Multiply_half(_Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3, _Property_459a51ffc4728c8ca2926024707897c6_Out_0, _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2);
            half _Absolute_6e05a38014175a829a84304e3f621745_Out_1;
            Unity_Absolute_half(_Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2, _Absolute_6e05a38014175a829a84304e3f621745_Out_1);
            half _Property_493c2ccb3a27c580ab437efe58937c35_Out_0 = _MediumLavaEmissionMaskTreshold;
            half _Power_899ae999721ad384b72c681599af42de_Out_2;
            Unity_Power_half(_Absolute_6e05a38014175a829a84304e3f621745_Out_1, _Property_493c2ccb3a27c580ab437efe58937c35_Out_0, _Power_899ae999721ad384b72c681599af42de_Out_2);
            half4 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4;
            half3 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5;
            half2 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6;
            Unity_Combine_half(_Multiply_75ae743d3359148487052919f04e48b2_Out_2, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3, _Power_899ae999721ad384b72c681599af42de_Out_2, 0, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6);
            half3 _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3;
            Unity_Lerp_half3(_Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3);
            half _Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0 = _HotLavaMetallic;
            half4 _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_R_4 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.r;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_G_5 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.g;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_B_6 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.b;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.a;
            half4 _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_R_4 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.r;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_G_5 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.g;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_B_6 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.b;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.a;
            half4 _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0, _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxxx), _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3);
            half _Split_94d707688f4dff88abea8f5931660ff1_R_1 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[0];
            half _Split_94d707688f4dff88abea8f5931660ff1_G_2 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[1];
            half _Split_94d707688f4dff88abea8f5931660ff1_B_3 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[2];
            half _Split_94d707688f4dff88abea8f5931660ff1_A_4 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[3];
            half _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2;
            Unity_Multiply_half(_Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0, _Split_94d707688f4dff88abea8f5931660ff1_R_1, _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2);
            half _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0 = _HotLavaAO;
            half _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2;
            Unity_Subtract_half(1, _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2);
            half _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3;
            Unity_Clamp_half(_Split_94d707688f4dff88abea8f5931660ff1_G_2, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2, 1, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3);
            half _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3;
            Unity_Lerp_half(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7, _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3);
            half _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0 = _HotLavaEmissionMaskIntensivity;
            half _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2;
            Unity_Multiply_half(_Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3, _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0, _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2);
            half _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1;
            Unity_Absolute_half(_Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2, _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1);
            half _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0 = _HotLavaEmissionMaskTreshold;
            half _Power_0e477cda8e66268c882e8889cb195d72_Out_2;
            Unity_Power_half(_Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1, _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0, _Power_0e477cda8e66268c882e8889cb195d72_Out_2);
            half4 _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4;
            half3 _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5;
            half2 _Combine_a55e0256baa5a682b416f12d9adff678_RG_6;
            Unity_Combine_half(_Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3, _Power_0e477cda8e66268c882e8889cb195d72_Out_2, 0, _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, _Combine_a55e0256baa5a682b416f12d9adff678_RG_6);
            half3 _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3;
            Unity_Lerp_half3(_Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3);
            half _Split_be0161af2d147e82901bbdfc190c174f_R_1 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[0];
            half _Split_be0161af2d147e82901bbdfc190c174f_G_2 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[1];
            half _Split_be0161af2d147e82901bbdfc190c174f_B_3 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[2];
            half _Split_be0161af2d147e82901bbdfc190c174f_A_4 = 0;
            half4 _Property_b3443969e143738086170ebbcf185caa_Out_0 = _EmissionColor;
            half4 _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2;
            Unity_Multiply_half((_Split_be0161af2d147e82901bbdfc190c174f_B_3.xxxx), _Property_b3443969e143738086170ebbcf185caa_Out_0, _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2);
            UnityTexture2D _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            half _Property_4273893c41e28e8f83e15df255cfe5c4_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1fbdca361af74f8791981d29932b1108_Out_0 = _NoiseSpeed;
            half2 _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0 = _NoiseTiling;
            half2 _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2;
            Unity_Multiply_half(_Property_1fbdca361af74f8791981d29932b1108_Out_0, _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2);
            half4 _UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0 = IN.uv3;
            half2 _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2;
            Unity_Multiply_half(_Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2, (_UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0.xy), _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2);
            half _Split_3b56d52b260c438bbdce3d9a7263a123_R_1 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[0];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_G_2 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[1];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_B_3 = 0;
            half _Split_3b56d52b260c438bbdce3d9a7263a123_A_4 = 0;
            half2 _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0 = half2(_Split_3b56d52b260c438bbdce3d9a7263a123_G_2, _Split_3b56d52b260c438bbdce3d9a7263a123_R_1);
            half2 _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3;
            Unity_Branch_half2(_Property_4273893c41e28e8f83e15df255cfe5c4_Out_0, _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2, _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0, _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3);
            half _Fraction_461c199aeefb17858283be24648f92b2_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_461c199aeefb17858283be24648f92b2_Out_1);
            half2 _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_461c199aeefb17858283be24648f92b2_Out_1.xx), _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2);
            half _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0 = _GlobalTiling;
            half _Divide_e5638bd7513498828942af638e25e433_Out_2;
            Unity_Divide_half(1, _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0, _Divide_e5638bd7513498828942af638e25e433_Out_2);
            half4 _UV_a5163f4adfe6828bab8fe0f10836a494_Out_0 = IN.uv0;
            half2 _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2;
            Unity_Multiply_half(_Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, (_UV_a5163f4adfe6828bab8fe0f10836a494_Out_0.xy), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2);
            half2 _Multiply_72744c49357fca8c93e11635730250a0_Out_2;
            Unity_Multiply_half((_Divide_e5638bd7513498828942af638e25e433_Out_2.xx), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2);
            half2 _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2;
            Unity_Add_half2(_Multiply_4c650d4119378583a5ecc8db4c483008_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half4 _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_R_4 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.r;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_G_5 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.g;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_B_6 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.b;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.a;
            half _Fraction_b466830e1befc687924722e8a7039fd6_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_b466830e1befc687924722e8a7039fd6_Out_1);
            half2 _Multiply_7b0ff2a5179865869152be767c03f255_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_b466830e1befc687924722e8a7039fd6_Out_1.xx), _Multiply_7b0ff2a5179865869152be767c03f255_Out_2);
            half2 _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2;
            Unity_Add_half2(_Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Multiply_7b0ff2a5179865869152be767c03f255_Out_2, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half4 _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_R_4 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.r;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_G_5 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.g;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_B_6 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.b;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.a;
            half _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3;
            Unity_Lerp_half(_SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7, _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3);
            half _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1;
            Unity_Absolute_half(_Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3, _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1);
            half _Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0 = _ColdLavaNoisePower;
            half _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0 = _MediumLavaNoisePower;
            half _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3;
            Unity_Lerp_half(_Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0, _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3);
            half _Property_8a756da4fda1058f80ca49df1937f450_Out_0 = _HotLavaNoisePower;
            half _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3;
            Unity_Lerp_half(_Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3, _Property_8a756da4fda1058f80ca49df1937f450_Out_0, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3);
            half _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2;
            Unity_Power_half(_Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3, _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2);
            half _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2;
            Unity_Multiply_half(_Power_71ad5b7cab8b348ead45c623e2311de3_Out_2, 20, _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2);
            half _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3;
            Unity_Clamp_half(_Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2, 0.05, 1.2, _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3);
            half4 _Multiply_329a32a74e6e858696ca0b345435de30_Out_2;
            Unity_Multiply_half(_Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2, (_Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3.xxxx), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2);
            half4 _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3;
            Unity_Clamp_half4(_Multiply_329a32a74e6e858696ca0b345435de30_Out_2, half4(0, 0, 0, 0), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2, _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3);
            half4 _Multiply_6d53084a8041428287e1516d476b8861_Out_2;
            Unity_Multiply_half((_Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3.xxxx), _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3, _Multiply_6d53084a8041428287e1516d476b8861_Out_2);
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_R_1 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[0];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_G_2 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[1];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_B_3 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[2];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_A_4 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[3];
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            surface.NormalTS = _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            surface.Emission = (_Multiply_6d53084a8041428287e1516d476b8861_Out_2.xyz);
            surface.Metallic = _Split_be0161af2d147e82901bbdfc190c174f_R_1;
            surface.Smoothness = _Split_ab2d68297c00de8bb6f4fe8fc688cd05_A_4;
            surface.Occlusion = _Split_be0161af2d147e82901bbdfc190c174f_G_2;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.VertexColor =                 input.color;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRGBufferPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On
        ColorMask 0

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma multi_compile _ _CASTING_PUNCTUAL_LIGHT_SHADOW
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SHADOWCASTER
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            // GraphFunctions: <None>

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On
        ColorMask 0

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            // GraphFunctions: <None>

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormals"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile _ DOTS_INSTANCING_ON
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 normalWS;
            float4 tangentWS;
            float4 texCoord0;
            float4 texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float3 TangentSpaceNormal;
            float4 uv0;
            float4 uv3;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.tangentWS;
            output.interp2.xyzw =  input.texCoord0;
            output.interp3.xyzw =  input.texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.tangentWS = input.interp1.xyzw;
            output.texCoord0 = input.interp2.xyzw;
            output.texCoord3 = input.interp3.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_NormalStrength_half(half3 In, half Strength, out half3 Out)
        {
            Out = half3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 NormalTS;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0);
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_R_4 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.r;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_G_5 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.g;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_B_6 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.b;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_A_7 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.a;
            half _Property_30c36922aabc618192374556ee8ce299_Out_0 = _ColdLavaNormalScale;
            half3 _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2);
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0);
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_R_4 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.r;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_G_5 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.g;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_B_6 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.b;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_A_7 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.a;
            half3 _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2);
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half3 _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3;
            Unity_Lerp_half3(_NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxx), _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3);
            UnityTexture2D _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0);
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_R_4 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.r;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_G_5 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.g;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_B_6 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.b;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_A_7 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.a;
            half _Property_c488bf556481e28d8a97898896b5cdec_Out_0 = _MediumLavaNormalScale;
            half3 _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2);
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0);
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_R_4 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.r;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_G_5 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.g;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_B_6 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.b;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_A_7 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.a;
            half3 _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2);
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half3 _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3;
            Unity_Lerp_half3(_NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxx), _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half3 _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3;
            Unity_Lerp_half3(_Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3, _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3);
            UnityTexture2D _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0);
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_R_4 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.r;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_G_5 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.g;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_B_6 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.b;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_A_7 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.a;
            half _Property_48f492f6311fa887a8666bf46f288d9d_Out_0 = _HotLavaNormalScale;
            half3 _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2);
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0);
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_R_4 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.r;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_G_5 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.g;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_B_6 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.b;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_A_7 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.a;
            half3 _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2);
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half3 _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3;
            Unity_Lerp_half3(_NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxx), _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half3 _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            Unity_Lerp_half3(_Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3, _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_85790e354b8b8189bf7442246db27aca_Out_3);
            surface.NormalTS = _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "Meta"
            Tags
            {
                "LightMode" = "Meta"
            }

            // Render State
            Cull Off

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_META
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv2 : TEXCOORD2;
            float4 uv3 : TEXCOORD3;
            float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 normalWS;
            float4 texCoord0;
            float4 texCoord3;
            float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float4 uv0;
            float4 uv3;
            float4 VertexColor;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            output.interp2.xyzw =  input.texCoord3;
            output.interp3.xyzw =  input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            output.texCoord3 = input.interp2.xyzw;
            output.color = input.interp3.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

        void Unity_Lerp_half(half A, half B, half T, out half Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Clamp_half4(half4 In, half4 Min, half4 Max, out half4 Out)
        {
            Out = clamp(In, Min, Max);
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
            half3 Emission;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            half _Split_b898e81d55670f89944c32a8494f1543_R_1 = IN.VertexColor[0];
            half _Split_b898e81d55670f89944c32a8494f1543_G_2 = IN.VertexColor[1];
            half _Split_b898e81d55670f89944c32a8494f1543_B_3 = IN.VertexColor[2];
            half _Split_b898e81d55670f89944c32a8494f1543_A_4 = IN.VertexColor[3];
            half _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3;
            Unity_Clamp_half(_Split_b898e81d55670f89944c32a8494f1543_A_4, 0, 1, _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3);
            half _Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0 = _ColdLavaMetalic;
            half _Multiply_086367572588138ebc1240441f2f400d_Out_2;
            Unity_Multiply_half(_Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0, _Split_94cd24e718391e889d186d14735a5f81_R_1, _Multiply_086367572588138ebc1240441f2f400d_Out_2);
            half _Property_13629a8ac7e261869193e0848c215a76_Out_0 = _ColdLavaAO;
            half _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2;
            Unity_Subtract_half(1, _Property_13629a8ac7e261869193e0848c215a76_Out_0, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2);
            half _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3;
            Unity_Clamp_half(_Split_94cd24e718391e889d186d14735a5f81_G_2, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2, 1, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3);
            half _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3;
            Unity_Lerp_half(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3);
            half _Property_d965da3019019f838869a1fb407d698d_Out_0 = _ColdLavaEmissionMaskIntensivity;
            half _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2;
            Unity_Multiply_half(_Lerp_60114071ddee158f9af9b873a6d840d1_Out_3, _Property_d965da3019019f838869a1fb407d698d_Out_0, _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2);
            half _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1;
            Unity_Absolute_half(_Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2, _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1);
            half _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0 = _ColdLavaEmissionMaskTreshold;
            half _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2;
            Unity_Power_half(_Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1, _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2);
            half4 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4;
            half3 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5;
            half2 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6;
            Unity_Combine_half(_Multiply_086367572588138ebc1240441f2f400d_Out_2, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2, 0, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6);
            half _Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0 = _MediumLavaMetallic;
            half _Multiply_75ae743d3359148487052919f04e48b2_Out_2;
            Unity_Multiply_half(_Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0, _Split_503d896cedc1148aa1567e911ed3614b_R_1, _Multiply_75ae743d3359148487052919f04e48b2_Out_2);
            half _Property_71564d60d870518cbf142ff71794419d_Out_0 = _MediumLavaAO;
            half _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2;
            Unity_Subtract_half(1, _Property_71564d60d870518cbf142ff71794419d_Out_0, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2);
            half _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3;
            Unity_Clamp_half(_Split_503d896cedc1148aa1567e911ed3614b_G_2, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2, 1, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3);
            half _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3;
            Unity_Clamp_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, 0, 1, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3);
            half _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3;
            Unity_Lerp_half(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3, _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3);
            half _Property_459a51ffc4728c8ca2926024707897c6_Out_0 = _MediumLavaEmissionMaskIntesivity;
            half _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2;
            Unity_Multiply_half(_Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3, _Property_459a51ffc4728c8ca2926024707897c6_Out_0, _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2);
            half _Absolute_6e05a38014175a829a84304e3f621745_Out_1;
            Unity_Absolute_half(_Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2, _Absolute_6e05a38014175a829a84304e3f621745_Out_1);
            half _Property_493c2ccb3a27c580ab437efe58937c35_Out_0 = _MediumLavaEmissionMaskTreshold;
            half _Power_899ae999721ad384b72c681599af42de_Out_2;
            Unity_Power_half(_Absolute_6e05a38014175a829a84304e3f621745_Out_1, _Property_493c2ccb3a27c580ab437efe58937c35_Out_0, _Power_899ae999721ad384b72c681599af42de_Out_2);
            half4 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4;
            half3 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5;
            half2 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6;
            Unity_Combine_half(_Multiply_75ae743d3359148487052919f04e48b2_Out_2, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3, _Power_899ae999721ad384b72c681599af42de_Out_2, 0, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6);
            half3 _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3;
            Unity_Lerp_half3(_Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3);
            half _Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0 = _HotLavaMetallic;
            half4 _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_R_4 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.r;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_G_5 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.g;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_B_6 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.b;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.a;
            half4 _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_R_4 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.r;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_G_5 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.g;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_B_6 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.b;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.a;
            half4 _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0, _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxxx), _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3);
            half _Split_94d707688f4dff88abea8f5931660ff1_R_1 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[0];
            half _Split_94d707688f4dff88abea8f5931660ff1_G_2 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[1];
            half _Split_94d707688f4dff88abea8f5931660ff1_B_3 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[2];
            half _Split_94d707688f4dff88abea8f5931660ff1_A_4 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[3];
            half _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2;
            Unity_Multiply_half(_Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0, _Split_94d707688f4dff88abea8f5931660ff1_R_1, _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2);
            half _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0 = _HotLavaAO;
            half _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2;
            Unity_Subtract_half(1, _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2);
            half _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3;
            Unity_Clamp_half(_Split_94d707688f4dff88abea8f5931660ff1_G_2, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2, 1, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3);
            half _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3;
            Unity_Lerp_half(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7, _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3);
            half _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0 = _HotLavaEmissionMaskIntensivity;
            half _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2;
            Unity_Multiply_half(_Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3, _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0, _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2);
            half _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1;
            Unity_Absolute_half(_Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2, _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1);
            half _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0 = _HotLavaEmissionMaskTreshold;
            half _Power_0e477cda8e66268c882e8889cb195d72_Out_2;
            Unity_Power_half(_Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1, _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0, _Power_0e477cda8e66268c882e8889cb195d72_Out_2);
            half4 _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4;
            half3 _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5;
            half2 _Combine_a55e0256baa5a682b416f12d9adff678_RG_6;
            Unity_Combine_half(_Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3, _Power_0e477cda8e66268c882e8889cb195d72_Out_2, 0, _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, _Combine_a55e0256baa5a682b416f12d9adff678_RG_6);
            half3 _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3;
            Unity_Lerp_half3(_Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3);
            half _Split_be0161af2d147e82901bbdfc190c174f_R_1 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[0];
            half _Split_be0161af2d147e82901bbdfc190c174f_G_2 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[1];
            half _Split_be0161af2d147e82901bbdfc190c174f_B_3 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[2];
            half _Split_be0161af2d147e82901bbdfc190c174f_A_4 = 0;
            half4 _Property_b3443969e143738086170ebbcf185caa_Out_0 = _EmissionColor;
            half4 _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2;
            Unity_Multiply_half((_Split_be0161af2d147e82901bbdfc190c174f_B_3.xxxx), _Property_b3443969e143738086170ebbcf185caa_Out_0, _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2);
            UnityTexture2D _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            half _Property_4273893c41e28e8f83e15df255cfe5c4_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1fbdca361af74f8791981d29932b1108_Out_0 = _NoiseSpeed;
            half2 _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0 = _NoiseTiling;
            half2 _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2;
            Unity_Multiply_half(_Property_1fbdca361af74f8791981d29932b1108_Out_0, _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2);
            half4 _UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0 = IN.uv3;
            half2 _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2;
            Unity_Multiply_half(_Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2, (_UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0.xy), _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2);
            half _Split_3b56d52b260c438bbdce3d9a7263a123_R_1 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[0];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_G_2 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[1];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_B_3 = 0;
            half _Split_3b56d52b260c438bbdce3d9a7263a123_A_4 = 0;
            half2 _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0 = half2(_Split_3b56d52b260c438bbdce3d9a7263a123_G_2, _Split_3b56d52b260c438bbdce3d9a7263a123_R_1);
            half2 _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3;
            Unity_Branch_half2(_Property_4273893c41e28e8f83e15df255cfe5c4_Out_0, _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2, _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0, _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3);
            half _Fraction_461c199aeefb17858283be24648f92b2_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_461c199aeefb17858283be24648f92b2_Out_1);
            half2 _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_461c199aeefb17858283be24648f92b2_Out_1.xx), _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2);
            half _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0 = _GlobalTiling;
            half _Divide_e5638bd7513498828942af638e25e433_Out_2;
            Unity_Divide_half(1, _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0, _Divide_e5638bd7513498828942af638e25e433_Out_2);
            half4 _UV_a5163f4adfe6828bab8fe0f10836a494_Out_0 = IN.uv0;
            half2 _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2;
            Unity_Multiply_half(_Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, (_UV_a5163f4adfe6828bab8fe0f10836a494_Out_0.xy), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2);
            half2 _Multiply_72744c49357fca8c93e11635730250a0_Out_2;
            Unity_Multiply_half((_Divide_e5638bd7513498828942af638e25e433_Out_2.xx), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2);
            half2 _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2;
            Unity_Add_half2(_Multiply_4c650d4119378583a5ecc8db4c483008_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half4 _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_R_4 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.r;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_G_5 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.g;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_B_6 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.b;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.a;
            half _Fraction_b466830e1befc687924722e8a7039fd6_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_b466830e1befc687924722e8a7039fd6_Out_1);
            half2 _Multiply_7b0ff2a5179865869152be767c03f255_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_b466830e1befc687924722e8a7039fd6_Out_1.xx), _Multiply_7b0ff2a5179865869152be767c03f255_Out_2);
            half2 _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2;
            Unity_Add_half2(_Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Multiply_7b0ff2a5179865869152be767c03f255_Out_2, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half4 _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_R_4 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.r;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_G_5 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.g;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_B_6 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.b;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.a;
            half _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3;
            Unity_Lerp_half(_SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7, _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3);
            half _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1;
            Unity_Absolute_half(_Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3, _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1);
            half _Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0 = _ColdLavaNoisePower;
            half _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0 = _MediumLavaNoisePower;
            half _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3;
            Unity_Lerp_half(_Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0, _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3);
            half _Property_8a756da4fda1058f80ca49df1937f450_Out_0 = _HotLavaNoisePower;
            half _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3;
            Unity_Lerp_half(_Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3, _Property_8a756da4fda1058f80ca49df1937f450_Out_0, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3);
            half _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2;
            Unity_Power_half(_Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3, _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2);
            half _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2;
            Unity_Multiply_half(_Power_71ad5b7cab8b348ead45c623e2311de3_Out_2, 20, _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2);
            half _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3;
            Unity_Clamp_half(_Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2, 0.05, 1.2, _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3);
            half4 _Multiply_329a32a74e6e858696ca0b345435de30_Out_2;
            Unity_Multiply_half(_Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2, (_Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3.xxxx), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2);
            half4 _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3;
            Unity_Clamp_half4(_Multiply_329a32a74e6e858696ca0b345435de30_Out_2, half4(0, 0, 0, 0), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2, _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3);
            half4 _Multiply_6d53084a8041428287e1516d476b8861_Out_2;
            Unity_Multiply_half((_Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3.xxxx), _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3, _Multiply_6d53084a8041428287e1516d476b8861_Out_2);
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            surface.Emission = (_Multiply_6d53084a8041428287e1516d476b8861_Out_2.xyz);
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.VertexColor =                 input.color;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/LightingMetaPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            // Name: <None>
            Tags
            {
                "LightMode" = "Universal2D"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 4.5
        #pragma exclude_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_2D
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 normalWS;
            float4 texCoord0;
            float4 texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float4 uv0;
            float4 uv3;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            output.interp2.xyzw =  input.texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            output.texCoord3 = input.interp2.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBR2DPass.hlsl"

            ENDHLSL
        }
    }
    SubShader
    {
        Tags
        {
            "RenderPipeline"="UniversalPipeline"
            "RenderType"="Opaque"
            "UniversalMaterialType" = "Lit"
            "Queue"="Geometry"
        }
        Pass
        {
            Name "Universal Forward"
            Tags
            {
                "LightMode" = "UniversalForward"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
        #pragma only_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma multi_compile_fog
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma multi_compile _ _SCREEN_SPACE_OCCLUSION
        #pragma multi_compile _ LIGHTMAP_ON
        #pragma multi_compile _ DIRLIGHTMAP_COMBINED
        #pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
        #pragma multi_compile _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS _ADDITIONAL_OFF
        #pragma multi_compile _ _ADDITIONAL_LIGHT_SHADOWS
        #pragma multi_compile _ _SHADOWS_SOFT
        #pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
        #pragma multi_compile _ SHADOWS_SHADOWMASK
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_POSITION_WS
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
            #define VARYINGS_NEED_VIEWDIRECTION_WS
            #define VARYINGS_NEED_FOG_AND_VERTEX_LIGHT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_FORWARD
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv3 : TEXCOORD3;
            float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 positionWS;
            float3 normalWS;
            float4 tangentWS;
            float4 texCoord0;
            float4 texCoord3;
            float4 color;
            float3 viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            float2 lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            float3 sh;
            #endif
            float4 fogFactorAndVertexLight;
            float4 shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float3 TangentSpaceNormal;
            float4 uv0;
            float4 uv3;
            float4 VertexColor;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float3 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            float4 interp4 : TEXCOORD4;
            float4 interp5 : TEXCOORD5;
            float3 interp6 : TEXCOORD6;
            #if defined(LIGHTMAP_ON)
            float2 interp7 : TEXCOORD7;
            #endif
            #if !defined(LIGHTMAP_ON)
            float3 interp8 : TEXCOORD8;
            #endif
            float4 interp9 : TEXCOORD9;
            float4 interp10 : TEXCOORD10;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.positionWS;
            output.interp1.xyz =  input.normalWS;
            output.interp2.xyzw =  input.tangentWS;
            output.interp3.xyzw =  input.texCoord0;
            output.interp4.xyzw =  input.texCoord3;
            output.interp5.xyzw =  input.color;
            output.interp6.xyz =  input.viewDirectionWS;
            #if defined(LIGHTMAP_ON)
            output.interp7.xy =  input.lightmapUV;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.interp8.xyz =  input.sh;
            #endif
            output.interp9.xyzw =  input.fogFactorAndVertexLight;
            output.interp10.xyzw =  input.shadowCoord;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.positionWS = input.interp0.xyz;
            output.normalWS = input.interp1.xyz;
            output.tangentWS = input.interp2.xyzw;
            output.texCoord0 = input.interp3.xyzw;
            output.texCoord3 = input.interp4.xyzw;
            output.color = input.interp5.xyzw;
            output.viewDirectionWS = input.interp6.xyz;
            #if defined(LIGHTMAP_ON)
            output.lightmapUV = input.interp7.xy;
            #endif
            #if !defined(LIGHTMAP_ON)
            output.sh = input.interp8.xyz;
            #endif
            output.fogFactorAndVertexLight = input.interp9.xyzw;
            output.shadowCoord = input.interp10.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

        void Unity_NormalStrength_half(half3 In, half Strength, out half3 Out)
        {
            Out = half3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half(half A, half B, half T, out half Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Clamp_half4(half4 In, half4 Min, half4 Max, out half4 Out)
        {
            Out = clamp(In, Min, Max);
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
            half3 NormalTS;
            half3 Emission;
            half Metallic;
            half Smoothness;
            half Occlusion;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            UnityTexture2D _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0);
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_R_4 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.r;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_G_5 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.g;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_B_6 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.b;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_A_7 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.a;
            half _Property_30c36922aabc618192374556ee8ce299_Out_0 = _ColdLavaNormalScale;
            half3 _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2);
            half4 _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0);
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_R_4 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.r;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_G_5 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.g;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_B_6 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.b;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_A_7 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.a;
            half3 _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2);
            half3 _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3;
            Unity_Lerp_half3(_NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxx), _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3);
            UnityTexture2D _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0);
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_R_4 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.r;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_G_5 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.g;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_B_6 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.b;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_A_7 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.a;
            half _Property_c488bf556481e28d8a97898896b5cdec_Out_0 = _MediumLavaNormalScale;
            half3 _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2);
            half4 _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0);
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_R_4 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.r;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_G_5 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.g;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_B_6 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.b;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_A_7 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.a;
            half3 _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2);
            half3 _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3;
            Unity_Lerp_half3(_NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxx), _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3);
            half3 _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3;
            Unity_Lerp_half3(_Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3, _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3);
            UnityTexture2D _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half4 _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0);
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_R_4 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.r;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_G_5 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.g;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_B_6 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.b;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_A_7 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.a;
            half _Property_48f492f6311fa887a8666bf46f288d9d_Out_0 = _HotLavaNormalScale;
            half3 _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2);
            half4 _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0);
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_R_4 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.r;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_G_5 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.g;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_B_6 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.b;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_A_7 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.a;
            half3 _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2);
            half3 _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3;
            Unity_Lerp_half3(_NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxx), _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3);
            half3 _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            Unity_Lerp_half3(_Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3, _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_85790e354b8b8189bf7442246db27aca_Out_3);
            half _Split_b898e81d55670f89944c32a8494f1543_R_1 = IN.VertexColor[0];
            half _Split_b898e81d55670f89944c32a8494f1543_G_2 = IN.VertexColor[1];
            half _Split_b898e81d55670f89944c32a8494f1543_B_3 = IN.VertexColor[2];
            half _Split_b898e81d55670f89944c32a8494f1543_A_4 = IN.VertexColor[3];
            half _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3;
            Unity_Clamp_half(_Split_b898e81d55670f89944c32a8494f1543_A_4, 0, 1, _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3);
            half _Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0 = _ColdLavaMetalic;
            half _Multiply_086367572588138ebc1240441f2f400d_Out_2;
            Unity_Multiply_half(_Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0, _Split_94cd24e718391e889d186d14735a5f81_R_1, _Multiply_086367572588138ebc1240441f2f400d_Out_2);
            half _Property_13629a8ac7e261869193e0848c215a76_Out_0 = _ColdLavaAO;
            half _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2;
            Unity_Subtract_half(1, _Property_13629a8ac7e261869193e0848c215a76_Out_0, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2);
            half _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3;
            Unity_Clamp_half(_Split_94cd24e718391e889d186d14735a5f81_G_2, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2, 1, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3);
            half _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3;
            Unity_Lerp_half(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3);
            half _Property_d965da3019019f838869a1fb407d698d_Out_0 = _ColdLavaEmissionMaskIntensivity;
            half _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2;
            Unity_Multiply_half(_Lerp_60114071ddee158f9af9b873a6d840d1_Out_3, _Property_d965da3019019f838869a1fb407d698d_Out_0, _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2);
            half _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1;
            Unity_Absolute_half(_Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2, _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1);
            half _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0 = _ColdLavaEmissionMaskTreshold;
            half _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2;
            Unity_Power_half(_Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1, _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2);
            half4 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4;
            half3 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5;
            half2 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6;
            Unity_Combine_half(_Multiply_086367572588138ebc1240441f2f400d_Out_2, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2, 0, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6);
            half _Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0 = _MediumLavaMetallic;
            half _Multiply_75ae743d3359148487052919f04e48b2_Out_2;
            Unity_Multiply_half(_Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0, _Split_503d896cedc1148aa1567e911ed3614b_R_1, _Multiply_75ae743d3359148487052919f04e48b2_Out_2);
            half _Property_71564d60d870518cbf142ff71794419d_Out_0 = _MediumLavaAO;
            half _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2;
            Unity_Subtract_half(1, _Property_71564d60d870518cbf142ff71794419d_Out_0, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2);
            half _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3;
            Unity_Clamp_half(_Split_503d896cedc1148aa1567e911ed3614b_G_2, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2, 1, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3);
            half _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3;
            Unity_Clamp_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, 0, 1, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3);
            half _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3;
            Unity_Lerp_half(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3, _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3);
            half _Property_459a51ffc4728c8ca2926024707897c6_Out_0 = _MediumLavaEmissionMaskIntesivity;
            half _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2;
            Unity_Multiply_half(_Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3, _Property_459a51ffc4728c8ca2926024707897c6_Out_0, _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2);
            half _Absolute_6e05a38014175a829a84304e3f621745_Out_1;
            Unity_Absolute_half(_Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2, _Absolute_6e05a38014175a829a84304e3f621745_Out_1);
            half _Property_493c2ccb3a27c580ab437efe58937c35_Out_0 = _MediumLavaEmissionMaskTreshold;
            half _Power_899ae999721ad384b72c681599af42de_Out_2;
            Unity_Power_half(_Absolute_6e05a38014175a829a84304e3f621745_Out_1, _Property_493c2ccb3a27c580ab437efe58937c35_Out_0, _Power_899ae999721ad384b72c681599af42de_Out_2);
            half4 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4;
            half3 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5;
            half2 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6;
            Unity_Combine_half(_Multiply_75ae743d3359148487052919f04e48b2_Out_2, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3, _Power_899ae999721ad384b72c681599af42de_Out_2, 0, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6);
            half3 _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3;
            Unity_Lerp_half3(_Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3);
            half _Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0 = _HotLavaMetallic;
            half4 _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_R_4 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.r;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_G_5 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.g;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_B_6 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.b;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.a;
            half4 _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_R_4 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.r;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_G_5 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.g;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_B_6 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.b;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.a;
            half4 _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0, _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxxx), _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3);
            half _Split_94d707688f4dff88abea8f5931660ff1_R_1 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[0];
            half _Split_94d707688f4dff88abea8f5931660ff1_G_2 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[1];
            half _Split_94d707688f4dff88abea8f5931660ff1_B_3 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[2];
            half _Split_94d707688f4dff88abea8f5931660ff1_A_4 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[3];
            half _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2;
            Unity_Multiply_half(_Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0, _Split_94d707688f4dff88abea8f5931660ff1_R_1, _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2);
            half _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0 = _HotLavaAO;
            half _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2;
            Unity_Subtract_half(1, _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2);
            half _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3;
            Unity_Clamp_half(_Split_94d707688f4dff88abea8f5931660ff1_G_2, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2, 1, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3);
            half _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3;
            Unity_Lerp_half(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7, _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3);
            half _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0 = _HotLavaEmissionMaskIntensivity;
            half _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2;
            Unity_Multiply_half(_Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3, _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0, _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2);
            half _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1;
            Unity_Absolute_half(_Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2, _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1);
            half _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0 = _HotLavaEmissionMaskTreshold;
            half _Power_0e477cda8e66268c882e8889cb195d72_Out_2;
            Unity_Power_half(_Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1, _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0, _Power_0e477cda8e66268c882e8889cb195d72_Out_2);
            half4 _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4;
            half3 _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5;
            half2 _Combine_a55e0256baa5a682b416f12d9adff678_RG_6;
            Unity_Combine_half(_Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3, _Power_0e477cda8e66268c882e8889cb195d72_Out_2, 0, _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, _Combine_a55e0256baa5a682b416f12d9adff678_RG_6);
            half3 _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3;
            Unity_Lerp_half3(_Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3);
            half _Split_be0161af2d147e82901bbdfc190c174f_R_1 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[0];
            half _Split_be0161af2d147e82901bbdfc190c174f_G_2 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[1];
            half _Split_be0161af2d147e82901bbdfc190c174f_B_3 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[2];
            half _Split_be0161af2d147e82901bbdfc190c174f_A_4 = 0;
            half4 _Property_b3443969e143738086170ebbcf185caa_Out_0 = _EmissionColor;
            half4 _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2;
            Unity_Multiply_half((_Split_be0161af2d147e82901bbdfc190c174f_B_3.xxxx), _Property_b3443969e143738086170ebbcf185caa_Out_0, _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2);
            UnityTexture2D _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            half _Property_4273893c41e28e8f83e15df255cfe5c4_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1fbdca361af74f8791981d29932b1108_Out_0 = _NoiseSpeed;
            half2 _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0 = _NoiseTiling;
            half2 _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2;
            Unity_Multiply_half(_Property_1fbdca361af74f8791981d29932b1108_Out_0, _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2);
            half4 _UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0 = IN.uv3;
            half2 _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2;
            Unity_Multiply_half(_Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2, (_UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0.xy), _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2);
            half _Split_3b56d52b260c438bbdce3d9a7263a123_R_1 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[0];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_G_2 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[1];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_B_3 = 0;
            half _Split_3b56d52b260c438bbdce3d9a7263a123_A_4 = 0;
            half2 _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0 = half2(_Split_3b56d52b260c438bbdce3d9a7263a123_G_2, _Split_3b56d52b260c438bbdce3d9a7263a123_R_1);
            half2 _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3;
            Unity_Branch_half2(_Property_4273893c41e28e8f83e15df255cfe5c4_Out_0, _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2, _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0, _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3);
            half _Fraction_461c199aeefb17858283be24648f92b2_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_461c199aeefb17858283be24648f92b2_Out_1);
            half2 _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_461c199aeefb17858283be24648f92b2_Out_1.xx), _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2);
            half _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0 = _GlobalTiling;
            half _Divide_e5638bd7513498828942af638e25e433_Out_2;
            Unity_Divide_half(1, _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0, _Divide_e5638bd7513498828942af638e25e433_Out_2);
            half4 _UV_a5163f4adfe6828bab8fe0f10836a494_Out_0 = IN.uv0;
            half2 _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2;
            Unity_Multiply_half(_Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, (_UV_a5163f4adfe6828bab8fe0f10836a494_Out_0.xy), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2);
            half2 _Multiply_72744c49357fca8c93e11635730250a0_Out_2;
            Unity_Multiply_half((_Divide_e5638bd7513498828942af638e25e433_Out_2.xx), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2);
            half2 _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2;
            Unity_Add_half2(_Multiply_4c650d4119378583a5ecc8db4c483008_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half4 _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_R_4 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.r;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_G_5 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.g;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_B_6 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.b;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.a;
            half _Fraction_b466830e1befc687924722e8a7039fd6_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_b466830e1befc687924722e8a7039fd6_Out_1);
            half2 _Multiply_7b0ff2a5179865869152be767c03f255_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_b466830e1befc687924722e8a7039fd6_Out_1.xx), _Multiply_7b0ff2a5179865869152be767c03f255_Out_2);
            half2 _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2;
            Unity_Add_half2(_Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Multiply_7b0ff2a5179865869152be767c03f255_Out_2, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half4 _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_R_4 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.r;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_G_5 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.g;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_B_6 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.b;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.a;
            half _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3;
            Unity_Lerp_half(_SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7, _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3);
            half _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1;
            Unity_Absolute_half(_Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3, _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1);
            half _Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0 = _ColdLavaNoisePower;
            half _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0 = _MediumLavaNoisePower;
            half _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3;
            Unity_Lerp_half(_Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0, _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3);
            half _Property_8a756da4fda1058f80ca49df1937f450_Out_0 = _HotLavaNoisePower;
            half _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3;
            Unity_Lerp_half(_Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3, _Property_8a756da4fda1058f80ca49df1937f450_Out_0, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3);
            half _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2;
            Unity_Power_half(_Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3, _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2);
            half _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2;
            Unity_Multiply_half(_Power_71ad5b7cab8b348ead45c623e2311de3_Out_2, 20, _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2);
            half _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3;
            Unity_Clamp_half(_Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2, 0.05, 1.2, _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3);
            half4 _Multiply_329a32a74e6e858696ca0b345435de30_Out_2;
            Unity_Multiply_half(_Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2, (_Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3.xxxx), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2);
            half4 _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3;
            Unity_Clamp_half4(_Multiply_329a32a74e6e858696ca0b345435de30_Out_2, half4(0, 0, 0, 0), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2, _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3);
            half4 _Multiply_6d53084a8041428287e1516d476b8861_Out_2;
            Unity_Multiply_half((_Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3.xxxx), _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3, _Multiply_6d53084a8041428287e1516d476b8861_Out_2);
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_R_1 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[0];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_G_2 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[1];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_B_3 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[2];
            half _Split_ab2d68297c00de8bb6f4fe8fc688cd05_A_4 = _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3[3];
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            surface.NormalTS = _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            surface.Emission = (_Multiply_6d53084a8041428287e1516d476b8861_Out_2.xyz);
            surface.Metallic = _Split_be0161af2d147e82901bbdfc190c174f_R_1;
            surface.Smoothness = _Split_ab2d68297c00de8bb6f4fe8fc688cd05_A_4;
            surface.Occlusion = _Split_be0161af2d147e82901bbdfc190c174f_G_2;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.VertexColor =                 input.color;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBRForwardPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "ShadowCaster"
            Tags
            {
                "LightMode" = "ShadowCaster"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On
        ColorMask 0

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
        #pragma only_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma multi_compile _ _CASTING_PUNCTUAL_LIGHT_SHADOW
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_SHADOWCASTER
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            // GraphFunctions: <None>

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShadowCasterPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "DepthOnly"
            Tags
            {
                "LightMode" = "DepthOnly"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On
        ColorMask 0

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
        #pragma only_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHONLY
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            // GraphFunctions: <None>

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);





        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthOnlyPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "DepthNormals"
            Tags
            {
                "LightMode" = "DepthNormals"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
        #pragma only_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TANGENT_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_DEPTHNORMALSONLY
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 normalWS;
            float4 tangentWS;
            float4 texCoord0;
            float4 texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float3 TangentSpaceNormal;
            float4 uv0;
            float4 uv3;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.tangentWS;
            output.interp2.xyzw =  input.texCoord0;
            output.interp3.xyzw =  input.texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.tangentWS = input.interp1.xyzw;
            output.texCoord0 = input.interp2.xyzw;
            output.texCoord3 = input.interp3.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_NormalStrength_half(half3 In, half Strength, out half3 Out)
        {
            Out = half3(In.rg * Strength, lerp(1, In.b, saturate(Strength)));
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 NormalTS;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0);
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_R_4 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.r;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_G_5 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.g;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_B_6 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.b;
            half _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_A_7 = _SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.a;
            half _Property_30c36922aabc618192374556ee8ce299_Out_0 = _ColdLavaNormalScale;
            half3 _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_2a19232462fd2d83a3dcaeb21503d1fd_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2);
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0 = SAMPLE_TEXTURE2D(_Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.tex, _Property_973bdb6610abba8aaa3cef63a78baa06_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0);
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_R_4 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.r;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_G_5 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.g;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_B_6 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.b;
            half _SampleTexture2D_1ffc3e624270ee899e223323127b2536_A_7 = _SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.a;
            half3 _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_1ffc3e624270ee899e223323127b2536_RGBA_0.xyz), _Property_30c36922aabc618192374556ee8ce299_Out_0, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2);
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half3 _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3;
            Unity_Lerp_half3(_NormalStrength_768a023761a00b8bac27ec164d4feca3_Out_2, _NormalStrength_4e7cd17dd72ec485804b19279c62419c_Out_2, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxx), _Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3);
            UnityTexture2D _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0);
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_R_4 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.r;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_G_5 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.g;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_B_6 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.b;
            half _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_A_7 = _SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.a;
            half _Property_c488bf556481e28d8a97898896b5cdec_Out_0 = _MediumLavaNormalScale;
            half3 _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_4f3a718c2123288ea2ba36a9344e64e9_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2);
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.tex, _Property_d418387b80a0018fb3928ae94e5c71d9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0);
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_R_4 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.r;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_G_5 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.g;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_B_6 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.b;
            half _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_A_7 = _SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.a;
            half3 _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_e0bdc3ec5369e88bab241bd5157590f1_RGBA_0.xyz), _Property_c488bf556481e28d8a97898896b5cdec_Out_0, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2);
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half3 _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3;
            Unity_Lerp_half3(_NormalStrength_353595f40a3cf58cb0c8a5f951505d9a_Out_2, _NormalStrength_59ae87d6cd8faa8f9fea0640fb1a6c7c_Out_2, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxx), _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half3 _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3;
            Unity_Lerp_half3(_Lerp_63c09ae12140de8da5d72b0b88f55a50_Out_3, _Lerp_dd9598f6e61c5d85886c8f9a886b7d1b_Out_3, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3);
            UnityTexture2D _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaNormal);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0);
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_R_4 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.r;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_G_5 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.g;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_B_6 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.b;
            half _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_A_7 = _SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.a;
            half _Property_48f492f6311fa887a8666bf46f288d9d_Out_0 = _HotLavaNormalScale;
            half3 _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_571502bc4c48ad8e8d75d0743c4fd5d6_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2);
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0 = SAMPLE_TEXTURE2D(_Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.tex, _Property_290ade081dbe8389a26a7b6d07e6044f_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.rgb = UnpackNormal(_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0);
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_R_4 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.r;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_G_5 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.g;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_B_6 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.b;
            half _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_A_7 = _SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.a;
            half3 _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2;
            Unity_NormalStrength_half((_SampleTexture2D_785ee097397c3688ad51d76c6fc5ba77_RGBA_0.xyz), _Property_48f492f6311fa887a8666bf46f288d9d_Out_0, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2);
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half3 _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3;
            Unity_Lerp_half3(_NormalStrength_9ebe899be918d38cac53ee34e22a3dc2_Out_2, _NormalStrength_52f08b3e9ed3698faef8a8a64e53b5cf_Out_2, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxx), _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half3 _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            Unity_Lerp_half3(_Lerp_e4fa486d1ccf2c89a46bb1b2e14a0f39_Out_3, _Lerp_18ff3f20527a1f80bfa8428835890e50_Out_3, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_85790e354b8b8189bf7442246db27aca_Out_3);
            surface.NormalTS = _Lerp_85790e354b8b8189bf7442246db27aca_Out_3;
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph
            output.TangentSpaceNormal =          float3(0.0f, 0.0f, 1.0f);


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/DepthNormalsOnlyPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            Name "Meta"
            Tags
            {
                "LightMode" = "Meta"
            }

            // Render State
            Cull Off

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
        #pragma only_renderers gles gles3 glcore
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            #pragma shader_feature _ _SMOOTHNESS_TEXTURE_ALBEDO_CHANNEL_A
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD1
            #define ATTRIBUTES_NEED_TEXCOORD2
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define ATTRIBUTES_NEED_COLOR
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define VARYINGS_NEED_COLOR
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_META
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv1 : TEXCOORD1;
            float4 uv2 : TEXCOORD2;
            float4 uv3 : TEXCOORD3;
            float4 color : COLOR;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 normalWS;
            float4 texCoord0;
            float4 texCoord3;
            float4 color;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float4 uv0;
            float4 uv3;
            float4 VertexColor;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            float4 interp3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            output.interp2.xyzw =  input.texCoord3;
            output.interp3.xyzw =  input.color;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            output.texCoord3 = input.interp2.xyzw;
            output.color = input.interp3.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

        void Unity_Lerp_half(half A, half B, half T, out half Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Lerp_half3(half3 A, half3 B, half3 T, out half3 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Clamp_half4(half4 In, half4 Min, half4 Max, out half4 Out)
        {
            Out = clamp(In, Min, Max);
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
            half3 Emission;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            half _Split_b898e81d55670f89944c32a8494f1543_R_1 = IN.VertexColor[0];
            half _Split_b898e81d55670f89944c32a8494f1543_G_2 = IN.VertexColor[1];
            half _Split_b898e81d55670f89944c32a8494f1543_B_3 = IN.VertexColor[2];
            half _Split_b898e81d55670f89944c32a8494f1543_A_4 = IN.VertexColor[3];
            half _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3;
            Unity_Clamp_half(_Split_b898e81d55670f89944c32a8494f1543_A_4, 0, 1, _Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3);
            half _Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0 = _ColdLavaMetalic;
            half _Multiply_086367572588138ebc1240441f2f400d_Out_2;
            Unity_Multiply_half(_Property_0dcc58d7fae4a88ba2336bd311b40f19_Out_0, _Split_94cd24e718391e889d186d14735a5f81_R_1, _Multiply_086367572588138ebc1240441f2f400d_Out_2);
            half _Property_13629a8ac7e261869193e0848c215a76_Out_0 = _ColdLavaAO;
            half _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2;
            Unity_Subtract_half(1, _Property_13629a8ac7e261869193e0848c215a76_Out_0, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2);
            half _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3;
            Unity_Clamp_half(_Split_94cd24e718391e889d186d14735a5f81_G_2, _Subtract_dc12c0f384708b8fa4a87f78882cf2c9_Out_2, 1, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3);
            half _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3;
            Unity_Lerp_half(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_60114071ddee158f9af9b873a6d840d1_Out_3);
            half _Property_d965da3019019f838869a1fb407d698d_Out_0 = _ColdLavaEmissionMaskIntensivity;
            half _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2;
            Unity_Multiply_half(_Lerp_60114071ddee158f9af9b873a6d840d1_Out_3, _Property_d965da3019019f838869a1fb407d698d_Out_0, _Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2);
            half _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1;
            Unity_Absolute_half(_Multiply_6b1473d0f44e4e82bcf34936acd1a95a_Out_2, _Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1);
            half _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0 = _ColdLavaEmissionMaskTreshold;
            half _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2;
            Unity_Power_half(_Absolute_c53321c5bc0f868d9de81835ef6f4088_Out_1, _Property_0b9196e386b4ae85a838a11d69d1f2de_Out_0, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2);
            half4 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4;
            half3 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5;
            half2 _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6;
            Unity_Combine_half(_Multiply_086367572588138ebc1240441f2f400d_Out_2, _Clamp_1410b9818aefd882b5441fa87950e0a6_Out_3, _Power_861f8c527ae8548caad9f4bf5fc72029_Out_2, 0, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGBA_4, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RG_6);
            half _Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0 = _MediumLavaMetallic;
            half _Multiply_75ae743d3359148487052919f04e48b2_Out_2;
            Unity_Multiply_half(_Property_d5d35ee97dd2fd8185146b2cbd679bcd_Out_0, _Split_503d896cedc1148aa1567e911ed3614b_R_1, _Multiply_75ae743d3359148487052919f04e48b2_Out_2);
            half _Property_71564d60d870518cbf142ff71794419d_Out_0 = _MediumLavaAO;
            half _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2;
            Unity_Subtract_half(1, _Property_71564d60d870518cbf142ff71794419d_Out_0, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2);
            half _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3;
            Unity_Clamp_half(_Split_503d896cedc1148aa1567e911ed3614b_G_2, _Subtract_1744923d096e8885aea6bc6cc552f975_Out_2, 1, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3);
            half _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3;
            Unity_Clamp_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, 0, 1, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3);
            half _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3;
            Unity_Lerp_half(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7, _Clamp_dadddbb9d4aefc81bb21dc6aee563aab_Out_3, _Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3);
            half _Property_459a51ffc4728c8ca2926024707897c6_Out_0 = _MediumLavaEmissionMaskIntesivity;
            half _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2;
            Unity_Multiply_half(_Lerp_86d79a5fd121e78398e4ef45afdd6f37_Out_3, _Property_459a51ffc4728c8ca2926024707897c6_Out_0, _Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2);
            half _Absolute_6e05a38014175a829a84304e3f621745_Out_1;
            Unity_Absolute_half(_Multiply_0fa7b2efab7b5a8e881668d02edd0564_Out_2, _Absolute_6e05a38014175a829a84304e3f621745_Out_1);
            half _Property_493c2ccb3a27c580ab437efe58937c35_Out_0 = _MediumLavaEmissionMaskTreshold;
            half _Power_899ae999721ad384b72c681599af42de_Out_2;
            Unity_Power_half(_Absolute_6e05a38014175a829a84304e3f621745_Out_1, _Property_493c2ccb3a27c580ab437efe58937c35_Out_0, _Power_899ae999721ad384b72c681599af42de_Out_2);
            half4 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4;
            half3 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5;
            half2 _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6;
            Unity_Combine_half(_Multiply_75ae743d3359148487052919f04e48b2_Out_2, _Clamp_05e5ffdd0a816887833802741aa74f0a_Out_3, _Power_899ae999721ad384b72c681599af42de_Out_2, 0, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGBA_4, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RG_6);
            half3 _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3;
            Unity_Lerp_half3(_Combine_c59b12698cf8ec8f89598ca72a0c2ba3_RGB_5, _Combine_e8537fccc132f988b4e66b7fbcb0054a_RGB_5, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxx), _Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3);
            half _Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0 = _HotLavaMetallic;
            half4 _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_R_4 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.r;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_G_5 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.g;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_B_6 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.b;
            half _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7 = _SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0.a;
            half4 _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_R_4 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.r;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_G_5 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.g;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_B_6 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.b;
            half _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7 = _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0.a;
            half4 _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_RGBA_0, _SampleTexture2D_03c935556def188b9bc5100587e1286e_RGBA_0, (_Clamp_44546232e7f62087a73f2c7998c0c775_Out_3.xxxx), _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3);
            half _Split_94d707688f4dff88abea8f5931660ff1_R_1 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[0];
            half _Split_94d707688f4dff88abea8f5931660ff1_G_2 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[1];
            half _Split_94d707688f4dff88abea8f5931660ff1_B_3 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[2];
            half _Split_94d707688f4dff88abea8f5931660ff1_A_4 = _Lerp_79f8c8161fb394818f7c8937b1054b53_Out_3[3];
            half _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2;
            Unity_Multiply_half(_Property_bd7e990dab5d208d8620b5e1f44874a7_Out_0, _Split_94d707688f4dff88abea8f5931660ff1_R_1, _Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2);
            half _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0 = _HotLavaAO;
            half _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2;
            Unity_Subtract_half(1, _Property_e283a823a4aece82b7b75b005a98c0e4_Out_0, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2);
            half _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3;
            Unity_Clamp_half(_Split_94d707688f4dff88abea8f5931660ff1_G_2, _Subtract_1b951849f3879a89ba4754f096b27f78_Out_2, 1, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3);
            half _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3;
            Unity_Lerp_half(_SampleTexture2D_77e57223e0c2a685b9a9a05b971858ae_A_7, _SampleTexture2D_03c935556def188b9bc5100587e1286e_A_7, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3);
            half _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0 = _HotLavaEmissionMaskIntensivity;
            half _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2;
            Unity_Multiply_half(_Lerp_496c3cb184f74e81b99eb5acea16b6a0_Out_3, _Property_515e46f8e19c928393b13e9f9cbdb04e_Out_0, _Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2);
            half _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1;
            Unity_Absolute_half(_Multiply_4a03b8b1ae505d86a988822af9ecfb45_Out_2, _Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1);
            half _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0 = _HotLavaEmissionMaskTreshold;
            half _Power_0e477cda8e66268c882e8889cb195d72_Out_2;
            Unity_Power_half(_Absolute_d1d0ea347a23fb81a2c31ad237e5a357_Out_1, _Property_f496c3d8b9533f84a76c0fde29dfef08_Out_0, _Power_0e477cda8e66268c882e8889cb195d72_Out_2);
            half4 _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4;
            half3 _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5;
            half2 _Combine_a55e0256baa5a682b416f12d9adff678_RG_6;
            Unity_Combine_half(_Multiply_8c23c6f15dbc0b89b63b439186a74461_Out_2, _Clamp_d08049eeee098087ab9597b4ca5669b9_Out_3, _Power_0e477cda8e66268c882e8889cb195d72_Out_2, 0, _Combine_a55e0256baa5a682b416f12d9adff678_RGBA_4, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, _Combine_a55e0256baa5a682b416f12d9adff678_RG_6);
            half3 _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3;
            Unity_Lerp_half3(_Lerp_8ab58e5131383f8baeb4b7abe9b54719_Out_3, _Combine_a55e0256baa5a682b416f12d9adff678_RGB_5, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxx), _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3);
            half _Split_be0161af2d147e82901bbdfc190c174f_R_1 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[0];
            half _Split_be0161af2d147e82901bbdfc190c174f_G_2 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[1];
            half _Split_be0161af2d147e82901bbdfc190c174f_B_3 = _Lerp_407a4e984788d48e9dd385caf96f3e6d_Out_3[2];
            half _Split_be0161af2d147e82901bbdfc190c174f_A_4 = 0;
            half4 _Property_b3443969e143738086170ebbcf185caa_Out_0 = _EmissionColor;
            half4 _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2;
            Unity_Multiply_half((_Split_be0161af2d147e82901bbdfc190c174f_B_3.xxxx), _Property_b3443969e143738086170ebbcf185caa_Out_0, _Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2);
            UnityTexture2D _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0 = UnityBuildTexture2DStructNoScale(_Noise);
            half _Property_4273893c41e28e8f83e15df255cfe5c4_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1fbdca361af74f8791981d29932b1108_Out_0 = _NoiseSpeed;
            half2 _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0 = _NoiseTiling;
            half2 _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2;
            Unity_Multiply_half(_Property_1fbdca361af74f8791981d29932b1108_Out_0, _Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, _Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2);
            half4 _UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0 = IN.uv3;
            half2 _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2;
            Unity_Multiply_half(_Multiply_61eea14ff48bf1808c776d1f67469e57_Out_2, (_UV_9ef9e4e6c5e70383b615949b81b11d0c_Out_0.xy), _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2);
            half _Split_3b56d52b260c438bbdce3d9a7263a123_R_1 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[0];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_G_2 = _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2[1];
            half _Split_3b56d52b260c438bbdce3d9a7263a123_B_3 = 0;
            half _Split_3b56d52b260c438bbdce3d9a7263a123_A_4 = 0;
            half2 _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0 = half2(_Split_3b56d52b260c438bbdce3d9a7263a123_G_2, _Split_3b56d52b260c438bbdce3d9a7263a123_R_1);
            half2 _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3;
            Unity_Branch_half2(_Property_4273893c41e28e8f83e15df255cfe5c4_Out_0, _Multiply_0be8ca5b6a2c73889322e66a7b9d302a_Out_2, _Vector2_1fa216cb0d85fd8fb968fc0798345cac_Out_0, _Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3);
            half _Fraction_461c199aeefb17858283be24648f92b2_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_461c199aeefb17858283be24648f92b2_Out_1);
            half2 _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_461c199aeefb17858283be24648f92b2_Out_1.xx), _Multiply_4c650d4119378583a5ecc8db4c483008_Out_2);
            half _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0 = _GlobalTiling;
            half _Divide_e5638bd7513498828942af638e25e433_Out_2;
            Unity_Divide_half(1, _Property_f29cfb3a736d4e84af114cbf820ee141_Out_0, _Divide_e5638bd7513498828942af638e25e433_Out_2);
            half4 _UV_a5163f4adfe6828bab8fe0f10836a494_Out_0 = IN.uv0;
            half2 _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2;
            Unity_Multiply_half(_Property_cd0729367b406d8cacc6ccd0f976623f_Out_0, (_UV_a5163f4adfe6828bab8fe0f10836a494_Out_0.xy), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2);
            half2 _Multiply_72744c49357fca8c93e11635730250a0_Out_2;
            Unity_Multiply_half((_Divide_e5638bd7513498828942af638e25e433_Out_2.xx), _Multiply_329b104f1b3b948cbeb846aaf8c0e720_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2);
            half2 _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2;
            Unity_Add_half2(_Multiply_4c650d4119378583a5ecc8db4c483008_Out_2, _Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half4 _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_b4b4a7ed755cc089b0ce5d74b7988480_Out_2);
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_R_4 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.r;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_G_5 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.g;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_B_6 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.b;
            half _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7 = _SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_RGBA_0.a;
            half _Fraction_b466830e1befc687924722e8a7039fd6_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_b466830e1befc687924722e8a7039fd6_Out_1);
            half2 _Multiply_7b0ff2a5179865869152be767c03f255_Out_2;
            Unity_Multiply_half(_Branch_c66f80a3f861ad8c88820062dcb31be2_Out_3, (_Fraction_b466830e1befc687924722e8a7039fd6_Out_1.xx), _Multiply_7b0ff2a5179865869152be767c03f255_Out_2);
            half2 _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2;
            Unity_Add_half2(_Multiply_72744c49357fca8c93e11635730250a0_Out_2, _Multiply_7b0ff2a5179865869152be767c03f255_Out_2, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half4 _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.tex, _Property_b204f4dc0e3a57808a1b3d2afeac1751_Out_0.samplerstate, _Add_49b59b2dc2aed78b9eae4886fbe52982_Out_2);
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_R_4 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.r;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_G_5 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.g;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_B_6 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.b;
            half _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7 = _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_RGBA_0.a;
            half _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3;
            Unity_Lerp_half(_SampleTexture2D_c5434a9c2ec579878f4cd8248e70b955_A_7, _SampleTexture2D_44313f2c910f9f8f8a87c190445fdac0_A_7, _Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3);
            half _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1;
            Unity_Absolute_half(_Lerp_a61b31a158d25a80a7ed85f97cdf0e36_Out_3, _Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1);
            half _Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0 = _ColdLavaNoisePower;
            half _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0 = _MediumLavaNoisePower;
            half _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3;
            Unity_Lerp_half(_Property_46ee64e6cca7cf819ebc27703d71fc3a_Out_0, _Property_a3f785ba1025c483b9e4d05a939efcac_Out_0, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3);
            half _Property_8a756da4fda1058f80ca49df1937f450_Out_0 = _HotLavaNoisePower;
            half _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3;
            Unity_Lerp_half(_Lerp_e859fb8bea9a2b8d8e157dca75b8d969_Out_3, _Property_8a756da4fda1058f80ca49df1937f450_Out_0, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3);
            half _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2;
            Unity_Power_half(_Absolute_bbb7620451d39789a4abb5a43e40c6c1_Out_1, _Lerp_3600ec4e14b0ec8e956abc53133f1250_Out_3, _Power_71ad5b7cab8b348ead45c623e2311de3_Out_2);
            half _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2;
            Unity_Multiply_half(_Power_71ad5b7cab8b348ead45c623e2311de3_Out_2, 20, _Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2);
            half _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3;
            Unity_Clamp_half(_Multiply_70fde6b67f141a8abf73a5edeab0e286_Out_2, 0.05, 1.2, _Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3);
            half4 _Multiply_329a32a74e6e858696ca0b345435de30_Out_2;
            Unity_Multiply_half(_Multiply_139ff6c8e0a3fb8caa53be175ec38d28_Out_2, (_Clamp_6e6f3fcbd3c1538a947629b6025b80bc_Out_3.xxxx), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2);
            half4 _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3;
            Unity_Clamp_half4(_Multiply_329a32a74e6e858696ca0b345435de30_Out_2, half4(0, 0, 0, 0), _Multiply_329a32a74e6e858696ca0b345435de30_Out_2, _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3);
            half4 _Multiply_6d53084a8041428287e1516d476b8861_Out_2;
            Unity_Multiply_half((_Clamp_e2feb470baccd389bb1d3283786d0eae_Out_3.xxxx), _Clamp_ed0dfc1986298f8a8ea5d26b22a4c936_Out_3, _Multiply_6d53084a8041428287e1516d476b8861_Out_2);
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            surface.Emission = (_Multiply_6d53084a8041428287e1516d476b8861_Out_2.xyz);
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.VertexColor =                 input.color;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/LightingMetaPass.hlsl"

            ENDHLSL
        }
        Pass
        {
            // Name: <None>
            Tags
            {
                "LightMode" = "Universal2D"
            }

            // Render State
            Cull Back
        Blend One Zero
        ZTest LEqual
        ZWrite On

            // Debug
            // <None>

            // --------------------------------------------------
            // Pass

            HLSLPROGRAM

            // Pragmas
            #pragma target 2.0
        #pragma only_renderers gles gles3 glcore
        #pragma multi_compile_instancing
        #pragma vertex vert
        #pragma fragment frag

            // DotsInstancingOptions: <None>
            // HybridV1InjectedBuiltinProperties: <None>

            // Keywords
            // PassKeywords: <None>
            // GraphKeywords: <None>

            // Defines
            #define _NORMALMAP 1
            #define _NORMAL_DROPOFF_TS 1
            #define ATTRIBUTES_NEED_NORMAL
            #define ATTRIBUTES_NEED_TANGENT
            #define ATTRIBUTES_NEED_TEXCOORD0
            #define ATTRIBUTES_NEED_TEXCOORD3
            #define VARYINGS_NEED_NORMAL_WS
            #define VARYINGS_NEED_TEXCOORD0
            #define VARYINGS_NEED_TEXCOORD3
            #define FEATURES_GRAPH_VERTEX
            /* WARNING: $splice Could not find named fragment 'PassInstancing' */
            #define SHADERPASS SHADERPASS_2D
            /* WARNING: $splice Could not find named fragment 'DotsInstancingVars' */

            // Includes
            #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
        #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

            // --------------------------------------------------
            // Structs and Packing

            struct Attributes
        {
            float3 positionOS : POSITION;
            float3 normalOS : NORMAL;
            float4 tangentOS : TANGENT;
            float4 uv0 : TEXCOORD0;
            float4 uv3 : TEXCOORD3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : INSTANCEID_SEMANTIC;
            #endif
        };
        struct Varyings
        {
            float4 positionCS : SV_POSITION;
            float3 normalWS;
            float4 texCoord0;
            float4 texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };
        struct SurfaceDescriptionInputs
        {
            float3 WorldSpaceNormal;
            float4 uv0;
            float4 uv3;
            float3 TimeParameters;
        };
        struct VertexDescriptionInputs
        {
            float3 ObjectSpaceNormal;
            float3 ObjectSpaceTangent;
            float3 ObjectSpacePosition;
        };
        struct PackedVaryings
        {
            float4 positionCS : SV_POSITION;
            float3 interp0 : TEXCOORD0;
            float4 interp1 : TEXCOORD1;
            float4 interp2 : TEXCOORD2;
            #if UNITY_ANY_INSTANCING_ENABLED
            uint instanceID : CUSTOM_INSTANCE_ID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            uint stereoTargetEyeIndexAsBlendIdx0 : BLENDINDICES0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            uint stereoTargetEyeIndexAsRTArrayIdx : SV_RenderTargetArrayIndex;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            FRONT_FACE_TYPE cullFace : FRONT_FACE_SEMANTIC;
            #endif
        };

            PackedVaryings PackVaryings (Varyings input)
        {
            PackedVaryings output;
            output.positionCS = input.positionCS;
            output.interp0.xyz =  input.normalWS;
            output.interp1.xyzw =  input.texCoord0;
            output.interp2.xyzw =  input.texCoord3;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }
        Varyings UnpackVaryings (PackedVaryings input)
        {
            Varyings output;
            output.positionCS = input.positionCS;
            output.normalWS = input.interp0.xyz;
            output.texCoord0 = input.interp1.xyzw;
            output.texCoord3 = input.interp2.xyzw;
            #if UNITY_ANY_INSTANCING_ENABLED
            output.instanceID = input.instanceID;
            #endif
            #if (defined(UNITY_STEREO_MULTIVIEW_ENABLED)) || (defined(UNITY_STEREO_INSTANCING_ENABLED) && (defined(SHADER_API_GLES3) || defined(SHADER_API_GLCORE)))
            output.stereoTargetEyeIndexAsBlendIdx0 = input.stereoTargetEyeIndexAsBlendIdx0;
            #endif
            #if (defined(UNITY_STEREO_INSTANCING_ENABLED))
            output.stereoTargetEyeIndexAsRTArrayIdx = input.stereoTargetEyeIndexAsRTArrayIdx;
            #endif
            #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
            output.cullFace = input.cullFace;
            #endif
            return output;
        }

            // --------------------------------------------------
            // Graph

            // Graph Properties
            CBUFFER_START(UnityPerMaterial)
        half _GlobalTiling;
        half _UVVDirection1UDirection0;
        half2 _ColdLavaMainSpeed;
        half2 _MediumLavaMainSpeed;
        half2 _HotLavaMainSpeed;
        float4 _ColdLavaAlbedo_SM_TexelSize;
        half4 _ColdLavaAlbedoColor;
        half _ColdLavaAlbedoColorMultiply;
        half2 _ColdLavaTiling;
        half _ColdLavaSmoothness;
        float4 _ColdLavaNormal_TexelSize;
        half _ColdLavaNormalScale;
        float4 _ColdLavaMT_AO_H_EM_TexelSize;
        half _ColdLavaMetalic;
        half _ColdLavaAO;
        half _MediumLavaAngle;
        half _MediumLavaAngleFalloff;
        half _MediumLavaHeightBlendTreshold;
        half _MediumLavaHeightBlendStrenght;
        half4 _MediumLavaAlbedoColor;
        half _MediumLavaAlbedoColorMultiply;
        half2 _MediumLavaTiling;
        half _MediumLavaSmoothness;
        half _MediumLavaNormalScale;
        half _MediumLavaMetallic;
        half _MediumLavaAO;
        half _HotLavaAngle;
        half _HotLavaAngleFalloff;
        half _HotLavaHeightBlendTreshold;
        half _HotLavaHeightBlendStrenght;
        half4 _HotLavaAlbedoColor;
        half _HotLavaAlbedoColorMultiply;
        half2 _HotLavaTiling;
        half _HotLavaSmoothness;
        half _HotLavaNormalScale;
        half _HotLavaMetallic;
        half _HotLavaAO;
        half _ColdLavaFlowUVRefresSpeed;
        half _MediumLavaFlowUVRefreshSpeed;
        half _HotLavaFlowUVRefreshSpeed;
        half4 _EmissionColor;
        half _ColdLavaEmissionMaskIntensivity;
        half _ColdLavaEmissionMaskTreshold;
        half _MediumLavaEmissionMaskIntesivity;
        half _MediumLavaEmissionMaskTreshold;
        half _HotLavaEmissionMaskIntensivity;
        half _HotLavaEmissionMaskTreshold;
        float4 _Noise_TexelSize;
        half2 _NoiseTiling;
        half2 _NoiseSpeed;
        half _ColdLavaNoisePower;
        half _MediumLavaNoisePower;
        half _HotLavaNoisePower;
        CBUFFER_END

        // Object and Global properties
        SAMPLER(SamplerState_Linear_Repeat);
        TEXTURE2D(_ColdLavaAlbedo_SM);
        SAMPLER(sampler_ColdLavaAlbedo_SM);
        TEXTURE2D(_ColdLavaNormal);
        SAMPLER(sampler_ColdLavaNormal);
        TEXTURE2D(_ColdLavaMT_AO_H_EM);
        SAMPLER(sampler_ColdLavaMT_AO_H_EM);
        TEXTURE2D(_Noise);
        SAMPLER(sampler_Noise);

            // Graph Functions
            
        void Unity_Multiply_half(half2 A, half2 B, out half2 Out)
        {
            Out = A * B;
        }

        void Unity_Branch_half2(half Predicate, half2 True, half2 False, out half2 Out)
        {
            Out = Predicate ? True : False;
        }

        void Unity_Multiply_half(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_half(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Fraction_half(half In, out half Out)
        {
            Out = frac(In);
        }

        void Unity_Divide_half(half A, half B, out half Out)
        {
            Out = A / B;
        }

        void Unity_Add_half2(half2 A, half2 B, out half2 Out)
        {
            Out = A + B;
        }

        void Unity_Absolute_half(half In, out half Out)
        {
            Out = abs(In);
        }

        void Unity_Power_half(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Clamp_half(half In, half Min, half Max, out half Out)
        {
            Out = clamp(In, Min, Max);
        }

        void Unity_Lerp_half4(half4 A, half4 B, half4 T, out half4 Out)
        {
            Out = lerp(A, B, T);
        }

        void Unity_Multiply_half(half4 A, half4 B, out half4 Out)
        {
            Out = A * B;
        }

        void Unity_Combine_half(half R, half G, half B, half A, out half4 RGBA, out half3 RGB, out half2 RG)
        {
            RGBA = half4(R, G, B, A);
            RGB = half3(R, G, B);
            RG = half2(R, G);
        }

        void Unity_Blend_Overwrite_half4(half4 Base, half4 Blend, out half4 Out, half Opacity)
        {
            Out = lerp(Base, Blend, Opacity);
        }

        void Unity_OneMinus_half(half In, out half Out)
        {
            Out = 1 - In;
        }

        void Unity_Subtract_half(half A, half B, out half Out)
        {
            Out = A - B;
        }

        void Unity_Multiply_float(half A, half B, out half Out)
        {
            Out = A * B;
        }

        void Unity_Add_float(half A, half B, out half Out)
        {
            Out = A + B;
        }

        void Unity_Power_float(half A, half B, out half Out)
        {
            Out = pow(A, B);
        }

        void Unity_Saturate_float(half In, out half Out)
        {
            Out = saturate(In);
        }

        struct Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c
        {
        };

        void SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(float Vector1_3D7AF960, float Vector1_23CABB44, float Vector1_50A6BA5E, Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c IN, out float Blend_1)
        {
            float _Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0 = Vector1_3D7AF960;
            float _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0 = Vector1_23CABB44;
            float _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2;
            Unity_Multiply_float(_Property_804e86228598eb8b92ce6c007ac5fbc9_Out_0, _Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, _Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2);
            float _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2;
            Unity_Multiply_float(_Multiply_fb91b2b13771718bb4cb013bab2768ac_Out_2, 4, _Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2);
            float _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2;
            Unity_Multiply_float(_Property_9dc4e2ee5a613e8bacccce3aeee46c32_Out_0, 2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2);
            float _Add_4b45f8bab5909883a71690c307b567a2_Out_2;
            Unity_Add_float(_Multiply_aa534fa162410f8f935b21bd5d9c58ab_Out_2, _Multiply_283464c99bf1ac8789d1ff8ec1948e00_Out_2, _Add_4b45f8bab5909883a71690c307b567a2_Out_2);
            float _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0 = Vector1_50A6BA5E;
            float _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2;
            Unity_Power_float(_Add_4b45f8bab5909883a71690c307b567a2_Out_2, _Property_d01ca21e1ed88487b7c132aa64b85be9_Out_0, _Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2);
            float _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
            Unity_Saturate_float(_Power_376a71f1029b5d888cc4d93d7d8eb032_Out_2, _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1);
            Blend_1 = _Saturate_f8e2104d93edb487bdad4a3604d89b6b_Out_1;
        }

            // Graph Vertex
            struct VertexDescription
        {
            half3 Position;
            half3 Normal;
            half3 Tangent;
        };

        VertexDescription VertexDescriptionFunction(VertexDescriptionInputs IN)
        {
            VertexDescription description = (VertexDescription)0;
            description.Position = IN.ObjectSpacePosition;
            description.Normal = IN.ObjectSpaceNormal;
            description.Tangent = IN.ObjectSpaceTangent;
            return description;
        }

            // Graph Pixel
            struct SurfaceDescription
        {
            half3 BaseColor;
        };

        SurfaceDescription SurfaceDescriptionFunction(SurfaceDescriptionInputs IN)
        {
            SurfaceDescription surface = (SurfaceDescription)0;
            UnityTexture2D _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_9888f536495c078d8d13e4f93f260994_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0 = _ColdLavaMainSpeed;
            half2 _Property_398a18f78c81d28393067eae86b7f0c3_Out_0 = _ColdLavaTiling;
            half2 _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2;
            Unity_Multiply_half(_Property_877d7e28976d8f85bbc8ab7a485949eb_Out_0, _Property_398a18f78c81d28393067eae86b7f0c3_Out_0, _Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2);
            half4 _UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0 = IN.uv3;
            half2 _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2;
            Unity_Multiply_half(_Multiply_5c583e2d69d84f8fbe8b4780b3d8a63e_Out_2, (_UV_f4621e74c19ebd878a3c17e67ce708ea_Out_0.xy), _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2);
            half _Split_3275572cd890568f980cafc7c60f69f9_R_1 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[0];
            half _Split_3275572cd890568f980cafc7c60f69f9_G_2 = _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2[1];
            half _Split_3275572cd890568f980cafc7c60f69f9_B_3 = 0;
            half _Split_3275572cd890568f980cafc7c60f69f9_A_4 = 0;
            half2 _Vector2_5031750d94e60b848422357418f3bcaf_Out_0 = half2(_Split_3275572cd890568f980cafc7c60f69f9_G_2, _Split_3275572cd890568f980cafc7c60f69f9_R_1);
            half2 _Branch_80768e9b64c73389b3b24ff32967183d_Out_3;
            Unity_Branch_half2(_Property_9888f536495c078d8d13e4f93f260994_Out_0, _Multiply_6f7dc3b0a672db849d21ad93536b3fdb_Out_2, _Vector2_5031750d94e60b848422357418f3bcaf_Out_0, _Branch_80768e9b64c73389b3b24ff32967183d_Out_3);
            half _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0 = _ColdLavaFlowUVRefresSpeed;
            half _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_41fea7dea3f9c78fbf154c14e561037a_Out_0, _Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2);
            half _Add_92b72288f9b4b38db82936b75e8bb404_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 1, _Add_92b72288f9b4b38db82936b75e8bb404_Out_2);
            half _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1;
            Unity_Fraction_half(_Add_92b72288f9b4b38db82936b75e8bb404_Out_2, _Fraction_9838acefa02d1a818122b87a27ec8206_Out_1);
            half2 _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1.xx), _Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2);
            half _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0 = _GlobalTiling;
            half _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2;
            Unity_Divide_half(1, _Property_c92d9ef7ab07168d94ca1468d5742541_Out_0, _Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2);
            half4 _UV_a0446ab16407b5868a7f70150b9cf00f_Out_0 = IN.uv0;
            half2 _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2;
            Unity_Multiply_half(_Property_398a18f78c81d28393067eae86b7f0c3_Out_0, (_UV_a0446ab16407b5868a7f70150b9cf00f_Out_0.xy), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2);
            half2 _Multiply_e0cf556371b9008ab263486f791dff07_Out_2;
            Unity_Multiply_half((_Divide_1994eb9d3d14d4858efc4027a20aa109_Out_2.xx), _Multiply_ed84567dd41c4184ad608bb3b41b9759_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2);
            half2 _Add_d445560ce659218caecd0d785efe05e8_Out_2;
            Unity_Add_half2(_Multiply_c9f099059e20738880a26ac7ced1e57b_Out_2, _Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half4 _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_R_4 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.r;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_G_5 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.g;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_B_6 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.b;
            half _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_A_7 = _SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0.a;
            half _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2;
            Unity_Add_half(_Multiply_470ba181bab9cb8fa9ff9e14fa2cc574_Out_2, 0.5, _Add_70b1025eb8a6f28ea40920257d95c21a_Out_2);
            half _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1;
            Unity_Fraction_half(_Add_70b1025eb8a6f28ea40920257d95c21a_Out_2, _Fraction_d2d2249ee260908ebc6a039a24038686_Out_1);
            half2 _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2;
            Unity_Multiply_half(_Branch_80768e9b64c73389b3b24ff32967183d_Out_3, (_Fraction_d2d2249ee260908ebc6a039a24038686_Out_1.xx), _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2);
            half2 _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2;
            Unity_Add_half2(_Multiply_e0cf556371b9008ab263486f791dff07_Out_2, _Multiply_278ee8efb0bfeb80afe3f504cfc90631_Out_2, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half4 _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.tex, _Property_b7dfcca6c5206288b31b8e82b4e2e7c5_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_R_4 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.r;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_G_5 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.g;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_B_6 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.b;
            half _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_A_7 = _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0.a;
            half _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2;
            Unity_Add_half(_Fraction_9838acefa02d1a818122b87a27ec8206_Out_1, -0.5, _Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2);
            half _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2;
            Unity_Multiply_half(_Add_3ddb19ebf14eb0878d65e72de48dc419_Out_2, 2, _Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2);
            half _Absolute_dcde6658c2a5098cb211075361125443_Out_1;
            Unity_Absolute_half(_Multiply_d7bcc2eee2e3f4859b303a06641db1c5_Out_2, _Absolute_dcde6658c2a5098cb211075361125443_Out_1);
            UnityTexture2D _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_R_4 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.r;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_G_5 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.g;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.b;
            half _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_A_7 = _SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_RGBA_0.a;
            half _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2;
            Unity_Multiply_half(_SampleTexture2D_eb7ecc331d2cc483acd531da75a96524_B_6, 7, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2);
            half _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2;
            Unity_Power_half(_Absolute_dcde6658c2a5098cb211075361125443_Out_1, _Multiply_5d49388dc3e3af819a8e7bc10a09c52a_Out_2, _Power_aa8da26016dd7983bc036a1e82cb4051_Out_2);
            half _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3;
            Unity_Clamp_half(_Power_aa8da26016dd7983bc036a1e82cb4051_Out_2, 0, 1, _Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3);
            half4 _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_689ba69e2ff7bd888eeaec5d40bddbf7_RGBA_0, _SampleTexture2D_a5d6d50c4eb30382b6dbb4e680e74adb_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3);
            half4 _Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0 = _ColdLavaAlbedoColor;
            half _Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0 = _ColdLavaAlbedoColorMultiply;
            half4 _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2;
            Unity_Multiply_half(_Property_81ee9e99fc7a068889b6ba7bdcb202a7_Out_0, (_Property_ae430ed70f674188b5e8b6cf5ec101a9_Out_0.xxxx), _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2);
            half4 _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2;
            Unity_Multiply_half(_Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3, _Multiply_04c020203bf21887b1adae9c4cc6574a_Out_2, _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2);
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[0];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[1];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[2];
            half _Split_5d0c2ad1f86f8d8e8438ed908f41a258_A_4 = _Multiply_76086b1955ddc78db0d1105cb61c546f_Out_2[3];
            half _Split_336849396de78d88909e4ad054a44d6c_R_1 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[0];
            half _Split_336849396de78d88909e4ad054a44d6c_G_2 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[1];
            half _Split_336849396de78d88909e4ad054a44d6c_B_3 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[2];
            half _Split_336849396de78d88909e4ad054a44d6c_A_4 = _Lerp_aab81b541d9d3b8280e5e9a669be7666_Out_3[3];
            half _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0 = _ColdLavaSmoothness;
            half _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2;
            Unity_Multiply_half(_Split_336849396de78d88909e4ad054a44d6c_A_4, _Property_23e147d5684b0c89a7767d3573b12dbd_Out_0, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2);
            half4 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4;
            half3 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5;
            half2 _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6;
            Unity_Combine_half(_Split_5d0c2ad1f86f8d8e8438ed908f41a258_R_1, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_G_2, _Split_5d0c2ad1f86f8d8e8438ed908f41a258_B_3, _Multiply_3d6654a0a154e088b1b7671fab414640_Out_2, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RGB_5, _Combine_0a9a9298a9464d899f2f2fafc625fec8_RG_6);
            UnityTexture2D _Property_a2073034a5e61e8faeeada8151652a19_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_8774276f674d8a8598020af3d4e74f0f_Out_0 = _MediumLavaMainSpeed;
            half2 _Property_4a73de4bbed00585a919d64e1b181601_Out_0 = _MediumLavaTiling;
            half2 _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2;
            Unity_Multiply_half(_Property_8774276f674d8a8598020af3d4e74f0f_Out_0, _Property_4a73de4bbed00585a919d64e1b181601_Out_0, _Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2);
            half4 _UV_155f0df72125e686952a429f2ffd5986_Out_0 = IN.uv3;
            half2 _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2;
            Unity_Multiply_half(_Multiply_fe4a93ea96d39d868ea4f0d5efeb112c_Out_2, (_UV_155f0df72125e686952a429f2ffd5986_Out_0.xy), _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2);
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[0];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2 = _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2[1];
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_B_3 = 0;
            half _Split_05bdaffcb94c1a8b8907cb9ae0088e28_A_4 = 0;
            half2 _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0 = half2(_Split_05bdaffcb94c1a8b8907cb9ae0088e28_G_2, _Split_05bdaffcb94c1a8b8907cb9ae0088e28_R_1);
            half2 _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3;
            Unity_Branch_half2(_Property_ac1c529019c9d1869d9bfe9bf1104042_Out_0, _Multiply_495eeee31c9fc988ae6d3bfad7de23fb_Out_2, _Vector2_84709b62ed0ec186bd8b6072a206cdbb_Out_0, _Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3);
            half _Property_a13ee271968db284868bea327ce6cf48_Out_0 = _MediumLavaFlowUVRefreshSpeed;
            half _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a13ee271968db284868bea327ce6cf48_Out_0, _Multiply_23d149ee58240383a51fc47bd5080b38_Out_2);
            half _Add_876c6899ecc5ff8d90391762631227de_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 1, _Add_876c6899ecc5ff8d90391762631227de_Out_2);
            half _Fraction_41693b4717b08a83ac836421a5f95130_Out_1;
            Unity_Fraction_half(_Add_876c6899ecc5ff8d90391762631227de_Out_2, _Fraction_41693b4717b08a83ac836421a5f95130_Out_1);
            half2 _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_41693b4717b08a83ac836421a5f95130_Out_1.xx), _Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2);
            half _Property_4122249ff176f58da9d0dbace2f883d0_Out_0 = _GlobalTiling;
            half _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2;
            Unity_Divide_half(1, _Property_4122249ff176f58da9d0dbace2f883d0_Out_0, _Divide_8ff972a4c7eb9b839c81321397365f61_Out_2);
            half4 _UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0 = IN.uv0;
            half2 _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2;
            Unity_Multiply_half(_Property_4a73de4bbed00585a919d64e1b181601_Out_0, (_UV_2506007e9678e78d9a828d0c6d6d7a6d_Out_0.xy), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2);
            half2 _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2;
            Unity_Multiply_half((_Divide_8ff972a4c7eb9b839c81321397365f61_Out_2.xx), _Multiply_70e030c7414b0a80aeeebb50f2b469c7_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2);
            half2 _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2;
            Unity_Add_half2(_Multiply_d677aa2bd65e8c8380cc99b91cd2c450_Out_2, _Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half4 _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_R_4 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.r;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_G_5 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.g;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_B_6 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.b;
            half _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_A_7 = _SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0.a;
            half _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2;
            Unity_Add_half(_Multiply_23d149ee58240383a51fc47bd5080b38_Out_2, 0.5, _Add_cb630e934cbc9685bfa7152c7efc895b_Out_2);
            half _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1;
            Unity_Fraction_half(_Add_cb630e934cbc9685bfa7152c7efc895b_Out_2, _Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1);
            half2 _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2;
            Unity_Multiply_half(_Branch_7f6b134feaed2883a01e3679b96dd6bd_Out_3, (_Fraction_53e447b055d9058b81ec06c6e9fd2751_Out_1.xx), _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2);
            half2 _Add_5b1693e6737d608d8875d9528a4320b8_Out_2;
            Unity_Add_half2(_Multiply_5a2803205dbaf0849d4f2b64006614c0_Out_2, _Multiply_04dd3871b39faf80b01741bc1936975c_Out_2, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half4 _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0 = SAMPLE_TEXTURE2D(_Property_a2073034a5e61e8faeeada8151652a19_Out_0.tex, _Property_a2073034a5e61e8faeeada8151652a19_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_R_4 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.r;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_G_5 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.g;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_B_6 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.b;
            half _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_A_7 = _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0.a;
            half _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2;
            Unity_Add_half(_Fraction_41693b4717b08a83ac836421a5f95130_Out_1, -0.5, _Add_078e4ea9683ad287b56d5fba77b52f19_Out_2);
            half _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2;
            Unity_Multiply_half(_Add_078e4ea9683ad287b56d5fba77b52f19_Out_2, 2, _Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2);
            half _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1;
            Unity_Absolute_half(_Multiply_5c1317f91a58a385b34f1663a9bf1484_Out_2, _Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1);
            UnityTexture2D _Property_f025f06987a6978aa280ff284290ece9_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_R_4 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.r;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_G_5 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.g;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.b;
            half _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_A_7 = _SampleTexture2D_a4f1e618fc2551888bae362756d44d66_RGBA_0.a;
            half _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2;
            Unity_Multiply_half(_SampleTexture2D_a4f1e618fc2551888bae362756d44d66_B_6, 7, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2);
            half _Power_546f815f47c24a8280ccc223b7c8121a_Out_2;
            Unity_Power_half(_Absolute_8e8928ed0e4ab886a977ad493e353a5e_Out_1, _Multiply_92455d05f56c8485948c321f936d3dc7_Out_2, _Power_546f815f47c24a8280ccc223b7c8121a_Out_2);
            half _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3;
            Unity_Clamp_half(_Power_546f815f47c24a8280ccc223b7c8121a_Out_2, 0, 1, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Blend_974beedeef1c3582be67dcaec325dad4_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_81669c6dd85bd2879473510bc4f9109e_RGBA_0, _SampleTexture2D_4a602e59b5c2cd82b26c6532c5dec4bb_RGBA_0, _Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3);
            half4 _Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0 = _MediumLavaAlbedoColor;
            half _Property_1761dd4732c7b3858814a0c4bc252900_Out_0 = _MediumLavaAlbedoColorMultiply;
            half4 _Multiply_c98351861904f487ac8fc5401441358e_Out_2;
            Unity_Multiply_half(_Property_a9e1aded62977f8ca3f9c46207eadfe8_Out_0, (_Property_1761dd4732c7b3858814a0c4bc252900_Out_0.xxxx), _Multiply_c98351861904f487ac8fc5401441358e_Out_2);
            half4 _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2;
            Unity_Multiply_half(_Blend_974beedeef1c3582be67dcaec325dad4_Out_2, _Multiply_c98351861904f487ac8fc5401441358e_Out_2, _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2);
            half _Split_c431e838bb4f458084245282a6fc6137_R_1 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[0];
            half _Split_c431e838bb4f458084245282a6fc6137_G_2 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[1];
            half _Split_c431e838bb4f458084245282a6fc6137_B_3 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[2];
            half _Split_c431e838bb4f458084245282a6fc6137_A_4 = _Multiply_16971dced2f6f384b7d2d65006f03b46_Out_2[3];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_R_1 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[0];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_G_2 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[1];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_B_3 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[2];
            half _Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4 = _Blend_974beedeef1c3582be67dcaec325dad4_Out_2[3];
            half _Property_19729c3fad203984b63630ce8edabf9d_Out_0 = _MediumLavaSmoothness;
            half _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2;
            Unity_Multiply_half(_Split_77f8ec7707a1888f8f5bb2f3ee9ef36e_A_4, _Property_19729c3fad203984b63630ce8edabf9d_Out_0, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2);
            half4 _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4;
            half3 _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5;
            half2 _Combine_8579144aa55f898b914c649ff6fd204b_RG_6;
            Unity_Combine_half(_Split_c431e838bb4f458084245282a6fc6137_R_1, _Split_c431e838bb4f458084245282a6fc6137_G_2, _Split_c431e838bb4f458084245282a6fc6137_B_3, _Multiply_25537a09620d8e84a159350d0570e3b8_Out_2, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGB_5, _Combine_8579144aa55f898b914c649ff6fd204b_RG_6);
            half4 _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_d445560ce659218caecd0d785efe05e8_Out_2);
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_R_4 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.r;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_G_5 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.g;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_B_6 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.b;
            half _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_A_7 = _SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0.a;
            half4 _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0 = SAMPLE_TEXTURE2D(_Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.tex, _Property_431fc4b7bdb08186aa2ebdcdd9ddd27f_Out_0.samplerstate, _Add_e10ca8ac66cfc08297c1281f988ee064_Out_2);
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_R_4 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.r;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_G_5 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.g;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_B_6 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.b;
            half _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_A_7 = _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0.a;
            half4 _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_f6eb47aeeabe878b991235bf880d85a1_RGBA_0, _SampleTexture2D_e2844d016376b18d88bde96b6ed83eb0_RGBA_0, (_Clamp_cc3866cc0b18f487a58a420cfdc6b300_Out_3.xxxx), _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3);
            half _Split_94cd24e718391e889d186d14735a5f81_R_1 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[0];
            half _Split_94cd24e718391e889d186d14735a5f81_G_2 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[1];
            half _Split_94cd24e718391e889d186d14735a5f81_B_3 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[2];
            half _Split_94cd24e718391e889d186d14735a5f81_A_4 = _Lerp_73c98c320ef744838327c8caa26d91fb_Out_3[3];
            half _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1;
            Unity_OneMinus_half(_Split_94cd24e718391e889d186d14735a5f81_B_3, _OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1);
            half _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0 = _MediumLavaHeightBlendTreshold;
            half _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2;
            Unity_Power_half(_OneMinus_d30fd2a07aa5ef8c954841430c669c6f_Out_1, _Property_b4d8b44b98ac218ab5b64e9509780c1f_Out_0, _Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2);
            half _Split_e8815c5687c0c188b222e57b486e0e5d_R_1 = IN.WorldSpaceNormal[0];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_G_2 = IN.WorldSpaceNormal[1];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_B_3 = IN.WorldSpaceNormal[2];
            half _Split_e8815c5687c0c188b222e57b486e0e5d_A_4 = 0;
            half _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1;
            Unity_Absolute_half(_Split_e8815c5687c0c188b222e57b486e0e5d_G_2, _Absolute_af2e8d067a75a385bc8da51b27457800_Out_1);
            half _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3;
            Unity_Clamp_half(_Absolute_af2e8d067a75a385bc8da51b27457800_Out_1, 0, 1, _Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3);
            half _Property_f6e7c7b7064d56849dcc327504a5af65_Out_0 = _MediumLavaAngle;
            half _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2;
            Unity_Divide_half(_Property_f6e7c7b7064d56849dcc327504a5af65_Out_0, 45, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2);
            half _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1;
            Unity_OneMinus_half(_Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1);
            half _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2;
            Unity_Subtract_half(_Clamp_6461e1158ec9fc888d6226acfef2903e_Out_3, _OneMinus_51d362fe4abf8088a515cede6efdeae6_Out_1, _Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2);
            half _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3;
            Unity_Clamp_half(_Subtract_df7e03fdbee60f829e8414ab56aebd63_Out_2, 0, 2, _Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3);
            half _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2;
            Unity_Divide_half(1, _Divide_9e59ae67ac09cc85ac256679eb8a92df_Out_2, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2);
            half _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2;
            Unity_Multiply_half(_Clamp_93c899dfdaf23a869025d2fe37cbc17b_Out_3, _Divide_99e399b80aa3d78c9b8289ae31f1e13c_Out_2, _Multiply_7079e8acdf5057888843275dbbdb199c_Out_2);
            half _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3;
            Unity_Clamp_half(_Multiply_7079e8acdf5057888843275dbbdb199c_Out_2, 0, 1, _Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3);
            half _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1;
            Unity_OneMinus_half(_Clamp_6b3d8b850a8e5188b8a540162210198d_Out_3, _OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1);
            half _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1;
            Unity_Absolute_half(_OneMinus_464327f0009fbb8fa990d2cb702b2da9_Out_1, _Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1);
            half _Property_f6203e15e471e481b8369ee14c1cf745_Out_0 = _MediumLavaAngleFalloff;
            half _Power_1bc1b18487206481a5ce3274075c24a1_Out_2;
            Unity_Power_half(_Absolute_7b20625d60e2458b89c9ca794a5039a5_Out_1, _Property_f6203e15e471e481b8369ee14c1cf745_Out_0, _Power_1bc1b18487206481a5ce3274075c24a1_Out_2);
            half _Clamp_08c896054837bb88bc0374bd536ee024_Out_3;
            Unity_Clamp_half(_Power_1bc1b18487206481a5ce3274075c24a1_Out_2, 0, 1, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3);
            half _Property_ebcd48be1c3af28cb628925671461e5e_Out_0 = _MediumLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b;
            float _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_8ccb8ba2dc154d8f8d23bb151fc3f936_Out_2, _Clamp_08c896054837bb88bc0374bd536ee024_Out_3, _Property_ebcd48be1c3af28cb628925671461e5e_Out_0, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b, _HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1);
            half4 _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3;
            Unity_Lerp_half4(_Combine_0a9a9298a9464d899f2f2fafc625fec8_RGBA_4, _Combine_8579144aa55f898b914c649ff6fd204b_RGBA_4, (_HeightBlendSplat_9e03d1eca2c3de809ba034bf4abd6d2b_Blend_1.xxxx), _Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3);
            UnityTexture2D _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaAlbedo_SM);
            half _Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0 = _UVVDirection1UDirection0;
            half2 _Property_1d9925ecc19284849bb9705ef6f8c824_Out_0 = _HotLavaMainSpeed;
            half2 _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0 = _HotLavaTiling;
            half2 _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2;
            Unity_Multiply_half(_Property_1d9925ecc19284849bb9705ef6f8c824_Out_0, _Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, _Multiply_374e1fbcd041348c9e1605af3643e846_Out_2);
            half4 _UV_8773728161068380a52b4e181688e112_Out_0 = IN.uv3;
            half2 _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2;
            Unity_Multiply_half(_Multiply_374e1fbcd041348c9e1605af3643e846_Out_2, (_UV_8773728161068380a52b4e181688e112_Out_0.xy), _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2);
            half _Split_1be1e5da5681728eb25e1e0354a56de2_R_1 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[0];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_G_2 = _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2[1];
            half _Split_1be1e5da5681728eb25e1e0354a56de2_B_3 = 0;
            half _Split_1be1e5da5681728eb25e1e0354a56de2_A_4 = 0;
            half2 _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0 = half2(_Split_1be1e5da5681728eb25e1e0354a56de2_G_2, _Split_1be1e5da5681728eb25e1e0354a56de2_R_1);
            half2 _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3;
            Unity_Branch_half2(_Property_b7109f4bbd38b98d9cbae4fba5543a46_Out_0, _Multiply_fc100cbda65dac85b9b9027ad12941ed_Out_2, _Vector2_3d86aae3a5d3f48ba40ca1f53e71acb7_Out_0, _Branch_02ce2610bffd338dbded26b6ea66c568_Out_3);
            half _Property_a29d4264dadc878687c59348530e0cb7_Out_0 = _HotLavaFlowUVRefreshSpeed;
            half _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2;
            Unity_Multiply_half(IN.TimeParameters.x, _Property_a29d4264dadc878687c59348530e0cb7_Out_0, _Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2);
            half _Add_e75161453035ef83a5b9148c4fa24c85_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 1, _Add_e75161453035ef83a5b9148c4fa24c85_Out_2);
            half _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1;
            Unity_Fraction_half(_Add_e75161453035ef83a5b9148c4fa24c85_Out_2, _Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1);
            half2 _Multiply_019f6e249bc19989a973145ebde380e1_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1.xx), _Multiply_019f6e249bc19989a973145ebde380e1_Out_2);
            half _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0 = _GlobalTiling;
            half _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2;
            Unity_Divide_half(1, _Property_7e9d4fdee6095e8cb4916776509ca03c_Out_0, _Divide_02af391a0750818ebddd34c84d2ba47c_Out_2);
            half4 _UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0 = IN.uv0;
            half2 _Multiply_50905267b40ca083b044feec615f813b_Out_2;
            Unity_Multiply_half(_Property_1c3d54765bc6a585ac8690ff98875af6_Out_0, (_UV_88bd34a9ea85a58eaa101897a97eeb96_Out_0.xy), _Multiply_50905267b40ca083b044feec615f813b_Out_2);
            half2 _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2;
            Unity_Multiply_half((_Divide_02af391a0750818ebddd34c84d2ba47c_Out_2.xx), _Multiply_50905267b40ca083b044feec615f813b_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2);
            half2 _Add_67676bdc2d71838aa03add93a1fcd582_Out_2;
            Unity_Add_half2(_Multiply_019f6e249bc19989a973145ebde380e1_Out_2, _Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half4 _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_R_4 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.r;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_G_5 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.g;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_B_6 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.b;
            half _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_A_7 = _SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0.a;
            half _Add_2de62508721fd88e99b5c9b5be747fde_Out_2;
            Unity_Add_half(_Multiply_16610d661d5a6d80af8fd2981d1e9bff_Out_2, 0.5, _Add_2de62508721fd88e99b5c9b5be747fde_Out_2);
            half _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1;
            Unity_Fraction_half(_Add_2de62508721fd88e99b5c9b5be747fde_Out_2, _Fraction_0c3370c032db598eba83b2e087064b6d_Out_1);
            half2 _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2;
            Unity_Multiply_half(_Branch_02ce2610bffd338dbded26b6ea66c568_Out_3, (_Fraction_0c3370c032db598eba83b2e087064b6d_Out_1.xx), _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2);
            half2 _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2;
            Unity_Add_half2(_Multiply_9f387ef657d4498e9c8e57dd3a8dd117_Out_2, _Multiply_c1fdaed91a9f6b83b8d2aae1d0a34551_Out_2, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half4 _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0 = SAMPLE_TEXTURE2D(_Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.tex, _Property_20fd54d40336cd88847d5d8d097bdeb7_Out_0.samplerstate, _Add_36b0ecfdc70c2a89860e8ffcd342f158_Out_2);
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_R_4 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.r;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_G_5 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.g;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_B_6 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.b;
            half _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_A_7 = _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0.a;
            half _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2;
            Unity_Add_half(_Fraction_a9e03f4c9a57c38e8d33cfa1842b86e2_Out_1, -0.5, _Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2);
            half _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2;
            Unity_Multiply_half(_Add_65f2d60b944d3e80ac12d4517bbd021b_Out_2, 2, _Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2);
            half _Absolute_abccf7112257d18a9a55c71637619c70_Out_1;
            Unity_Absolute_half(_Multiply_3e21e0051eeddd8db9798e5cce43fb03_Out_2, _Absolute_abccf7112257d18a9a55c71637619c70_Out_1);
            UnityTexture2D _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0 = UnityBuildTexture2DStructNoScale(_ColdLavaMT_AO_H_EM);
            half4 _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0 = SAMPLE_TEXTURE2D(_Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.tex, _Property_6796bac247ac4d84b1814019e0c9ebf6_Out_0.samplerstate, _Add_67676bdc2d71838aa03add93a1fcd582_Out_2);
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_R_4 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.r;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_G_5 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.g;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.b;
            half _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_A_7 = _SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_RGBA_0.a;
            half _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2;
            Unity_Multiply_half(_SampleTexture2D_56651c11093b4887a9fb0cd6a28a773f_B_6, 7, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2);
            half _Power_79400efa1aecb18cb6478b182addffc5_Out_2;
            Unity_Power_half(_Absolute_abccf7112257d18a9a55c71637619c70_Out_1, _Multiply_3ba04eff8212b9878d78501ae72c7189_Out_2, _Power_79400efa1aecb18cb6478b182addffc5_Out_2);
            half _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3;
            Unity_Clamp_half(_Power_79400efa1aecb18cb6478b182addffc5_Out_2, 0, 1, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2;
            Unity_Blend_Overwrite_half4(_SampleTexture2D_8d83fa40657c878a8b6151bfdeeecc4e_RGBA_0, _SampleTexture2D_d57511f844f62d8e8e5df4262ed4e29c_RGBA_0, _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Clamp_44546232e7f62087a73f2c7998c0c775_Out_3);
            half4 _Property_f426055a212d488b92e0721ad75eea0d_Out_0 = _HotLavaAlbedoColor;
            half _Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0 = _HotLavaAlbedoColorMultiply;
            half4 _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2;
            Unity_Multiply_half(_Property_f426055a212d488b92e0721ad75eea0d_Out_0, (_Property_bc8ee8b7cbd16f83aa2d51aada2d0a51_Out_0.xxxx), _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2);
            half4 _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2;
            Unity_Multiply_half(_Blend_28e5fbdffc085286870d544e5e8e6627_Out_2, _Multiply_61a826a1dba47d8dbb0510fc1fd2dee2_Out_2, _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2);
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[0];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[1];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[2];
            half _Split_2a6cfd0eedbc128d83eff83d5df85b09_A_4 = _Multiply_f73b703611c2ee8ea1b712546ec1fdc8_Out_2[3];
            half _Split_87213490114de18bbc7496f97751b00a_R_1 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[0];
            half _Split_87213490114de18bbc7496f97751b00a_G_2 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[1];
            half _Split_87213490114de18bbc7496f97751b00a_B_3 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[2];
            half _Split_87213490114de18bbc7496f97751b00a_A_4 = _Blend_28e5fbdffc085286870d544e5e8e6627_Out_2[3];
            half _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0 = _HotLavaSmoothness;
            half _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2;
            Unity_Multiply_half(_Split_87213490114de18bbc7496f97751b00a_A_4, _Property_94efdcfe3a5a998bb3b399b34d6110a5_Out_0, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2);
            half4 _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4;
            half3 _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5;
            half2 _Combine_1bd6341933599685bd9dfbd647433b28_RG_6;
            Unity_Combine_half(_Split_2a6cfd0eedbc128d83eff83d5df85b09_R_1, _Split_2a6cfd0eedbc128d83eff83d5df85b09_G_2, _Split_2a6cfd0eedbc128d83eff83d5df85b09_B_3, _Multiply_1e7303fa97e339859abc539fdacc406f_Out_2, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, _Combine_1bd6341933599685bd9dfbd647433b28_RGB_5, _Combine_1bd6341933599685bd9dfbd647433b28_RG_6);
            half4 _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_ab2fbb5d62416f8a8395247b1abd0edb_Out_2);
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_R_4 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.r;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_G_5 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.g;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_B_6 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.b;
            half _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_A_7 = _SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0.a;
            half4 _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0 = SAMPLE_TEXTURE2D(_Property_f025f06987a6978aa280ff284290ece9_Out_0.tex, _Property_f025f06987a6978aa280ff284290ece9_Out_0.samplerstate, _Add_5b1693e6737d608d8875d9528a4320b8_Out_2);
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_R_4 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.r;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_G_5 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.g;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_B_6 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.b;
            half _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_A_7 = _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0.a;
            half4 _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3;
            Unity_Lerp_half4(_SampleTexture2D_78273dcdf5689f80952c2a7be9451994_RGBA_0, _SampleTexture2D_19b94184e9c8e288b96bc669f7cbed52_RGBA_0, (_Clamp_9c19c9dec9f754859e341e841dd7ee65_Out_3.xxxx), _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3);
            half _Split_503d896cedc1148aa1567e911ed3614b_R_1 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[0];
            half _Split_503d896cedc1148aa1567e911ed3614b_G_2 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[1];
            half _Split_503d896cedc1148aa1567e911ed3614b_B_3 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[2];
            half _Split_503d896cedc1148aa1567e911ed3614b_A_4 = _Lerp_d9190fa5aad64387a59eae8b234267b1_Out_3[3];
            half _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1;
            Unity_OneMinus_half(_Split_503d896cedc1148aa1567e911ed3614b_B_3, _OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1);
            half _Property_54766320db4a7f848cb65321bce3a68e_Out_0 = _HotLavaHeightBlendTreshold;
            half _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2;
            Unity_Power_half(_OneMinus_8cea3f85f066ff8fbbd54f2eff89ae58_Out_1, _Property_54766320db4a7f848cb65321bce3a68e_Out_0, _Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2);
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_R_1 = IN.WorldSpaceNormal[0];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2 = IN.WorldSpaceNormal[1];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_B_3 = IN.WorldSpaceNormal[2];
            half _Split_b9e7b4a6b4b5e58aa502d4b6d169b792_A_4 = 0;
            half _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1;
            Unity_Absolute_half(_Split_b9e7b4a6b4b5e58aa502d4b6d169b792_G_2, _Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1);
            half _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3;
            Unity_Clamp_half(_Absolute_45576b7b972f7d8a82c937564b72f70b_Out_1, 0, 1, _Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3);
            half _Property_6975119070a7eb84950e7da691463776_Out_0 = _HotLavaAngle;
            half _Divide_86811ef0d01a1581b082fc982daa687c_Out_2;
            Unity_Divide_half(_Property_6975119070a7eb84950e7da691463776_Out_0, 45, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2);
            half _OneMinus_a7c4799546af71898266d38a4354b568_Out_1;
            Unity_OneMinus_half(_Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1);
            half _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2;
            Unity_Subtract_half(_Clamp_c2714660b22e6a86a3d1f402132434f1_Out_3, _OneMinus_a7c4799546af71898266d38a4354b568_Out_1, _Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2);
            half _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3;
            Unity_Clamp_half(_Subtract_e487cff09d7e158e8f03eeef153fbe43_Out_2, 0, 2, _Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3);
            half _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2;
            Unity_Divide_half(1, _Divide_86811ef0d01a1581b082fc982daa687c_Out_2, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2);
            half _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2;
            Unity_Multiply_half(_Clamp_001f24015dba2889ab4be2cbfefbc81f_Out_3, _Divide_d1e47cd61e61c487be74fb1989b0bbb8_Out_2, _Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2);
            half _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3;
            Unity_Clamp_half(_Multiply_a6dece8284b0dd89b3ad78540f989a2b_Out_2, 0, 1, _Clamp_69414a1448338e8d9fc890185b317bc6_Out_3);
            half _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1;
            Unity_OneMinus_half(_Clamp_69414a1448338e8d9fc890185b317bc6_Out_3, _OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1);
            half _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1;
            Unity_Absolute_half(_OneMinus_b3ab3b092be02a8dbebaa86bf00ea51d_Out_1, _Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1);
            half _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0 = _HotLavaAngleFalloff;
            half _Power_0c67a131ce19048caeed8c043e033fb9_Out_2;
            Unity_Power_half(_Absolute_6cd65b456694da879b288dd44c7c10c6_Out_1, _Property_f5a63fda0810468082350dfa23ec6bf3_Out_0, _Power_0c67a131ce19048caeed8c043e033fb9_Out_2);
            half _Clamp_c8349812bf696286b9429cd182d4670a_Out_3;
            Unity_Clamp_half(_Power_0c67a131ce19048caeed8c043e033fb9_Out_2, 0, 1, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3);
            half _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0 = _HotLavaHeightBlendStrenght;
            Bindings_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718;
            float _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1;
            SG_HeightBlendSplat_e80db2c21dcba0342900d4e58bcd040c(_Power_ca351c1cbba04987a097e6ea5a87eeb8_Out_2, _Clamp_c8349812bf696286b9429cd182d4670a_Out_3, _Property_598298ccdedaea81a6c7006cdf26d0b3_Out_0, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718, _HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1);
            half4 _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3;
            Unity_Lerp_half4(_Lerp_60e101ecb3d9ae8a8c8208e24fcdc145_Out_3, _Combine_1bd6341933599685bd9dfbd647433b28_RGBA_4, (_HeightBlendSplat_83e736f68e1ff280996cf462b7ac2718_Blend_1.xxxx), _Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3);
            surface.BaseColor = (_Lerp_a4738e050e0d2685b5f9698d1e2bb098_Out_3.xyz);
            return surface;
        }

            // --------------------------------------------------
            // Build Graph Inputs

            VertexDescriptionInputs BuildVertexDescriptionInputs(Attributes input)
        {
            VertexDescriptionInputs output;
            ZERO_INITIALIZE(VertexDescriptionInputs, output);

            output.ObjectSpaceNormal =           input.normalOS;
            output.ObjectSpaceTangent =          input.tangentOS.xyz;
            output.ObjectSpacePosition =         input.positionOS;

            return output;
        }
            SurfaceDescriptionInputs BuildSurfaceDescriptionInputs(Varyings input)
        {
            SurfaceDescriptionInputs output;
            ZERO_INITIALIZE(SurfaceDescriptionInputs, output);

            // must use interpolated tangent, bitangent and normal before they are normalized in the pixel shader.
            float3 unnormalizedNormalWS = input.normalWS;
            const float renormFactor = 1.0 / length(unnormalizedNormalWS);


            output.WorldSpaceNormal =            renormFactor*input.normalWS.xyz;      // we want a unit length Normal Vector node in shader graph


            output.uv0 =                         input.texCoord0;
            output.uv3 =                         input.texCoord3;
            output.TimeParameters =              _TimeParameters.xyz; // This is mainly for LW as HD overwrite this value
        #if defined(SHADER_STAGE_FRAGMENT) && defined(VARYINGS_NEED_CULLFACE)
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN output.FaceSign =                    IS_FRONT_VFACE(input.cullFace, true, false);
        #else
        #define BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN
        #endif
        #undef BUILD_SURFACE_DESCRIPTION_INPUTS_OUTPUT_FACESIGN

            return output;
        }

            // --------------------------------------------------
            // Main

            #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/Varyings.hlsl"
        #include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/PBR2DPass.hlsl"

            ENDHLSL
        }
    }
    //CustomEditorForRenderPipeline "ShaderGraph.PBRMasterGUI" "UnityEngine.Rendering.Universal.UniversalRenderPipelineAsset"
    FallBack "Hidden/Shader Graph/FallbackError"
}