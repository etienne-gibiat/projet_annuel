using UnityEngine;


namespace AmazingAssets.DynamicRadialMasks.Example
{
    public class KillZone : MonoBehaviour
    {
        public float groundLevel = 0;

        void FixedUpdate()
        {
            if (transform.position.y < groundLevel)
                GameObject.Destroy(this.gameObject);
        }
    }
}