using System;
using UnityEngine;
using System.Collections.Generic;
using PGSauce.Core.Extensions;
using PGSauce.Core.Utilities;
using PGSauce.Unity;

namespace PGSauce.Core.PGPhysics
{
    public class Parabola : PGMonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform end;
        [SerializeField] private LayerMask stopProjectileMask;
        [SerializeField] private float defaultGroundHeight = 0;
        [SerializeField] private float g = 9.81f;

        public LayerMask StopProjectileMask => stopProjectileMask;

        private void Awake()
        {
            ResetTransform();
        }

        private void ResetTransform()
        {
            transform.parent = null;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }

        [Serializable]
        public struct ParabolaData
        {
            public List<Vector3> WorldSpacePoints;
            public float WorldHeightBetweenStartAndEnd, WorldMaxHeight;
            public float Velocity, Vx, Vy, FlightTime;
            public Vector3 WorldParabolaSummit;

            public ParabolaData(List<Vector3> worldSpacePoints, float worldHeightBetweenStartAndEnd, float worldMaxHeight, float velocity, float vx, float vy, float flightTime, Vector3 worldParabolaSummit)
            {
                WorldSpacePoints = worldSpacePoints;
                WorldHeightBetweenStartAndEnd = worldHeightBetweenStartAndEnd;
                WorldMaxHeight = worldMaxHeight;
                Velocity = velocity;
                Vx = vx;
                Vy = vy;
                FlightTime = flightTime;
                WorldParabolaSummit = worldParabolaSummit;
            }
        }

        public ParabolaData GetParabolaWithRange(Transform transformToCopy, float range, int resolution, bool testCollision = false, bool refineIfCollision = true)
        {
            _angle = -transformToCopy.eulerAngles.x;
            //transform.parent = transformToCopy.parent;
            transform.position = transformToCopy.position;
            
            start.localPosition = Vector3.zero;
            start.localRotation = Quaternion.identity;
            end.localRotation = Quaternion.identity;

            transform.eulerAngles = new Vector3(0, transformToCopy.eulerAngles.y, 0);
            var globalForward = transform.forward;
            var localForward = transform.InverseTransformDirection(globalForward);
            
            var endTransform = end;
            var localPos = start.localPosition +  localForward * range;
            endTransform.localPosition = localPos;

            var position = endTransform.position;
            var worldPos = position;
            worldPos.y = defaultGroundHeight;

           position = worldPos;
           endTransform.position = position;

           _height = transform.position.y - defaultGroundHeight;// - position.y;
            
            _worldPositions = new List<Vector3>();
            
            var alpha = _angle * Mathf.Deg2Rad;
            var localPosition = endTransform.localPosition;
            var ex = localPosition.z;
            var ey = localPosition.y;
            
            var b = Mathf.Tan(alpha);
            var c = 0;

            _a = (ey - b * ex - c) / (ex * ex);
            var a = _a;

            _vx = Mathf.Sqrt(-g / (2 * a));

            Func<float, float> parabola = x => a * x * x + b * x + c;

            var summitZ = -b / (2 * a);
            var summitY = parabola(summitZ);
            
            _summit = transform.TransformPoint(new Vector3(0, summitY, summitZ));
            _heightMax = _summit.y;

            _vy = Mathf.Sqrt((_heightMax - _height) * 2 * g);

            _velocity = new Vector2(_vx, _vy).magnitude;

            _flightTime = (_vy + Mathf.Sqrt(_vy * _vy + 2 * g * _height)) / g;
            
            for (var i = 0; i < resolution; i++)
            {
                var t = (float) i / (resolution - 1);
                var local = Vector3.Lerp(start.localPosition, localPos, t);
                var localZ = local.z;
                var localY = parabola(localZ);
                local.y = localY;

                var world = transform.TransformPoint(local);
                _worldPositions.Add(world);
            }
            
            var keptWorldPos = new List<Vector3>();

            if (testCollision)
            {
                for (var i = 0; i < _worldPositions.Count - 1; i++)
                {
                    var pos1 = _worldPositions[i];
                    keptWorldPos.Add(pos1);
                    var pos2 = _worldPositions[i + 1];
                    var direction = pos2 - pos1;
                    direction = direction.normalized;

                    var ray = new Ray(pos1, direction);
                    var hit = Physics.Raycast(ray, out var hitInfo, Vector3.Distance(pos1, pos2), StopProjectileMask);
                    if (hit)
                    {
                        pos2 = hitInfo.point;
                        keptWorldPos.Add(pos2);
                        if (refineIfCollision)
                        {
                            keptWorldPos.Clear();
                            var maxT = (float) i / (resolution - 1);
                            for (var j = 0; j < resolution; j++)
                            {
                                var t = (float) j / (resolution - 1);
                                t = t.Remap(0, 1f, 0, maxT);
                                var local = Vector3.Lerp(start.localPosition, localPos, t);
                                var localZ = local.z;
                                var localY = parabola(localZ);
                                local.y = localY;

                                var world = transform.TransformPoint(local);
                                keptWorldPos.Add(world);
                            }
                        }

                        break;
                    }

                    if (i == _worldPositions.Count - 2)
                    {
                        keptWorldPos.Add(pos2);
                    }
                }
            }
            else
            {
                keptWorldPos = _worldPositions;
            }

            //ResetTransform();

            return new ParabolaData(keptWorldPos, _height, _heightMax, _velocity, _vx, _vy, _flightTime, _summit);
        }

        private float _height, _heightMax;
        
        private float _velocity, _vx, _vy, _flightTime;

        private Vector3 _summit;

        private List<Vector3> _worldPositions;

        private float _angle;

        private float _a;
    }
}
