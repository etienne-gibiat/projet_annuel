using UnityEngine;

namespace PGSauce.Gameplay.AbilitySystem
{
    public class CastOrigin : MonoBehaviour
    {
        public Transform spawn;
        public Vector3 Position => spawn.position;
    }
}