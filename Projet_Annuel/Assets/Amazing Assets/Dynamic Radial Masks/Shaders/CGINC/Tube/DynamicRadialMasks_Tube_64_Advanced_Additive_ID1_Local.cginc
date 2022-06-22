#define DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT 64


float4 DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_Position[DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];	
float  DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_Radius[DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_Intensity[DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_NoiseStrength[DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];
float  DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_EdgeSize[DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local(float3 positionWS, float noise)
{
    float retValue = 0; 

	int i = 0;
	for(i = 0; i < DynamicRadialMasks_TUBE_64_ADVANCED_ADDITIVE_ID1_LOCAL_LOOP_COUNT; i++)
	{
		retValue += ShaderExtensions_DynamicRadialMasks_Tube_Advanced(positionWS,
																	noise,
																	DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_Position[i].xyz, 
																	DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_Radius[i],         
																	DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_Intensity[i],           
																	DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_NoiseStrength[i],  
																	DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_EdgeSize[i]);
	}		

    return retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local(positionWS, noise); 		
}

void DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_Tube_64_Advanced_Additive_ID1_Local(positionWS, noise); 		
}
