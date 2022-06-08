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
        public List<Transform> pathsToActivate;
        public float pathSpeed = 1;
        public float pathWidth = 1.5f;
        public RamSpline spline;
        public List<Vector4> pathsLocalPoints;

        private bool _activated;
        private bool _animating;
        private int _currentTransFormIndex;
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
            if(_currentTransFormIndex == pathsToActivate.Count - 1) {return;}

            var targetPos = pathsToActivate[_currentTransFormIndex + 1].position;
            if (Vector3.Distance(targetPos, _currentPathPosition) <= .01f)
            {
                _currentPathPosition = targetPos;
                var localPoint = spline.transform.InverseTransformPoint(_currentPathPosition);
                _paths[_paths.Count - 1] = new Vector4(localPoint.x, localPoint.y, localPoint.z, pathWidth);
                _currentTransFormIndex++;
                if (_currentTransFormIndex == pathsToActivate.Count - 1)
                {
                    UpdateSpline();
                    OnActivationEnd();
                    return;
                }
                _paths.Add(new Vector4(localPoint.x, localPoint.y, localPoint.z, pathWidth));
                targetPos = pathsToActivate[_currentTransFormIndex + 1].position;
            }

            var dirToTargetPos = (targetPos - _currentPathPosition);
            var distance = dirToTargetPos.magnitude;
            dirToTargetPos.Normalize();
            var delta = Mathf.Min(distance, pathSpeed * Time.deltaTime);
            _currentPathPosition += dirToTargetPos * delta;
            var point = spline.transform.InverseTransformPoint(_currentPathPosition);
            _paths[_paths.Count - 1] = new Vector4(point.x, point.y, point.z, pathWidth);
            UpdateSpline();
            SetLookAtPosition(_currentPathPosition);
        }

        private void OnActivationEnd()
        {
            _hasEnded = true;
        }

        private void OnPrephaseEnd()
        {
            _animating = true;
            _currentTransFormIndex = 0;
            _currentPathPosition = pathsToActivate[_currentTransFormIndex].position;
            SetLookAtPosition(_currentPathPosition);
            var localPoint = spline.transform.InverseTransformPoint(_currentPathPosition);
            _paths.Add(new Vector4(localPoint.x, localPoint.y, localPoint.z, pathWidth));
            _paths.Add(new Vector4(localPoint.x, localPoint.y, localPoint.z, pathWidth));
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