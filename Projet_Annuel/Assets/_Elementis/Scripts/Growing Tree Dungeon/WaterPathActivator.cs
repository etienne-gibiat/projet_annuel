using System;
using System.Collections.Generic;
using _Elementis.Scripts.Character_Controller;
using Cinemachine;
using PGSauce.Core.Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class WaterPathActivator : MonoBehaviour
    {
        public CinemachineVirtualCamera pathCam;
        public Transform camFollow;
        public WaterPathActivatorPrePhaseBase prePhase;
        public float pathSpeed = 1;
        public RamSpline spline;
        public List<Vector4> pathsLocalPoints;

        private bool _activated;
        private bool _animating;
        private int _currentPointIndex;
        private Vector3 _currentPathPosition;
        private List<Vector4> _paths;
        private bool _hasEnded;
        [SerializeField] private float debugRadiusScale = 1f;

        private void Awake()
        {
            _activated = false;
            pathCam.Priority = 0;
            _animating = false;
            _paths = new List<Vector4>();
            _hasEnded = false;
        }

        public void Activate()
        {
            if(_activated){return;}
            _activated = true;

            var player = FindObjectOfType<ElementisCharacterController>();
            player.LockInputs();
            player.UnFocusCamera();
            pathCam.Priority = 10;

            StartCoroutine(prePhase.DoPrePhase(this, OnPrephaseEnd));
        }

        private void Update()
        {
            if(!_animating){return;}
            if(_hasEnded) {return;}
            if(IsCurrentPointLastOne) {return;}

            var targetPos = spline.transform.GetWorldPosition(pathsLocalPoints[_currentPointIndex + 1]);
            if (Vector3.Distance(targetPos, _currentPathPosition) <= .01f)
            {
                _currentPathPosition = targetPos;
                var localPoint = spline.transform.InverseTransformPoint(_currentPathPosition);
                _paths[_paths.Count - 1] = new Vector4(localPoint.x, localPoint.y, localPoint.z, pathsLocalPoints[_currentPointIndex + 1].w);
                _currentPointIndex++;
                if (IsCurrentPointLastOne)
                {
                    UpdateSpline();
                    OnActivationEnd();
                    return;
                }
                _paths.Add(new Vector4(localPoint.x, localPoint.y, localPoint.z, pathsLocalPoints[_currentPointIndex + 1].w));
                targetPos = spline.transform.GetWorldPosition(pathsLocalPoints[_currentPointIndex + 1]);
            }

            var dirToTargetPos = (targetPos - _currentPathPosition);
            var distance = dirToTargetPos.magnitude;
            dirToTargetPos.Normalize();
            var delta = Mathf.Min(distance, pathSpeed * Time.deltaTime);
            _currentPathPosition += dirToTargetPos * delta;
            var point = spline.transform.InverseTransformPoint(_currentPathPosition);
            var currentW = pathsLocalPoints[_currentPointIndex].w;
            var nextW = pathsLocalPoints[_currentPointIndex + 1].w;
            var startPos = spline.transform.GetWorldPosition(pathsLocalPoints[_currentPointIndex]);
            var distanceStartToEnd = Vector3.Distance(startPos, targetPos);
            var pathWidth = distance.Remap(0, distanceStartToEnd, nextW, currentW);
            _paths[_paths.Count - 1] = new Vector4(point.x, point.y, point.z, pathWidth);
            UpdateSpline();
            SetLookAtPosition(_currentPathPosition);
        }

        private bool IsCurrentPointLastOne => _currentPointIndex == pathsLocalPoints.Count - 1;

        private void OnActivationEnd()
        {
            _hasEnded = true;
        }

        private void OnPrephaseEnd()
        {
            _animating = true;
            _currentPointIndex = 0;
            _currentPathPosition = spline.transform.GetWorldPosition(pathsLocalPoints[_currentPointIndex]);
            SetLookAtPosition(_currentPathPosition);
            var localPoint = spline.transform.InverseTransformPoint(_currentPathPosition);
            _paths.Add(new Vector4(localPoint.x, localPoint.y, localPoint.z, pathsLocalPoints[_currentPointIndex].w));
            _paths.Add(new Vector4(localPoint.x, localPoint.y, localPoint.z, pathsLocalPoints[_currentPointIndex].w));
            UpdateSpline();
        }

        private void UpdateSpline()
        {
            spline.controlPoints = _paths;
            spline.GenerateSpline();
        }

        public void SetLookAtPosition(Vector3 pos)
        {
            camFollow.position = pos;
        }

        [Button]
        private void ConvertSplineToPoints()
        {
            pathsLocalPoints = spline.controlPoints;
            spline.controlPoints = new List<Vector4>();
            spline.GenerateSpline();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green.WithAlpha(0.85f);
            foreach (var localPoint in pathsLocalPoints)
            {
                var world = spline.transform.GetWorldPosition(localPoint);
                Gizmos.DrawSphere(world, localPoint.w * debugRadiusScale);
            }
        }
    }
}