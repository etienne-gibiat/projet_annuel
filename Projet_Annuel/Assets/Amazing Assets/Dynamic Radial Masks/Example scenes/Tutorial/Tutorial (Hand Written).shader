Shader "Amazing Assets/Dynamic Radial Masks/Tutorial/Hand Written"
{
    Properties
    {

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100 
		 
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag


            #include "UnityCG.cginc"
			


            struct appdata
            {
                float4 vertex : POSITION;
                float2 textcoord : TEXCOORD0;
            };

            struct v2f
            {                
                float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
            };            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.textcoord.xy;

				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;	
               
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				float mask = 0;

                return mask;
            }
            ENDCG
        }
    }
}
