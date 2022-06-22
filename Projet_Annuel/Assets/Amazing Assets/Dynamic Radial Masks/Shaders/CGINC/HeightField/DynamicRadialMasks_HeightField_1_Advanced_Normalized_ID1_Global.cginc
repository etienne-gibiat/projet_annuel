#define DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT 1


uniform float4 DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Position[DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT];	
uniform float  DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Radius[DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Intensity[DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_NoiseStrength[DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_EdgeSize[DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT];
uniform float  DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Smooth[DynamicRadialMasks_HEIGHTFIELD_1_ADVANCED_NORMALIZED_ID1_GLOBAL_LOOP_COUNT];


#include "../../Core/Core.cginc"



////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                                Main Method                                 //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
float DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global(float3 positionWS, float noise)
{
    float retValue = 1; 

	int i = 0;

	{
		retValue *= 1 - ShaderExtensions_DynamicRadialMasks_HeightField_Advanced(positionWS,
																		noise,
																		DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Position[i].xyz, 
																		DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Radius[i],
																		DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Intensity[i],
																		DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_NoiseStrength[i],	
																		DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_EdgeSize[i],	
																		DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_Smooth[i]);
	}		

    return 1 - retValue;
}

////////////////////////////////////////////////////////////////////////////////
//                                                                            //
//                               Helper Methods                               //
//                                                                            //
////////////////////////////////////////////////////////////////////////////////
void DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_float(float3 positionWS, float noise, out float retValue)
{
    retValue = DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global(positionWS, noise); 		
}

void DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global_half(half3 positionWS, half noise, out half retValue)
{
    retValue = DynamicRadialMasks_HeightField_1_Advanced_Normalized_ID1_Global(positionWS, noise); 		
}
