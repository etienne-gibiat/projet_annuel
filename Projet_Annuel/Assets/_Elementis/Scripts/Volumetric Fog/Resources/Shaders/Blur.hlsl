#ifndef VOLUMETRIC_FOG_2_BLUR
#define VOLUMETRIC_FOG_2_BLUR

#include "CommonsURP.hlsl"

TEXTURE2D_X(_MainTex);
TEXTURE2D_X(_LightBuffer);
TEXTURE2D_X(_DownsampledDepth);
float4    _MainTex_TexelSize;
float4    _MainTex_ST;
float     _BlurScale;
float4    _DownsampledDepth_TexelSize;
float     _EdgeThreshold;

// Optimization for SSPR
#define uvN uv1
#define uvE uv2
#define uvW uv3
#define uvS uv4


#if defined(UNITY_STEREO_INSTANCING_ENABLED) || defined(UNITY_STEREO_MULTIVIEW_ENABLED) || defined(SINGLE_PASS_STEREO)
    #define VERTEX_CROSS_UV_DATA
    #define VERTEX_OUTPUT_GAUSSIAN_UV(o)

    #if defined(BLUR_HORIZ)
        #define FRAG_SETUP_GAUSSIAN_UV(i) float2 inc = float2(_MainTex_TexelSize.x * 1.3846153846 * _BlurScale, 0); float2 uv1 = i.uv - inc; float2 uv2 = i.uv + inc; float2 inc2 = float2(_MainTex_TexelSize.x * 3.2307692308 * _BlurScale, 0); float2 uv3 = i.uv - inc2; float2 uv4 = i.uv + inc2;
    #else
        #define FRAG_SETUP_GAUSSIAN_UV(i) float2 inc = float2(0, _MainTex_TexelSize.y * 1.3846153846 * _BlurScale); float2 uv1 = i.uv - inc; float2 uv2 = i.uv + inc; float2 inc2 = float2(0, _MainTex_TexelSize.y * 3.2307692308 * _BlurScale); float2 uv3 = i.uv - inc2; float2 uv4 = i.uv + inc2;
    #endif

#else
    #define VERTEX_CROSS_UV_DATA float2 uvN : TEXCOORD1; float2 uvW: TEXCOORD2; float2 uvE: TEXCOORD3; float2 uvS: TEXCOORD4;

    #if defined(BLUR_HORIZ)
        #define VERTEX_OUTPUT_GAUSSIAN_UV(o) float2 inc = float2(_MainTex_TexelSize.x * 1.3846153846 * _BlurScale, 0); o.uv1 = o.uv - inc; o.uv2 = o.uv + inc; float2 inc2 = float2(_MainTex_TexelSize.x * 3.2307692308 * _BlurScale, 0); o.uv3 = o.uv - inc2; o.uv4 = o.uv + inc2;
    #else
        #define VERTEX_OUTPUT_GAUSSIAN_UV(o) float2 inc = float2(0, _MainTex_TexelSize.y * 1.3846153846 * _BlurScale); o.uv1 = o.uv - inc; o.uv2 = o.uv + inc; float2 inc2 = float2(0, _MainTex_TexelSize.y * 3.2307692308 * _BlurScale); o.uv3 = o.uv - inc2; o.uv4 = o.uv + inc2;
    #endif
    #define FRAG_SETUP_GAUSSIAN_UV(i) float2 uv1 = i.uv1; float2 uv2 = i.uv2; float2 uv3 = i.uv3; float2 uv4 = i.uv4;

