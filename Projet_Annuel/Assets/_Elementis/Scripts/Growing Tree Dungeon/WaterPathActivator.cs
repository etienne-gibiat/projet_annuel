using System;
using System.Collections.Generic;
using _Elementis.Scripts.Character_Controller;
using Cinemachine;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

namespace _Elementis.Scripts.Growing_Tree_Dungeon
{
    public class WaterPathActivator : MonoBehaviour
    {
        public CinemachineVirtualCamera pathCam;
        public Transform camFollow;
        public WaterPathActivatorPrePhaseBase prePhase;
        public WaterPathActivatorPostPhase postPhase;
        public float pathSpeed = 1;
        public RamSpline spline;
        public List<Vector4> pathsLocalPoints;
        [SerializeField] private List<EventData> events;

        private bool _activated;
        private bool _animating;
        private int _currentPointIndex;
        private Vector3 _currentPathPosition;
        private List<Vector4> _paths;
        private bool _hasEnded;
        [SerializeField] private float debugRadiusScale = 1f;
        private ElementisCharacterController _player;

        private Float01 CurrentDistanceRatio { get; set; }
        
        [Serializable]
        private struct EventData
        {
            public Float01 whenToOccur;
            public UnityEvent whatToDo;
        }

        private void Awake()
        {
            _activated = false;
            pathCam.Priority = 0;
            _animating = false;
            _paths = new List<Vector4>();
            _hasEnded = false;
            CurrentDistanceRatio = 0;
        }

        public void Activate()
        {
            if(_activated){return;}
            _activated = true;

            _player = FindObjectOfType<ElementisCharacterController>();
            _player.LockInputs();
            _player.UnFocusCamera();
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
            CurrentDistanceRatio += delta / TotalDistance() ;
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
            if (events.Count > 0)
            {
                if (events[0].whenToOccur <= CurrentDistanceRatio)
                {
                    events[0].whatToDo.Invoke();
                    events.RemoveAt(0);
                }
            }
        }

        private bool IsCurrentPointLastOne => _currentPointIndex == pathsLocalPoints.Count - 1;

        private void OnActivationEnd()
        {
            _hasEnded = true;
            pathCam.Priority = 0;
            StartCoroutine(postPhase.DoPostPhase(this, OnPostPhaseEnd));
        }

        private void OnPostPhaseEnd()
        {
            _player.UnLockInputs();
            _player.FocusCamera();
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
#if UNITY_EDITOR
            Gizmos.color = Color.green.WithAlpha(0.85f);
            foreach (var localPoint in pathsLocalPoints)
            {
                var world = LocalControlPointToWorldPosition(localPoint);
                Gizmos.DrawSphere(world, localPoint.w * debugRadiusScale);
            }
            
            Gizmos.color = Color.blue.WithAlpha(0.85f);
            for (var index = 0; index < events.Count; index++)
            {
                var eventData = events[index];
                var worldPos = GetWorldPositionFromDistanceRatio(eventData.whenToOccur);
                Gizmos.DrawSphere(worldPos, debugRadiusScale);
                Handles.Label(worldPos, index.ToString());
            }
#endif
            
        }

        private Vector3 LocalControlPointToWorldPosition(Vector4 localPoint)
        {
            return spline.transform.GetWorldPosition(localPoint);
        }

        private Vector3 GetWorldPositionFromDistanceRatio(Float01 ratio)
        {
            var totalDistance = TotalDistance();
            if (totalDistance <= 0)
            {
                return Vector3.zero;
            }
            var distance = totalDistance * ratio;
            var currentDistance = 0f;
            
            for (int i = 0; i < pathsLocalPoints.Count - 1; i++)
            {
                var distanceCurrToNext = GetDistanceCurrToNext(i);
                if (currentDistance + distanceCurrToNext < distance)
                {
                    currentDistance += distanceCurrToNext;
                }
                else
                {
                    var t = (distance - currentDistance) / distanceCurrToNext ;
                    return Vector3.Lerp(LocalControlPointToWorldPosition(pathsLocalPoints[i]),
                        LocalControlPointToWorldPosition(pathsLocalPoints[i + 1]), t);
                }
            }

            throw new ArgumentException("Can't be here (ratio value must be between 0 and 1)");
        }

        [ShowInInspector]
        private float TotalDistance()
        {
            var dist = 0f;
            for (int i = 0; i < pathsLocalPoints.Count - 1; i++)
            {
                dist += GetDistanceCurrToNext(i);
            }

            return dist;
        }

        private float GetDistanceCurrToNext(int i)
        {
            return Vector3.Distance(LocalControlPointToWorldPosition(pathsLocalPoints[i]),
                LocalControlPointToWorldPosition(pathsLocalPoints[i + 1]));
        }
    }
}