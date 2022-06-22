using System.Collections.Generic;

using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    [ExecuteInEditMode]
    public class DRMGameObjectsPool : MonoBehaviour
    {
        public DRMController drmController;

        [Space(10)]
        public List<DRMGameObject> drmGameObjects = new List<DRMGameObject>();


        void OnDestroy()
        {
            if (drmController != null)
                drmController.ResetShaderData();
        }

        void OnDisable()
        {
            if (drmController != null)
                drmController.ResetShaderData();
        }

        void OnEnable()
        {
            Update();
        }

        void Update()
        {
            if (drmController == null)
                return;


            drmController.ResetShaderData();


            for (int i = 0; i < drmGameObjects.Count; i++)
            {
                //maskGameObjects array length can not be bigger than DRMController can handle
                if (i >= drmController.count)
                    break;


                if (drmGameObjects[i] == null)
                    continue;


                drmController.shaderData_Position[i] = drmGameObjects[i].transform.position;
                drmController.shaderData_Radius[i] = drmGameObjects[i].radius;
                drmController.shaderData_Intensity[i] = drmGameObjects[i].intensity;
                drmController.shaderData_NoiseStrength[i] = drmGameObjects[i].noiseStrength;
                drmController.shaderData_EdgeSize[i] = drmGameObjects[i].edgeSize;
                drmController.shaderData_RingCount[i] = drmGameObjects[i].ringCount;
                drmController.shaderData_Phase[i] = drmGameObjects[i].currentPhase;
                drmController.shaderData_Frequency[i] = drmGameObjects[i].frequency;
                drmController.shaderData_Smooth[i] = drmGameObjects[i].smooth;

            }
        }

        public void AddObject(DRMGameObject obj)
        {
            if (drmGameObjects.Count < drmController.count)
            {
                drmGameObjects.Add(obj);
            }
        }
    }
}