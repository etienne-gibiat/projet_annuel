#define DynamicRadialMasks_DOT_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT 64


float4 DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_Position[DynamicRadialMasks_DOT_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];	
float  DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_Radius[DynamicRadialMasks_DOT_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_Intensity[DynamicRadialMasks_DOT_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_NoiseStrength[DynamicRadialMasks_DOT_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local(float3 positionWS, float noise)
{
    float retValue = 0; 

	int i = 0;
	for(i = 0; i < DynamicRadialMasks_DOT_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT; i++)
	{
		retValue += ShaderExtensions_DynamicRadialMasks_Dot_Advanced(positionWS,
																	noise,
																	DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_Position[i].xyz,
																	DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_Radius[i],
																	DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_Intensity[i],
																	DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_NoiseStrength[i]);
	}		

    return retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local(positionWS, noise); 		
}

void DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_Dot_64_Advanced_Additive_ID1_Local(positionWS, noise); 		
}
