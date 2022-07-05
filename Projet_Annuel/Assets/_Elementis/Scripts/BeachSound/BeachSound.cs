using System;
using System.Collections.Generic;
using PGSauce.Core;
using PGSauce.Core.Extensions;
using PGSauce.Unity;
using UnityEngine;

namespace _Elementis.Scripts.Beach_Sound
{
    public class BeachSound : PGMonoBehaviour
    {
        public List<Vector3> localPoints;
        [SerializeField] private float debugRadius = 0.25f;

        private void OnDrawGizmos()
        {
            Gizmos.color = PgColors.Blueish;
            foreach (var pt in localPoints)
            {
                Gizmos.DrawSphere(transform.GetWorldPosition(pt), debugRadius);
            }
        }

        public void AddPoint(Vector3 worldPosition)
        {
            localPoints.Add(transform.GetLocalPosition(worldPosition).WithY(0));
        }
        
        public void TryRemovePoint(Vector3 worldPosition)
        {
            for (int i = localPoints.Count - 1; i >= 0; i--)
            {
                if (Vector3.Distance(worldPosition, transform.GetWorldPosition(localPoints[i])) <= debugRadius)
                {
                    localPoints.RemoveAt(i);
                }
            }
        }
    }
}