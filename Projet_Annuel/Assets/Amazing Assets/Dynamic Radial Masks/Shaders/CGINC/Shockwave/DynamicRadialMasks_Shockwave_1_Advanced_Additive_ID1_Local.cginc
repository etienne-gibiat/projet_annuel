#define DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT 1


float4 DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Position[DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];	
float  DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Radius[DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Intensity[DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_NoiseStrength[DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_EdgeSize[DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Smooth[DynamicRadialMasks_SHOCKWAVE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local(float3 positionWS, float noise)
{
    float retValue = 0; 

	int i = 0;

	{
		retValue += ShaderExtensions_DynamicRadialMasks_Shockwave_Advanced(positionWS,
																	noise,
																	DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Position[i].xyz, 
																	DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Radius[i],         
																	DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Intensity[i],
																	DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_NoiseStrength[i],  
																	DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_EdgeSize[i],		
																	DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_Smooth[i]);
	}		

    return retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local(positionWS, noise); 		
}

void DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_Shockwave_1_Advanced_Additive_ID1_Local(positionWS, noise); 		
}
