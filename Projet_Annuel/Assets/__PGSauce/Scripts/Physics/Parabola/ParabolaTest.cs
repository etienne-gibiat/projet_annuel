using System;
using UnityEngine;

namespace PGSauce.Core.PGPhysics
{
    public class ParabolaTest : MonoBehaviour
    {
        [SerializeField] private Parabola parabola;
        [SerializeField] private float rangeMin = 2, rangeMax = 10;
        [SerializeField] private float rotSpeed = 50;
        [SerializeField] private Transform arrowSpawn;
        [SerializeField] private float radius = .05f;
        [SerializeField] private Vector2 hLimit, vLimit;
        [SerializeField] private bool testCollision, refine;
        [SerializeField, Min(2)] private int resolution = 30;
        [SerializeField] private ParabolaViewer parabolaViewer;

        private Parabola.ParabolaData _parabolaData;

        public Vector3 EulerAngles;
        public Vector3 initialEuler;

        private void Awake()
        {
            EulerAngles = transform.eulerAngles;
            initialEuler = EulerAngles;
            parabolaViewer.Initialize(resolution);
        }

        private void Update()
        {
            var deltaX = Input.GetAxis("Horizontal");
            var deltaY = Input.GetAxis("Vertical");
            EulerAngles += new Vector3(-deltaY, deltaX) * (rotSpeed * Time.deltaTime);

            var initialEulerX = initialEuler.x + vLimit.x;
            var vLimitY = initialEuler.x + vLimit.y;
            EulerAngles.x = Mathf.Clamp(EulerAngles.x, initialEulerX, vLimitY);
            EulerAngles.y = Mathf.Clamp(EulerAngles.y, initialEuler.y + hLimit.x, initialEuler.y + hLimit.y);
            
            transform.rotation = Quaternion.Euler(EulerAngles);

            var range = Mathf.Lerp(rangeMin, rangeMax, 1 - Mathf.InverseLerp(initialEulerX, vLimitY, EulerAngles.x));

            _parabolaData = parabola.GetParabolaWithRange(arrowSpawn, range, resolution, testCollision, refine);
            
            parabolaViewer.UpdatePositions(_parabolaData.WorldSpacePoints);
        }

        private void OnDrawGizmos()
        {
            if(_parabolaData.WorldSpacePoints == null){return;}
            
            Gizmos.color = Color.black;
            foreach (var vector3 in _parabolaData.WorldSpacePoints)
            {
                Gizmos.DrawSphere(vector3, radius);
            }
            
            Gizmos.color = Color.green;
            
            Gizmos.DrawSphere(_parabolaData.WorldParabolaSummit, radius);
            
        }
    }
}