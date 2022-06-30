using System;
using System.Collections.Generic;
using System.Linq;
using EasyCurvedLine;
using PGSauce.Core;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using PGSauce.Unity;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace _Elementis.Scripts.BezierLines
{
    public class BezierLine : PGMonoBehaviour
    {
        public Float01 progression = 1f;
        public List<Vector3> controlPoints;
        public LineRenderer line;
        public float smoothingLength = 2f;
        public Transform lookAt;
        [SerializeField] private float debugRadius = 0.5f;
        private Vector3[] _points;

        public void UpdateBezier(float f)
        {
            progression = f;
            Generate();
        }

        private void Awake()
        {
            GetSmoothedPoints();
            UpdateBezier(0f);
        }

        [Button]
        public void Generate()
        {
            if (_points == null)
            {
                GetSmoothedPoints();
            }
            var totalDistance = GetTotalDistance(_points);
            var targetDistance = progression * totalDistance;
            var currentDistance = 0f;

            var pointsChecked = new List<Vector3>();
            for (var i = 0; i < _points.Length - 1; i++)
            {
                var distanceCurrToNext = Vector3.Distance(_points[i],
                    _points[i + 1]);
                if (currentDistance + distanceCurrToNext < targetDistance)
                {
                    pointsChecked.Add(_points[i]);
                    currentDistance += distanceCurrToNext;
                }
                else
                {
                    pointsChecked.Add(_points[i]);
                    var t = (targetDistance - currentDistance) / distanceCurrToNext;
                    pointsChecked.Add(
                        Vector3.Lerp(_points[i], _points[i + 1], t));
                    break;
                }
            }
            
            line.positionCount = pointsChecked.Count;
            line.SetPositions(pointsChecked.ToArray());
            lookAt.transform.position = pointsChecked.Count >= 1 ? transform.GetWorldPosition(line.GetPosition(pointsChecked.Count - 1)) : transform.position;
        }

        private void GetSmoothedPoints()
        {
            _points = LineSmoother.SmoothLine(controlPoints.ToArray(), smoothingLength);
        }

        private float GetTotalDistance(Vector3[] points)
        {
            var dist = 0f;
            for (int i = 0; i < points.Length - 1; i++)
            {
                dist += Vector3.Distance(points[i],
                    points[i + 1]);
            }

            return dist;
        }

        public Vector3 GetControlPointWorldPos(int index)
        {
            return transform.GetWorldPosition(controlPoints[index]);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = PgColors.Redish;
            for (var index = 0; index < controlPoints.Count; index++)
            {
                Gizmos.DrawSphere(GetControlPointWorldPos(index), debugRadius);
            }
        }

        
    }
}