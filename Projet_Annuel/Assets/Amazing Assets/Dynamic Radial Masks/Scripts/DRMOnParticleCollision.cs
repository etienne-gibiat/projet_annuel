using System.Collections.Generic;

using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DRMOnParticleCollision : MonoBehaviour
    {
        public DRMAnimatableObjectsPool drmObjectPool;

        public DRMAnimatableObject drmObject;

        ParticleSystem pSystem;
        List<ParticleCollisionEvent> collisionEvents;


        void Start()
        {
            pSystem = GetComponent<ParticleSystem>();
            collisionEvents = new List<ParticleCollisionEvent>();
        }

        private void OnParticleCollision(GameObject other)
        {
            if (pSystem == null || collisionEvents == null || other == null)
                return;


            int numCollisionEvents = pSystem.GetCollisionEvents(other, collisionEvents);

            int i = 0;
            while (i < numCollisionEvents)
            {
                Vector3 pos = collisionEvents[i].intersection;

                drmObjectPool.AddItem(pos, drmObject);

                i++;
            }
        }
    }
}