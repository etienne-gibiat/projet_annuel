using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks
{
    [ExecuteInEditMode]
    public class DRMGameObject : MonoBehaviour
    {
        public float radius = 5;
        public float intensity = 1;
        public float noiseStrength = 0;
        public float edgeSize = 1;
        public int ringCount = 3;
        public float frequency = 10;
        public float phaseSpeed = 2;
        public float smooth = 1;


        [Space(5)]
        public bool drawInEditor = true;

        [HideInInspector] public float currentPhase = 0;



        void Start()
        {
            currentPhase = 0;
        }

        void Update()
        {
            currentPhase += Time.deltaTime * phaseSpeed;
        }

        private void OnDrawGizmosSelected()
        {
            if (drawInEditor)
                Gizmos.DrawWireSphere(transform.position, radius);
        }
    }
}