#endif

	struct VaryingsCross {
	    float4 positionCS : SV_POSITION;
	    float2 uv: TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
        VERTEX_CROSS_UV_DATA
        UNITY_VERTEX_OUTPUT_STEREO
	};


	VaryingsCross VertBlur(Attributes v) {
    	VaryingsCross o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_TRANSFER_INSTANCE_ID(v, o);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	    o.positionCS = v.positionOS;
		o.positionCS.y *= _ProjectionParams.x;
    	o.uv = v.uv;
        VERTEX_OUTPUT_GAUSSIAN_UV(o)

    	return o;
	}

	inline float getLuma(float4 rgb) {
		const float3 lum = float3(0.299, 0.587, 0.114);
		return dot(rgb.xyz, lum);
	}
	
	float4 FragBlur (VaryingsCross i): SV_Target {
    	UNITY_SETUP_INSTANCE_ID(i);
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
        i.uv = UnityStereoTransformScreenSpaceTex(i.uv);
        FRAG_SETUP_GAUSSIAN_UV(i)

        #if defined(FINAL_BLEND) && EDGE_PRESERVE
            // edge aware blur
            float4 mask  = SAMPLE_TEXTURE2D_X(_LightBuffer, sampler_PointClamp, i.uv);
            float4 mask1 = SAMPLE_TEXTURE2D_X(_LightBuffer, sampler_PointClamp, uv1);
            float4 mask2 = SAMPLE_TEXTURE2D_X(_LightBuffer, sampler_PointClamp, uv2);
            float4 mask3 = SAMPLE_TEXTURE2D_X(_LightBuffer, sampler_PointClamp, uv3);
            float4 mask4 = SAMPLE_TEXTURE2D_X(_LightBuffer, sampler_PointClamp, uv4);
            float luma  = getLuma(mask);
            float luma1 = getLuma(mask1);
            float luma2 = getLuma(mask2);
            float luma3 = getLuma(mask3);
            float luma4 = getLuma(mask4);
            float4 diffs = saturate(float4(luma, luma1, luma3, luma4) / _EdgeThreshold);
            float atten = max(max(diffs.x, diffs.y), max(diffs.z, diffs.w));
            if (atten <= 0) return 0;
        #endif

        float4 pixel0 = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, i.uv) * 0.2270270270;
        float4 pixel1 = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, uv1) * 0.3162162162;
        float4 pixel2 = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, uv2) * 0.3162162162;
		float4 pixel3 = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, uv3) * 0.0702702703;
        float4 pixel4 = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, uv4) * 0.0702702703;

        // remove black halo around edges
        if (getLuma(pixel1) <= 0) pixel1 = pixel2;
        if (getLuma(pixel2) <= 0) pixel2 = pixel1;
        if (getLuma(pixel3) <= 0) pixel3 = pixel4;
        if (getLuma(pixel4) <= 0) pixel4 = pixel3;

        float4 pixel = pixel0 + pixel1 + pixel2 + pixel3 + pixel4;

        #if defined(DITHER)
            const float3 magic = float3( 0.06711056, 0.00583715, 52.9829189 );
            float jitter = frac( magic.z * frac( dot( i.uv.xy * _MainTex_TexelSize.zw, magic.xy ) ) );
	        pixel = max(0, pixel - jitter * 0.01);
        #endif

        #if defined(FINAL_BLEND) && EDGE_PRESERVE
            pixel *= atten;
        #endif

   		return pixel;
	}

	struct VaryingsSimple {
	    float4 positionCS : SV_POSITION;
	    float2 uv: TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
        UNITY_VERTEX_OUTPUT_STEREO
	};


	VaryingsSimple VertSimple(Attributes v) {
    	VaryingsSimple o;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_TRANSFER_INSTANCE_ID(v, o);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

	    o.positionCS = v.positionOS;
		o.positionCS.y *= _ProjectionParams.x;
    	o.uv = v.uv;

    	return o;
	}

	float4 FragSeparatedBlend (VaryingsSimple i): SV_Target {
    	UNITY_SETUP_INSTANCE_ID(i);
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);

        float depthFull = GetRawDepth(i.uv);
        
        const float threshold = 0.0005;
        const float t = 0.5;
        float2 uv00 = UnityStereoTransformScreenSpaceTex(i.uv + _DownsampledDepth_TexelSize.xy * float2(-t, -t));
        float2 uv10 = UnityStereoTransformScreenSpaceTex(i.uv + _DownsampledDepth_TexelSize.xy * float2(t, -t));
        float2 uv01 = UnityStereoTransformScreenSpaceTex(i.uv + _DownsampledDepth_TexelSize.xy * float2(-t, t));
        float2 uv11 = UnityStereoTransformScreenSpaceTex(i.uv + _DownsampledDepth_TexelSize.xy * float2(t, t));
        float4 depths;
        depths.x = SAMPLE_TEXTURE2D_X(_DownsampledDepth, sampler_LinearClamp, uv00).r;
        depths.y = SAMPLE_TEXTURE2D_X(_DownsampledDepth, sampler_LinearClamp, uv10).r;
        depths.z = SAMPLE_TEXTURE2D_X(_DownsampledDepth, sampler_LinearClamp, uv01).r;
        depths.w = SAMPLE_TEXTURE2D_X(_DownsampledDepth, sampler_LinearClamp, uv11).r;
        float4 diffs = abs(depthFull.xxxx - depths);

        float2 minUV = UnityStereoTransformScreenSpaceTex(i.uv);
        if (any(diffs > threshold)) {
            // Check 10 vs 00
            float minDiff  = lerp(diffs.x, diffs.y, diffs.y < diffs.x);
            minUV    = lerp(uv00, uv10, diffs.y < diffs.x);
            // Check against 01
            minUV    = lerp(minUV, uv01, diffs.z < minDiff);
            minDiff  = lerp(minDiff, diffs.z, diffs.z < minDiff);
            // Check against 11
            minUV    = lerp(minUV, uv11, diffs.w < minDiff);
        } 
        
        float4 pixel = SAMPLE_TEXTURE2D_X(_MainTex, sampler_LinearClamp, minUV);
   		return pixel;
	}	

	
	float4 FragOnlyDepth (VaryingsSimple i): SV_Target {
    	UNITY_SETUP_INSTANCE_ID(i);
        UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
        return GetRawDepth(i.uv);
	}

#endif // VOLUMETRIC_FOG_2_BLUR