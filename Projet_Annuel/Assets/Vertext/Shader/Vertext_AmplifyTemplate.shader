Shader /*ase_name*/ "Hidden/Onager/Vertext" /*end*/
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

		/*ase_props*/
	}

	SubShader
	{
		Tags { "RenderType"="Opaque" }

		/*ase_pass*/
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

			/*ase_pragma*/
			/*ase_globals*/
			/*ase_funcs*/

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

				/*ase_vdata:p=p;c=c;n=n;t=t;uv0=tc0;uv1=tc1;uv2=tc2;uv3=tc3;uv4=tc4;uv5=tc5;uv6=tc6;uv7=tc7*/
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
				/*ase_interp(0,):sp=sp*/
			};

			half4 DummyFrag( DummyOutput IN /*ase_frag_input*/ ) : SV_Target
			{
				/*ase_frag_code:IN=DummyOutput*/
				return 0;
			}	

            float4 GetCustomData(appdata v)
            {
				DummyOutput o = (DummyOutput)0;

				/*ase_vert_code:v=appdata;o=DummyOutput*/
				float4 output = /*ase_vert_out:Data;Float4;_Data*/6.283185307179586/*end*/;

                return output;
            }

            #include "DigitRenderer.hlsl"
		
			ENDHLSL
		}
	}

	CustomEditor "Onager.Vertext.VertextShaderEditor"
	FallBack "Hidden/InternalErrorShader"
}