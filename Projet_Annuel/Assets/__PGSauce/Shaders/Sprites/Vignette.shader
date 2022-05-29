Shader "Custom/Vignette"
{
    Properties
    {
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Main texture Tint", Color) = (1,1,1,1)
    	_Strength ("Strength", float) = 0
    }

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma exclude_renderers d3d11_9x

			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 position   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 uv  : TEXCOORD0;
			};

			fixed4 _Color;
		float _Strength; 


			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.position = UnityObjectToClipPos(IN.vertex);
				OUT.uv = IN.uv;
				OUT.color = IN.color;

				return OUT;
			}

			sampler2D _MainTex;

			fixed4 frag(v2f IN) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, IN.uv);

				float1 l = length(IN.uv - float2(0.5, 0.5));

				l = (l - 0) / (sqrt(2)/2 - 0) * (1 - 0) + 0;

				l = 1 - l;

				l = l - _Strength;
				
				return fixed4(l,l,l,1) * col * fixed4(IN.color.a,IN.color.a,IN.color.a,IN.color.a);
			}
		ENDCG
		}
	}
}