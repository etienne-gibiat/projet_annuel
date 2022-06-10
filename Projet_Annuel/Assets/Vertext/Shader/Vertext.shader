Shader "Onager/Vertext"
{
    Properties
    {
        [Enum(Onager.Vertext.DataNames)]DisplayedData("Displayed Data", Float) = 0
        [Enum(Onager.Vertext.ChannelNames)]Channel("Channel", Float) = 0
        Bias("Bias", Range(-2,2)) = 1
        [Toggle(ABSVAL)]ABSVAL("Absolute Value", Float) = 0

        Tex("Tex", 2D) = "black" {}
        [Toggle(DIGITS)]DIGITS("Show Digits", Float) = 0
        DigitColor("Digit Color", Color) = (1,1,1,1)
        Separation("Separation", Range(0,1)) = 0
        Scale("Scale", Range(0, 1)) = 0.4
        [IntRange]Precision("Precision", Range(1, 10)) = 2
        [Toggle(DO_ROUND)]DO_ROUND("Round", Float) = 1

        WireframeColor ("Wireframe Color", Color) = (0, 0, 0)
		WireframeSmoothing ("Wireframe Smoothing", Range(0, 10)) = 0.75
		WireframeThickness ("Wireframe Thickness", Range(0, 10)) = 0.5
        [Toggle(WIREFRAME)]WIREFRAME("Show Wireframe", Float) = 0
    }
    SubShader
    {
        Name "Vertext"
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma target 5.0
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag

            #pragma shader_feature ABSVAL
            #pragma shader_feature WIREFRAME
            #pragma shader_feature DIGITS
            #pragma shader_feature DO_ROUND

			struct appdata
			{
				float4 vertex  : POSITION;
				float4 color   : COLOR;
				float4 normal  : NORMAL;
				float4 tangent : TANGENT;
				float4 uv0     : TEXCOORD0;
				float4 uv1     : TEXCOORD1;
				float4 uv2     : TEXCOORD2;
				float4 uv3     : TEXCOORD3;
				float4 uv4     : TEXCOORD4;
				float4 uv5     : TEXCOORD5;
				float4 uv6     : TEXCOORD6;
				float4 uv7     : TEXCOORD7;
				uint   id      : SV_VertexID;
			};

			struct varyings
			{
				float4 vertex     : SV_POSITION;
				float4 data       : TEXCOORD0;
				float2 baryCoords : TEXCOORD1;
			}; 

            float4 GetCustomData(appdata v)
            {
                return 6.283185307179586;
            }
            #include "DigitRenderer.hlsl"

            ENDCG
        }
    }

    CustomEditor "Onager.Vertext.VertextShaderEditor"
    FallBack "Hidden/InternalErrorShader"
}