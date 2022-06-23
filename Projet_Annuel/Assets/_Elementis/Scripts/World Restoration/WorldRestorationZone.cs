using System;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts.World_Restoration
{
    public class WorldRestorationZone : PGMonoBehaviour
    {
        [SerializeField] public float maxRadius;
        [SerializeField] public Float01 currentRadiusLerp;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, maxRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, CurrentRadius);
        }

        public float CurrentRadius => Mathf.Lerp(0, maxRadius, currentRadiusLerp);
    }
}