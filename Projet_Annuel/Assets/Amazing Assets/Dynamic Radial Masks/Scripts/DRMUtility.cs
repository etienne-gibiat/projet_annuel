using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    static public class DRMUtility
    {
        static public string GetMaterialPropertyName(DRMController.MASK_SHAPE maskShape, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, int ID, DRMController.MASK_SCOPE scope, DRMController.MASK_PROPERTY_NAME propertyName)
        {
            return string.Format("DynamicRadialMasks_{0}_{1}_{2}_{3}_ID{4}_{5}_{6}",
                                                   maskShape,       //0
                                                   shapeCount,      //1
                                                   maskAlgorithm,   //2
                                                   maskBlendMode,   //3
                                                   ID,              //4
                                                   scope,           //5                                               
                                                   propertyName);   //6
        }



        #region CPU Calculations
        static public float CalculateValue_Torus(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity, float[] maskEdgeSize, float[] maskSmooth, bool isSimple)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_Torus(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_Torus(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }

        static public float CalculateValue_Tube(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity, float[] maskEdgeSize)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_Tube(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_Tube(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }

        static public float CalculateValue_HeightField(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity, float[] maskEdgeSize, float[] maskSmooth, bool isSimple)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_HeightField(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_HeightField(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }

        static public float CalculateValue_Dot(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_Dot(position, maskPosition[i], maskRadius[i], maskIntensity[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_Dot(position, maskPosition[i], maskRadius[i], maskIntensity[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }

        static public float CalculateValue_Shockwave(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity, float[] maskEdgeSize, float[] maskSmooth, bool isSimple)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_Shockwave(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_Shockwave(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }

        static public float CalculateValue_Rings(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity, float[] maskEdgeSize, float[] maskRingCount, float[] maskSmooth, bool isSimple)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_Rings(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], (int)maskRingCount[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_Rings(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskEdgeSize[i], (int)maskRingCount[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }

        static public float CalculateValue_Ripple(Vector3 position, int shapeCount, DRMController.MASK_ALGORITHM maskAlgorithm, DRMController.MASK_BLEND_MODE maskBlendMode, Vector4[] maskPosition, float[] maskRadius, float[] maskIntensity, float[] maskPhase, float[] maskFrequency, float[] maskSmooth, bool isSimple)
        {
            switch (maskBlendMode)
            {
                case DRMController.MASK_BLEND_MODE.Normalized:
                    {
                        float retValue = 1;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue *= 1 - DynamicRadialMasks_Ripple(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskPhase[i], maskFrequency[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return 1 - retValue;
                    }

                case DRMController.MASK_BLEND_MODE.Additive:
                    {
                        float retValue = 0;
                        for (int i = 0; i < shapeCount; i++)
                        {
                            retValue += DynamicRadialMasks_Ripple(position, maskPosition[i], maskRadius[i], maskIntensity[i], maskPhase[i], maskFrequency[i], isSimple ? 2 : maskSmooth[i]);
                        }

                        return retValue;
                    }

                default:
                    return 0;
            }
        }
        #endregion


        #region Shader Functions
        static float Max(float a, float b)
        {
            return a > b ? a : b;
        }

        static Vector2 Max(Vector2 a, Vector2 b)
        {
            return new Vector2(Max(a.x, b.x), Max(a.y, b.y));
        }

        static float Frac(float a)
        {
            if (a > 0)
                return a - Mathf.Floor(a);
            else if (a < 0)
                return (Mathf.Abs(a) - Mathf.Floor(Mathf.Abs(a))) * Mathf.Sign(a);
            else
                return 0;
        }


        static float DynamicRadialMasks_Torus(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity, float edgeSize, float smooth)
        {
            if (radius < 0.0001f || intensity < 0.0001f || edgeSize < 0.0001f)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            Vector2 r = Max(Vector2.zero, new Vector2(radius - d, d - radius) + Vector2.one * edgeSize) / edgeSize;

            float shape = Mathf.Clamp01(r.x * r.y);

            //Smooth			
            shape = Mathf.Pow(shape, smooth + 0.01f);   //BUG: does not work if do not add 0.01. Hmmmm

            //Fade
            shape *= intensity;


            return shape;
        }

        static float DynamicRadialMasks_Tube(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity, float edgeSize)
        {
            if (radius < 0.0001f || intensity < 0.0001f || edgeSize < 0.0001f)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            Vector2 r = Max(Vector2.zero, new Vector2(radius - d, d - radius) + Vector2.one * edgeSize) / edgeSize;

            float shape = Mathf.Clamp01(r.x * r.y);
            shape = Mathf.Clamp01(shape * 50);

            //Fade
            shape *= intensity;


            return shape;
        }

        static float DynamicRadialMasks_HeightField(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity, float edgeSize, float smooth)
        {
            if (radius < 0.0001f || intensity < 0.0001f)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            float shape = 1 - Mathf.Clamp01(Max(0, d - radius + edgeSize) / edgeSize);

            //Smooth
            shape = Mathf.Pow(shape, smooth + 0.01f);   //BUG: does not work if do not add 0.01. Hmmmm

            //Fade
            shape *= intensity;


            return shape;
        }

        static float DynamicRadialMasks_Dot(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity)
        {
            if (radius < 0.0001f || intensity < 0.0001f)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            //Shape
            float shape = d < radius ? 1 : 0;

            //Fade
            shape *= intensity;


            return shape;
        }

        static float DynamicRadialMasks_Shockwave(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity, float edgeSize, float smooth)
        {
            if (radius < 0.0001f || intensity < 0.0001f || edgeSize < 0.0001)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            float r = Mathf.Clamp01(Max(0, d - radius + edgeSize) / edgeSize);

            float shape = Frac(r);

            //Smooth
            shape = Mathf.Pow(shape, smooth + 0.01f);   //BUG: does not work if do not add 0.01. Hmmmm

            //Fade
            shape *= intensity;


            return shape;
        }

        static float DynamicRadialMasks_Rings(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity, float edgeSize, int ringCount, float power)
        {
            if (radius < 0.0001f || intensity < 0.0001f || edgeSize < 0.0001)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            float r = Mathf.Clamp01(Max(0, d - radius + edgeSize) / edgeSize);

            float shape = Frac(r * ringCount);
            shape = Mathf.Sin(shape * 3.14159f);

            //Smooth
            shape = Mathf.Pow(shape, power + 0.01f);   //BUG: does not work if do not add 0.01. Hmmmm

            //Fade
            shape *= intensity;


            return shape;
        }

        static float DynamicRadialMasks_Ripple(Vector3 vertexWorldPosition, Vector3 maskPosition, float radius, float intensity, float phase, float frequency, float power)
        {
            if (radius < 0.0001f || intensity < 0.0001f)
                return 0;


            //Distance
            float d = Vector3.Distance(maskPosition, vertexWorldPosition);

            float shape = Mathf.Sin(d * frequency - phase);


            // //[-1; 1] -> [0, 1]
            shape = (shape + 1) / 2;

            //Smooth
            shape = Mathf.Pow(shape, power + 0.01f);   //BUG: does not work if do not add 0.01. Hmmmm


            //Slope
            float r = Mathf.Clamp01(Max(0, radius - d) / radius);
            shape *= r / (d + 1);

            shape = (d < radius) ? shape : 0;

            //Fade
            shape *= intensity;


            return shape;
        }
        #endregion
    }
}