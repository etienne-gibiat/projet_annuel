using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    public class DRMOnMouseRaycast : MonoBehaviour
    {
        public LayerMask raycastLayerMask;
        public bool selfCollisionOnly;
        public float clicksPerSecond = 10;
        float clicksTimer;

        public DRMAnimatableObjectsPool drmObjectPool;

        public DRMAnimatableObject drmObject = new DRMAnimatableObject();



        void Update()
        {
            clicksTimer += Time.deltaTime;


            if (DRMInput.GetLeftMouseButtonDown())     //Do raycast on every maouse click
            {
                clicksTimer = 0;

                DoRaycast();
            }
            else if (DRMInput.GetLeftMouseButton())    //Do raycast when hodling mouse button down
            {
                if (clicksTimer > 1.0f / clicksPerSecond)
                {
                    clicksTimer = 0;

                    DoRaycast();
                }
            }

        }

        void DoRaycast()
        {
            RaycastHit hitInfo;
            Ray ray = Camera.main.ScreenPointToRay(DRMInput.GetMousePosition());

            if (Physics.Raycast(ray, out hitInfo, 1000, raycastLayerMask))
            {
                if (selfCollisionOnly)
                {
                    if (hitInfo.collider.gameObject == this.gameObject)
                        drmObjectPool.AddItem(hitInfo.point, drmObject);    //Add item to the pool only if collider is the script holder
                }
                else
                    drmObjectPool.AddItem(hitInfo.point, drmObject);    //Add item to the pool
            }
        }

        private void Reset()
        {
            raycastLayerMask = 1;
            clicksPerSecond = 10;

            drmObject.Reset();
        }
    }
}