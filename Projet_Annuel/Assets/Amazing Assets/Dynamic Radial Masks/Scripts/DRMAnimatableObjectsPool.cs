using System.Collections.Generic;

using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    public class DRMAnimatableObjectsPool : MonoBehaviour
    {
        public DRMController drmController;

        [HideInInspector]
        public List<DRMAnimatableObject> drmAnimatableObjects;


        private void Start()
        {
            drmAnimatableObjects = new List<DRMAnimatableObject>();
        }

        void Update()
        {
            if (drmController == null || drmAnimatableObjects == null)
                return;


            //Make sure all variables are set to zero
            drmController.ResetShaderData();

            for (int i = drmAnimatableObjects.Count - 1; i >= 0; i -= 1)
            {
                bool isAlive = drmAnimatableObjects[i].Animate(Time.deltaTime);


                if (isAlive)  //Sending data to the DRM Controller
                {
                    drmController.shaderData_Position[i] = drmAnimatableObjects[i].position;
                    drmController.shaderData_Radius[i] = drmAnimatableObjects[i].currentRadius;
                    drmController.shaderData_Intensity[i] = drmAnimatableObjects[i].currentIntensity;
                    drmController.shaderData_NoiseStrength[i] = drmAnimatableObjects[i].currentNoiseStrength;
                    drmController.shaderData_EdgeSize[i] = drmAnimatableObjects[i].currentEdgeSize;
                    drmController.shaderData_RingCount[i] = drmAnimatableObjects[i].currentRingCount;
                    drmController.shaderData_Phase[i] = drmAnimatableObjects[i].currentPhase;
                    drmController.shaderData_Frequency[i] = drmAnimatableObjects[i].currentFrequency;
                    drmController.shaderData_Smooth[i] = drmAnimatableObjects[i].currentSmooth;
                }
                else
                {
                    drmAnimatableObjects.RemoveAt(i);
                }
            }
        }

        public void AddItem(Vector3 position, DRMAnimatableObject item)
        {
            if (drmAnimatableObjects == null)
            {
                drmAnimatableObjects = new List<DRMAnimatableObject>();
            }

            //Pool can not hold more objects than DRM Controller
            if (drmAnimatableObjects.Count < drmController.count)
            {
                drmAnimatableObjects.Add(new DRMAnimatableObject(item, position));
            }
        }
    }
}