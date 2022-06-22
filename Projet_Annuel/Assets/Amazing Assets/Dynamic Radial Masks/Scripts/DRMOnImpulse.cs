using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    public class DRMOnImpulse : MonoBehaviour
    {
        public DRMAnimatableObjectsPool drmObjectsPool;

        public float impulseFrequency = 1;
        float deltaTime;


        public DRMAnimatableObject drmObject;


        private void Start()
        {
            deltaTime = impulseFrequency;
        }

        void Update()
        {
            deltaTime += Time.deltaTime;
            if (deltaTime > impulseFrequency)
            {
                deltaTime = 0;

                drmObjectsPool.AddItem(transform.position, drmObject);
            }
        }
    }
}