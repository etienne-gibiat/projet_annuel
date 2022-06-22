using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    [ExecuteInEditMode]
    public class DRMController : MonoBehaviour
    {
        public enum MASK_SHAPE { Torus, Tube, HeightField, Dot, Shockwave, Rings, Ripple };
        public enum MASK_PROPERTY_NAME { Position, Radius, Intensity, NoiseStrength, EdgeSize, RingCount, Phase, Frequency, Smooth };
        public enum MASK_ALGORITHM { Advanced, Simple }
        public enum MASK_BLEND_MODE { Additive, Normalized }
        public enum MASK_SCOPE { Local, Global }


        public MASK_SHAPE shape;
        MASK_SHAPE previousShape;

        [DRMReadOnly] public int count = 16;
        int previousCount;

        public MASK_ALGORITHM algorithm;
        MASK_ALGORITHM previousAlgorithm;

        public MASK_BLEND_MODE blendMode;
        MASK_BLEND_MODE previousBlendMode;

        [Range(1, 16)]
        public int ID = 1;
        int previousID;

        public MASK_SCOPE scope;
        MASK_SCOPE previousScope;

        [HideInInspector]
        public Material[] materials;

        [Space]
        [HideInInspector] public bool drawInEditor = false;



        [HideInInspector] public Vector4[] shaderData_Position;
        [HideInInspector] public float[] shaderData_Radius;
        [HideInInspector] public float[] shaderData_Intensity;
        [HideInInspector] public float[] shaderData_NoiseStrength;
        [HideInInspector] public float[] shaderData_EdgeSize;
        [HideInInspector] public float[] shaderData_RingCount;
        [HideInInspector] public float[] shaderData_Phase;
        [HideInInspector] public float[] shaderData_Frequency;
        [HideInInspector] public float[] shaderData_Smooth;


        int materialPropertyID_Position;
        int materialPropertyID_Radius;
        int materialPropertyID_Intensity;
        int materialPropertyID_NoiseStrength;
        int materialPropertyID_EdgeSize;
        int materialPropertyID_RingCount;
        int materialPropertyID_Phase;
        int materialPropertyID_Frequency;
        int materialPropertyID_Smooth;



        private void OnDisable()
        {
            ResetShaderData();

            Update();
        }

        void Start()
        {
            Initialize();
        }

        void Update()
        {
            if (previousCount != count ||
                previousShape != shape ||
                previousAlgorithm != algorithm ||
                previousBlendMode != blendMode ||
                previousID != ID ||
                previousScope != scope)
            {
                ResetShaderData();
                UpdateShaderData(previousScope);

                Initialize();
            }


            UpdateShaderData(scope);
        }


        void Initialize()
        {
            if (count < 1)
                count = 1;


            shaderData_Position = new Vector4[count];
            shaderData_Radius = new float[count];
            shaderData_Intensity = new float[count];
            shaderData_NoiseStrength = new float[count];
            shaderData_EdgeSize = new float[count];
            shaderData_RingCount = new float[count];
            shaderData_Phase = new float[count];
            shaderData_Frequency = new float[count];
            shaderData_Smooth = new float[count];

            ResetShaderData();


            previousCount = count;
            previousShape = shape;
            previousAlgorithm = algorithm;
            previousBlendMode = blendMode;
            previousScope = scope;
            previousID = ID;


            GenerateShaderPropertyIDs();
        }

        public void ResetShaderData()
        {
            int arrayLength = shaderData_Position == null ? 0 : shaderData_Position.Length;


            for (int i = 0; i < arrayLength; i++)
            {
                shaderData_Position[i] = Vector4.zero;
                shaderData_Radius[i] = 0;
                shaderData_Intensity[i] = 0;
                shaderData_NoiseStrength[i] = 0;
                shaderData_EdgeSize[i] = 0;
                shaderData_RingCount[i] = 0;
                shaderData_Phase[i] = 0;
                shaderData_Frequency[i] = 0;
                shaderData_Smooth[i] = 0;
            }


        }

        void UpdateShaderData(MASK_SCOPE updateScope)
        {
            if (updateScope == MASK_SCOPE.Global)
            {
                Shader.SetGlobalVectorArray(materialPropertyID_Position, shaderData_Position);
                Shader.SetGlobalFloatArray(materialPropertyID_Radius, shaderData_Radius);
                Shader.SetGlobalFloatArray(materialPropertyID_Intensity, shaderData_Intensity);
                Shader.SetGlobalFloatArray(materialPropertyID_NoiseStrength, shaderData_NoiseStrength);
                Shader.SetGlobalFloatArray(materialPropertyID_EdgeSize, shaderData_EdgeSize);
                Shader.SetGlobalFloatArray(materialPropertyID_RingCount, shaderData_RingCount);
                Shader.SetGlobalFloatArray(materialPropertyID_Phase, shaderData_Phase);
                Shader.SetGlobalFloatArray(materialPropertyID_Frequency, shaderData_Frequency);
                Shader.SetGlobalFloatArray(materialPropertyID_Smooth, shaderData_Smooth);
            }
            else if (materials != null)
            {
                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i] == null)
                        continue;

                    materials[i].SetVectorArray(materialPropertyID_Position, shaderData_Position);
                    materials[i].SetFloatArray(materialPropertyID_Radius, shaderData_Radius);
                    materials[i].SetFloatArray(materialPropertyID_Intensity, shaderData_Intensity);
                    materials[i].SetFloatArray(materialPropertyID_NoiseStrength, shaderData_NoiseStrength);
                    materials[i].SetFloatArray(materialPropertyID_EdgeSize, shaderData_EdgeSize);
                    materials[i].SetFloatArray(materialPropertyID_RingCount, shaderData_RingCount);
                    materials[i].SetFloatArray(materialPropertyID_Phase, shaderData_Phase);
                    materials[i].SetFloatArray(materialPropertyID_Frequency, shaderData_Frequency);
                    materials[i].SetFloatArray(materialPropertyID_Smooth, shaderData_Smooth);
                }
            }
        }

        void GenerateShaderPropertyIDs()
        {
            materialPropertyID_Position = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.Position));
            materialPropertyID_Radius = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.Radius));
            materialPropertyID_Intensity = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.Intensity));
            materialPropertyID_NoiseStrength = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.NoiseStrength));
            materialPropertyID_EdgeSize = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.EdgeSize));
            materialPropertyID_RingCount = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.RingCount));
            materialPropertyID_Phase = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.Phase));
            materialPropertyID_Frequency = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.Frequency));
            materialPropertyID_Smooth = Shader.PropertyToID(DRMUtility.GetMaterialPropertyName(shape, count, algorithm, blendMode, ID, scope, MASK_PROPERTY_NAME.Smooth));
        }

        public float GetMaskValue(Vector3 position)
        {
            switch (shape)
            {
                case MASK_SHAPE.Torus:
                    return DRMUtility.CalculateValue_Torus(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity, shaderData_EdgeSize, shaderData_Smooth, algorithm == MASK_ALGORITHM.Simple);

                case MASK_SHAPE.Tube:
                    return DRMUtility.CalculateValue_Tube(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity, shaderData_EdgeSize);

                case MASK_SHAPE.HeightField:
                    return DRMUtility.CalculateValue_HeightField(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity, shaderData_EdgeSize, shaderData_Smooth, algorithm == MASK_ALGORITHM.Simple);

                case MASK_SHAPE.Dot:
                    return DRMUtility.CalculateValue_Dot(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity);

                case MASK_SHAPE.Shockwave:
                    return DRMUtility.CalculateValue_Shockwave(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity, shaderData_EdgeSize, shaderData_Smooth, algorithm == MASK_ALGORITHM.Simple);

                case MASK_SHAPE.Rings:
                    return DRMUtility.CalculateValue_Rings(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity, shaderData_EdgeSize, shaderData_RingCount, shaderData_Smooth, algorithm == MASK_ALGORITHM.Simple);

                case MASK_SHAPE.Ripple:
                    return DRMUtility.CalculateValue_Ripple(position, count, algorithm, blendMode, shaderData_Position, shaderData_Radius, shaderData_Intensity, shaderData_Phase, shaderData_Frequency, shaderData_Smooth, algorithm == MASK_ALGORITHM.Simple);


                default:
                    return 0;
            }
        }


        void OnDrawGizmos()
        {
            if (drawInEditor)
            {
                for (int i = 0; i < shaderData_Position.Length; i++)
                {
                    Gizmos.color = Color.white * shaderData_Intensity[i];
                    Gizmos.DrawWireSphere(shaderData_Position[i], shaderData_Radius[i]);
                }
            }
        }
    }
}