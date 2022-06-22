#define DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT 4


uniform float4 DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Position[DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT];	
uniform float  DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Radius[DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Intensity[DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_NoiseStrength[DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_EdgeSize[DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Smooth[DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global(float3 positionWS, float noise)
{
    float retValue = 0; 

	int i = 0;
	for(i = 0; i < DynamicRadialMasks_TORUS_4_ADVANCED_ADDITIVE_ID1_GLOBAL_LOOP_COUNT; i++)
	{
		retValue += ShaderExtensions_DynamicRadialMasks_Torus_Advanced(positionWS,
																	noise,
																	DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Position[i].xyz, 
																	DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Radius[i],         
																	DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Intensity[i],
																	DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_NoiseStrength[i],  
																	DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_EdgeSize[i],		
																	DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_Smooth[i]);
	}		

    return retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global(positionWS, noise); 		
}

void DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_Torus_4_Advanced_Additive_ID1_Global(positionWS, noise); 		
}
