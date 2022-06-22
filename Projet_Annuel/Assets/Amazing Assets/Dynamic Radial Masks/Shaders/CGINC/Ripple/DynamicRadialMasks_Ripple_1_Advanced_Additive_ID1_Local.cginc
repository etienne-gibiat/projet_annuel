#define DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT 1


float4 DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Position[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];	
float  DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Radius[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Intensity[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_NoiseStrength[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Phase[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Frequency[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Smooth[DynamicRadialMasks_RIPPLE_1_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local(float3 positionWS, float noise)
{
    float retValue = 0; 

	int i = 0;

	{
		retValue += ShaderExtensions_DynamicRadialMasks_Ripple_Advanced(positionWS,
																	noise,
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Position[i].xyz, 
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Radius[i],         
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Intensity[i],
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_NoiseStrength[i],  
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Phase[i],          
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Frequency[i],      
																	DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_Smooth[i]);
	}		

    return retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local(positionWS, noise); 		
}

void DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_Ripple_1_Advanced_Additive_ID1_Local(positionWS, noise); 		
}
