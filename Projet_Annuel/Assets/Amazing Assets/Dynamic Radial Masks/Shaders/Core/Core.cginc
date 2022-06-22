
#ifndef SHADER_EXTENTIONS_DYNAMIC_RADIAL_MASKS
#define SHADER_EXTENTIONS_DYNAMIC_RADIAL_MASKS


inline float ShaderExtensions_DynamicRadialMasks_Torus_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength, float edgeSize, float smooth)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	edgeSize += noise * noiseStrength;


	float2 r = max(float2(0, 0), float2(radius - d, d - radius) + edgeSize) / edgeSize;

	float shape = saturate(r.x * r.y);	

	//Smooth			
	shape = pow(shape, smooth + 0.01);	//BUG: does not work if do not add 0.01. Hmmmm

	//Fade
	shape *= intensity;


	return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_Torus_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity, float edgeSize)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	//-no


	float2 r = max(float2(0, 0), float2(radius - d, d - radius) + edgeSize) / edgeSize;

	float shape = saturate(r.x * r.y);	

	//Smooth			
	shape *= shape;

	//Fade
	shape *= intensity;


	return shape;
}


inline float ShaderExtensions_DynamicRadialMasks_Tube_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength, float edgeSize)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	edgeSize += noise * noiseStrength;


	float2 r = max(float2(0, 0), float2(radius - d, d - radius) + edgeSize) / edgeSize;

	float shape = saturate(r.x * r.y);
	shape = saturate(shape * 50);	

	//Fade
	shape *= intensity;


	return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_Tube_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity, float edgeSize)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);
	
	//Noise
	//-no


	float2 r = max(float2(0, 0), float2(radius - d, d - radius) + edgeSize) / edgeSize;

	float shape = saturate(r.x * r.y);
	shape = saturate(shape * 50);	

	//Fade
	shape *= intensity;


	return shape;
}


inline float ShaderExtensions_DynamicRadialMasks_HeightField_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength, float edgeSize, float smooth)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	radius += noise * noiseStrength;

	
    float shape = 1 - saturate(max(0, d - radius + edgeSize) / edgeSize);
	
	//Smooth
	shape = pow(shape, smooth + 0.01);	//BUG: does not work if do not add 0.01. Hmmmm

	//Fade
	shape *= intensity;


	return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_HeightField_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity, float edgeSize)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	//-no

	
    float shape = 1 - saturate(max(0, d - radius + edgeSize) / edgeSize);
	
	//Smooth
	shape *= shape;

	//Fade
	shape *= intensity;


	return shape;
}


inline float ShaderExtensions_DynamicRadialMasks_Dot_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	d += noise * noiseStrength;

	//Shape
	float shape = d < radius ? 1 : 0;

	//Fade
	shape *= intensity;


	return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_Dot_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	//-no

	//Shape
	float shape = d < radius ? 1 : 0;

	//Fade
	shape *= intensity;


	return shape;
}


inline float ShaderExtensions_DynamicRadialMasks_Shockwave_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength, float edgeSize, float smooth)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	edgeSize += noise * noiseStrength;


	float r = saturate(max(0, d - radius + edgeSize) / edgeSize);

	float shape = frac(r);

	//Smooth
	shape = pow(shape, smooth + 0.01);	//BUG: does not work if do not add 0.01. Hmmmm

	//Fade
	shape *= intensity;


	return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_Shockwave_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity, float edgeSize)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	//-no


	float r = saturate(max(0, d - radius + edgeSize) / edgeSize);

	float shape = frac(r);

	//Smooth
	shape *= shape;

	//Fade
	shape *= intensity;


	return shape;
}


inline float ShaderExtensions_DynamicRadialMasks_Rings_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength, float edgeSize, int ringCount, float smooth)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	d += noise * noiseStrength;


	float r = saturate(max(0, d - radius + edgeSize) / edgeSize);

	float shape = frac(r * ringCount);	
	shape = sin(shape * 3.14159);
	
	//Smooth
	shape = pow(shape, smooth + 0.01);	//BUG: does not work if do not add 0.01. Hmmmm

	//Fade
	shape *= intensity;


	return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_Rings_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity, float edgeSize, int ringCount)
{

	//Distance
	float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	//-no


	float r = saturate(max(0, d - radius + edgeSize) / edgeSize);

	float shape = frac(r * ringCount);	
	shape = sin(shape * 3.14159);
	
	//Smooth
	shape *= shape;

	//Fade
	shape *= intensity;


	return shape;
}


inline float ShaderExtensions_DynamicRadialMasks_Ripple_Advanced(float3 vertexWorldPosition, float noise, float3 maskPosition, float radius, float intensity, float noiseStrength, float phase, float frequency, float smooth)
{	

	//Distance
    float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	phase += noise * noiseStrength;   


    float shape = sin(d * frequency - phase);
	

	// //[-1; 1] -> [0, 1]
	shape = (shape + 1) / 2;

	//Smooth
	shape = pow(shape, smooth + 0.01);	//BUG: does not work if do not add 0.01. Hmmmm


    //Slope
	float r = saturate(max(0, radius - d) / radius);
    shape *= r / (d + 1);

	shape = (d < radius) ? shape : 0;

    //Fade
    shape *= intensity;
 

    return shape;
}

inline float ShaderExtensions_DynamicRadialMasks_Ripple_Simple(float3 vertexWorldPosition, float3 maskPosition, float radius, float intensity, float phase, float frequency)
{	

	//Distance
    float d = distance(maskPosition, vertexWorldPosition);

	//Noise
	//-no


    float shape = sin(d * frequency - phase);
	

	// //[-1; 1] -> [0, 1]
	shape = (shape + 1) / 2;

	//Smooth
	shape *= shape;


    //Slope
	float r = saturate(max(0, radius - d) / radius);
    shape *= r / (d + 1);

	shape = (d < radius) ? shape : 0;

    //Fade
    shape *= intensity;
 

    return shape;
}


#endif