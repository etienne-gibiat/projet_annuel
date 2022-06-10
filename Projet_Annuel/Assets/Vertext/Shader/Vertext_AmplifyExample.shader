// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Onager/Amplify/Vertext"
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

		[ASEEnd]_CustomValue("CustomValue", Float) = 3.14

	}

	SubShader
	{
		LOD 0

		Tags { "RenderType"="Opaque" }

		
		Pass
		{
			Name "Vertext_Amplify"
			Blend SrcAlpha OneMinusSrcAlpha

			HLSLPROGRAM
            
            #pragma target 5.0
            #pragma vertex vert
            #pragma geometry geom
            #pragma fragment frag

            #pragma shader_feature ABSVAL
            #pragma shader_feature WIREFRAME
            #pragma shader_feature DIGITS
            #pragma shader_feature DO_ROUND
			#include "UnityCG.cginc"

			
			uniform float _CustomValue;

			
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

			// dummy structures to make amplify template work.
			struct DummyOutput
			{
				float4 clipPos : SV_POSITION;
				
			};

			half4 DummyFrag( DummyOutput IN  ) : SV_Target
			{
				
				return 0;
			}	

            float4 GetCustomData(appdata v)
            {
				DummyOutput o = (DummyOutput)0;

				float4 temp_cast_0 = (_CustomValue).xxxx;
				
				float4 output = temp_cast_0;

                return output;
            }

            #include "DigitRenderer.hlsl"
		
			ENDHLSL
		}
	}

	CustomEditor "Onager.Vertext.VertextShaderEditor"
	Fallback "Hidden/InternalErrorShader"
	
}/*ASEBEGIN
Version=18900
1134;397;578;302;248.3393;117.4623;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;1;-149.6664,0.1756735;Inherit;False;Property;_CustomValue;CustomValue;0;0;Create;True;0;0;0;False;0;False;3.14;3.14;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;3;130.0232,2.015862;Float;False;True;-1;2;Onager.Vertext.VertextShaderEditor;0;5;Onager/Amplify/Vertext;233f5dd7fc19c524eafbf1a99f74aa37;True;Vertext_Amplify;0;0;Vertext_Amplify;1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;RenderType=Opaque=RenderType;False;0;True;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;7;0;Hidden/InternalErrorShader;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;3;0;1;0
ASEEND*/
//CHKSM=72C413CB8FC5AE6947378249526304ECE5C93944