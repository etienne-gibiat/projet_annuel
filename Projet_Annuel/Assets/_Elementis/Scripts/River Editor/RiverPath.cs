using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace _Elementis.Scripts.River_Editor
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public class RiverPath : MonoBehaviour
    {
        private const string Basic = "Basic";
        private const string Points = "Points";
        [BoxGroup(Basic)]
        public Float01 riverProgression = 1f;
        [BoxGroup(Basic), Range(1, 100)]
        public int U = 8;
        [BoxGroup(Basic), SerializeField, Range(2, 20)] private int vertsInShape = 7;
        [BoxGroup(Basic), SerializeField] private bool receiveShadows;
        [BoxGroup(Basic), SerializeField] private ShadowCastingMode shadowCastingMode;
        [BoxGroup(Basic), SerializeField] private float uvScale = 2f;
        public AnimationCurve flowFlat = new AnimationCurve(new Keyframe[] {
            new Keyframe (0, 0.025f),
            new Keyframe (0.5f, 0.05f),
            new Keyframe (1, 0.025f)
        });
        public AnimationCurve flowWaterfall = new AnimationCurve(new Keyframe[] {
            new Keyframe (0, 0.25f),
            new Keyframe (1, 0.25f)
        });
        public float minVal = 0.5f;
        public float maxVal = 0.5f;

        [BoxGroup(Points)] 
        public List<ControlPoint> controlPoints = new List<ControlPoint>();

        [SerializeField] private float debugRadius = 0.25f;

        public MeshFilter meshFilter;
        public MeshRenderer meshRenderer;
        
        public List<Vector2> colorsFlowMap = new List<Vector2>();

        [ShowInInspector, ReadOnly]
        private PointData _pointDatas;
        
        private float _length;
        private float _fulllength;
        private float _minMaxWidth;
        private float _uvWidth;
        private float _uvBegining;
        private float _uv3length;
        [SerializeField] private Color[] colors;

        [ShowInInspector]
        private float TriangleDensity => 1.0f / U;

        private void Awake()
        {
            meshFilter = meshFilter == null ? GetComponent<MeshFilter>() : meshFilter;
            meshRenderer = meshRenderer == null ? GetComponent<MeshRenderer>() : meshRenderer;
        }

        [Button]
        public void GenerateRiver()
        {
            var points = GetRealTimeControlPoints();
            if (meshFilter == null)
            {
                meshFilter = GetComponent<MeshFilter>();
            }
            
            var mesh = new Mesh();
            if (points.Count < 2)
            {
                mesh.Clear();
                meshFilter.mesh = mesh;
                return;
            }

            _pointDatas = new PointData();

            for (int i = 0; i < points.Count; i++)
            {
                if (i > points.Count - 2)
                {
                    continue;
                }

                CalculateSideSplines(points, i);
            }
            
            for (int i = 0; i < points.Count; i++)
            {

                if (i > points.Count - 2)
                {
                    continue;
                }

                CalculateCatmullRomSplineParameters(points, i);
            }

            for (int i = 0; i < _pointDatas.controlPointUps.Count; i++)
            {
                if (i > _pointDatas.controlPointUps.Count - 2)
                {
                    continue;
                }

                CalculateCatmullRomSpline(_pointDatas, i);
            }
            
            GenerateMesh(ref mesh);
        }

        private void GenerateMesh(ref Mesh mesh)
        {
            if (meshRenderer != null)
            {
                meshRenderer.receiveShadows = receiveShadows;
                meshRenderer.shadowCastingMode = shadowCastingMode;
            }

            var segments = _pointDatas.orientations.Count - 1;
            var edgeLoops = segments + 1;
            var vertCount = vertsInShape * edgeLoops;
            
            var triangleIndices = new List<int>();
            var vertices = new Vector3[vertCount];
            var normals = new Vector3[vertCount];
            var uvs = new Vector2[vertCount];
            var uvs3 = new Vector2[vertCount];
            var uvs4 = new Vector2[vertCount];
            
            if (colors == null || colors.Length != vertCount)
            {
                colors = new Color[vertCount];
                for (int i = 0; i < colors.Length; i++)
                {
                    colors[i] = Color.black;
                }
            }

            if (colorsFlowMap.Count != vertCount)
            {
                colorsFlowMap.Clear();
            }
            
            _length = 0;
            _fulllength = 0;

            _minMaxWidth = 1f;
            _uvWidth = 1f;
            _uvBegining = 0f;
            
            for (int i = 0; i < _pointDatas.pointDowns.Count; i++)
            {
                var width = _pointDatas.widths[i];
                if (i > 0)
                {
                    _fulllength += _uvWidth * Vector3.Distance(_pointDatas.pointDowns[i], _pointDatas.pointDowns[i - 1]) / (float)(uvScale * width);
                }
            }
            
            var roundEnding = Mathf.Round(_fulllength);
            for (var i = 0; i < _pointDatas.pointDowns.Count; i++)
            {
                var offset = i * vertsInShape;
                var width = _pointDatas.widths[i];
                if (i > 0)
                {
                    _length += _uvWidth * Vector3.Distance(_pointDatas.pointDowns[i], _pointDatas.pointDowns[i - 1]) / (float)(uvScale * width) / _fulllength * roundEnding;
                }
                
                float u = 0;
                float u3 = 0;
                for (int j = 0; j < vertsInShape; j++)
                {
                    var id = CreateVertices(offset, j, vertices, i, out var pos);
                    CreateNormals(normals, id, i);
                    u = CreateUVs(j, u, pos, normals, id, uvs, uvs3, uvs4, ref u3);
                }
            }
            
            CreateTriangles(segments, triangleIndices);
            
            mesh = new Mesh();
            mesh.Clear();
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.uv = uvs;
            mesh.uv3 = uvs3;
            mesh.uv4 = colorsFlowMap.ToArray();

            mesh.triangles = triangleIndices.ToArray();
            mesh.colors = colors;
            mesh.RecalculateTangents();
            meshFilter.mesh = mesh;
            meshRenderer.enabled = true;
        }

        private void CreateTriangles(int segments, List<int> triangleIndices)
        {
            for (int i = 0; i < segments; i++)
            {
                int offset = i * vertsInShape;
                for (int l = 0; l < vertsInShape - 1; l += 1)
                {
                    int a = offset + l;
                    int b = offset + l + vertsInShape;
                    int c = offset + l + 1 + vertsInShape;
                    int d = offset + l + 1;
                    triangleIndices.Add(a);
                    triangleIndices.Add(b);
                    triangleIndices.Add(c);
                    triangleIndices.Add(c);
                    triangleIndices.Add(d);
                    triangleIndices.Add(a);
                }
            }
        }

        private int CreateVertices(int offset, int j, Vector3[] vertices, int i, out float pos)
        {
            var id = offset + j;
            pos = j / (float) (vertsInShape - 1);
            if (pos < 0.5f)
            {
                pos *= minVal * 2;
            }
            else
            {
                pos = ((pos - 0.5f) * (1 - maxVal) + 0.5f * maxVal) * 2;
            }

            vertices[id] = Vector3.Lerp(_pointDatas.pointDowns[i], _pointDatas.pointUps[i], pos);
            
            
            //SNAPPING
            /*
            if (Physics.Raycast(vertices[id] + transform.position + Vector3.up * 5, Vector3.down, out var hit, 1000, snapMask.value))
            {

                vertices[id] = Vector3.Lerp(vertices[id], hit.point - transform.position + new Vector3(0, 0.1f, 0), (Mathf.Sin(Mathf.PI * snaps[i] - Mathf.PI * 0.5f) + 1) * 0.5f);

            }*/
            
            return id;
        }

        private void CreateNormals(Vector3[] normals, int id, int i)
        {
            normals[id] = _pointDatas.orientations[i] * Vector3.up;
        }

        private float CreateUVs(int j, float u, float pos, Vector3[] normals, int id, Vector2[] uvs, Vector2[] uvs3,
            Vector2[] uvs4, ref float u3)
        {
            if (j > 0)
            {
                u = (pos) * _uvWidth;
                u3 = pos;
            }

            u /= uvScale;

            var uv4U = FlowCalculate(u3, normals[id].y);
            var uv4V = -(u3 - 0.5f) * 0.01f;
            _uv3length = _length / _fulllength;

            uvs[id] = new Vector2(u, 1 - _length);
            uvs3[id] = new Vector2(u3, 1 - _uv3length);
            uvs4[id] = new Vector2(uv4V, uv4U);


            float tempRound = (int) (uvs4[id].x * 100);
            uvs4[id].x = tempRound * 0.01f;
            tempRound = (int) (uvs4[id].y * 100);
            uvs4[id].y = tempRound * 0.01f;


            if (colorsFlowMap.Count <= id)
            {
                colorsFlowMap.Add(uvs4[id]);
            }
            else
            {
                colorsFlowMap[id] = uvs4[id];
            }
            
            return u;
        }

        private float FlowCalculate(float u, float normalY)
        {
            return Mathf.Lerp(flowWaterfall.Evaluate(u), flowFlat.Evaluate(u), Mathf.Clamp(normalY, 0, 1));
        }

        private void CalculateCatmullRomSpline(PointData pointDatas, int pos)
        {
            var cPointsUp = new List<Vector3>(pointDatas.controlPointUps);
            CalculateAbstractCatmullRomData(cPointsUp, pos,
                (index, p0, p1, p2, p3, t) =>
                {
                    CalculatePointPosition(cPointsUp, pos, p0, p1, p2, p3, t, (i, position) =>
                    {
                        _pointDatas.pointUps.Add(position);
                    });
                });
            
            var cPointsDown = new List<Vector3>(pointDatas.controlPointDowns);
            CalculateAbstractCatmullRomData(cPointsDown, pos,
                (index, p0, p1, p2, p3, t) =>
                {
                    CalculatePointPosition(cPointsDown, pos, p0, p1, p2, p3, t, (i, position) =>
                    {
                        _pointDatas.pointDowns.Add(position);
                    });
                });
        }

        private void CalculatePointPosition(List<Vector3> points, int pos, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t, Action<int, Vector3> updateDataMethod)
        {
            var newPos = GetCatmullRomPosition(t, p0, p1, p2, p3);
            updateDataMethod(pos, newPos);
        }

        private void CalculateCatmullRomSplineParameters(List<ControlPoint> points, int pos)
        {
            CalculateAbstractCatmullRomData(points.Select(p => (Vector3) p.position).ToList(), pos,
                (index, p0, p1, p2, p3, t) =>
                {
                    CalculatePointParameters(points, pos, p0, p1, p2, p3, t);
                });
        }
        
        private void CalculateAbstractCatmullRomData(List<Vector3> points, int pos, Action<int, Vector3, Vector3, Vector3, Vector3, float> abstractCalculator)
        {
            var (p0, p1, p2, p3) = UtilityGetCatmullPositions(points, pos);
            var loops = Mathf.FloorToInt(1f / TriangleDensity);

            float i;
            float start = 0;
            if (pos > 0)
            {
                start = 1;
            }

            for (i = start; i <= loops; i++)
            {
                var t = i * TriangleDensity;
                abstractCalculator((int) (pos + i), p0, p1, p2, p3, t);
            }

            if (i < loops)
            {
                var t = loops * TriangleDensity;
                abstractCalculator(pos + loops, p0, p1, p2, p3, t);
            }
        }

        private void CalculatePointParameters(List<ControlPoint> points, int pos, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            if (pos >= points.Count)
            {
                Debug.Log("");
            }

            _pointDatas.widths.Add(Mathf.Lerp(points[pos].position.w, points[ClampListPos(pos + 1)].position.w, t));

            var tangent = GetCatmullRomTangent(t, p0, p1, p2, p3).normalized;
            var normal = CalculateNormal(tangent, Vector3.up).normalized;
            Quaternion orientation;
            if (normal.magnitude <= 0f && tangent.magnitude <= 0.0f)
            {
                orientation = Quaternion.identity;
            }
            else
            {
                orientation = Quaternion.LookRotation(tangent, normal);
            }

            orientation *= Quaternion.Lerp(Quaternion.Euler(points[pos].rotation),
                Quaternion.Euler(points[ClampListPos(pos + 1)].rotation), t);

            _pointDatas.orientations.Add(orientation);
            
            if (_pointDatas.normals.Count > 0 && Vector3.Angle(_pointDatas.normals.Last(), normal) > 90)
            {
                normal *= -1;
            }

            _pointDatas.normals.Add(normal);
        }

        private (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) UtilityGetCatmullPositions(List<ControlPoint> points, int pos)
        {
            return UtilityGetCatmullPositions(points.Select(p => (Vector3) p.position).ToList(), pos);
        }
        
        private (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3) UtilityGetCatmullPositions(List<Vector3> points, int pos)
        {
            Vector3 p0 = points[pos];
            Vector3 p1 = points[pos];
            Vector3 p2 = points[ClampListPos(pos + 1)];
            Vector3 p3 = points[ClampListPos(pos + 1)];

            if (pos + 1 < points.Count - 1)
            {
                if (pos > 0)
                {
                    p0 = points[ClampListPos(pos - 1)];
                }
            
                if (pos < points.Count - 2)
                {
                    p3 = points[ClampListPos(pos + 2)];
                }
            }

            return (p0, p1, p2, p3);
        }

        private void CalculateSideSplines(List<ControlPoint> points, int pos)
        {
            var (p0, p1, p2, p3) = UtilityGetCatmullPositions(points, pos);

            var tValueMax = 0;
            if (pos == points.Count - 2)
            {
                tValueMax = 1;
            }

            for (int tValue = 0; tValue <= tValueMax; tValue++)
            {
                var newPos = GetCatmullRomPosition(tValue, p0, p1, p2, p3);
                var tangent = GetCatmullRomTangent(tValue, p0, p1, p2, p3).normalized;
                var normal = CalculateNormal(tangent, Vector3.up).normalized;
                Quaternion orientation;
                if (normal.magnitude <= 0f && tangent.magnitude <= 0.0f)
                {
                    orientation = Quaternion.identity;
                }
                else
                {
                    orientation = Quaternion.LookRotation(tangent, normal);
                }

                orientation *= Quaternion.Lerp(Quaternion.Euler(points[pos].rotation),
                    Quaternion.Euler(points[ClampListPos(pos + 1)].rotation), tValue);

                _pointDatas.controlPointOrientations.Add(orientation);
                
                var posUp = newPos + orientation * (0.5f * points[pos + tValue].position.w * Vector3.right);
                var posDown = newPos + orientation * (0.5f * points[pos + tValue].position.w * Vector3.left);

                _pointDatas.controlPointUps.Add(posUp);
                _pointDatas.controlPointDowns.Add(posDown);
            }
        }
        
        private Vector3 CalculateNormal(Vector3 tangent, Vector3 up)
        {
            var binormal = Vector3.Cross(up, tangent);
            return Vector3.Cross(tangent, binormal);
        }

        private Vector3 GetCatmullRomTangent(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {
            return 0.5f * ((-p0 + p2) + 2f * (2f * p0 - 5f * p1 + 4f * p2 - p3) * t + 3f * (-p0 + 3f * p1 - 3f * p2 + p3) * t * t);
        }

        private Vector3 GetCatmullRomPosition(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        {

            var a = 2f * p1;
            var b = p2 - p0;
            var c = 2f * p0 - 5f * p1 + 4f * p2 - p3;
            var d = -p0 + 3f * p1 - 3f * p2 + p3;

            var pos = 0.5f * (a + (b * t) + (c * t * t) + (d * t * t * t));

            return pos;
        }
        
        private int ClampListPos(int pos)
        {
            if (pos < 0)
            {
                pos = controlPoints.Count - 1;
            }

            if (pos > controlPoints.Count)
            {
                pos = 1;
            }
            else if (pos > controlPoints.Count - 1)
            {
                pos = 0;
            }

            return pos;
        }

        private List<ControlPoint> GetRealTimeControlPoints()
        {
            if (riverProgression <= 0)
            {
                return new List<ControlPoint>();
            }

            if (riverProgression >= 1)
            {
                return new List<ControlPoint>(controlPoints);
            }

            var totalDistance = TotalDistance(controlPoints);
            var targetDistance = riverProgression * totalDistance;
            var currentDistance = 0f;

            var pointsChecked = new List<ControlPoint>();
            for (var i = 0; i < controlPoints.Count - 1; i++)
            {
                var distanceCurrToNext = Vector3.Distance(controlPoints[i].position,
                    controlPoints[i + 1].position);
                if (currentDistance + distanceCurrToNext < targetDistance)
                {
                    pointsChecked.Add(controlPoints[i]);
                    currentDistance += distanceCurrToNext;
                }
                else
                {
                    pointsChecked.Add(controlPoints[i]);
                    var t = (targetDistance - currentDistance) / distanceCurrToNext;
                    pointsChecked.Add(new ControlPoint()
                    {
                        position = Vector4.Lerp(controlPoints[i].position, controlPoints[i + 1].position, t),
                        rotation = Quaternion.Lerp(Quaternion.Euler(controlPoints[i].rotation), Quaternion.Euler(controlPoints[i + 1].rotation), t).eulerAngles
                    });
                    break;
                }
            }
            
            List<ControlPoint> points = new List<ControlPoint>();
            for (int i = 0; i < pointsChecked.Count; i++)
            {
                if (i > 0)
                {
                    if (Vector3.Distance(pointsChecked[i].position, pointsChecked[i - 1].position) > 0)
                    {
                        points.Add(pointsChecked[i]);
                    }

                }
                else
                {
                    points.Add(pointsChecked[i]);
                }
            }

            return points;
        }

        


        private float TotalDistance(IReadOnlyList<ControlPoint> cPoints)
        {
            var dist = 0f;
            for (int i = 0; i < cPoints.Count - 1; i++)
            {
                dist += Vector3.Distance(cPoints[i].position,
                    cPoints[i + 1].position);
            }

            return dist;
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Gizmos.color = Color.blue;
            for (var index = 0; index < controlPoints.Count; index++)
            {
                var controlPoint = controlPoints[index];
                Gizmos.DrawSphere(GetControlPointPosition(index), debugRadius * controlPoint.position.w);
            }

            Gizmos.color = Color.red;
            var points = GetRealTimeControlPoints();
            for (var index = 0; index < points.Count; index++)
            {
                var controlPoint = points[index];
                Gizmos.DrawSphere(GetControlPointPosition(points, index), debugRadius * controlPoint.position.w);
            }
#endif
        }

        private Vector3 GetControlPointPosition(List<ControlPoint> points, int index)
        {
            var local = points[index].position;
            return GetWorldPosition(local);
        }

        public Vector3 GetWorldPosition(Vector4 local)
        {
            return transform.GetWorldPosition(local);
        }

        private Vector3 GetControlPointPosition(int index)
        {
            return GetControlPointPosition(controlPoints, index);
        }

        [Serializable]
        public class PointData
        {
            public List<Quaternion> controlPointOrientations;
            public List<Vector3> controlPointUps;
            public List<Vector3> controlPointDowns;
            public List<float> widths;
            public List<Quaternion> orientations;
            public List<Vector3> normals;
            public List<Vector3> pointUps;
            public List<Vector3> pointDowns;

            public PointData()
            {
                controlPointOrientations = new List<Quaternion>();
                controlPointDowns = new List<Vector3>();
                controlPointUps = new List<Vector3>();
                widths = new List<float>();
                orientations = new List<Quaternion>();
                normals = new List<Vector3>();
                pointUps = new List<Vector3>();
                pointDowns = new List<Vector3>();
            }
        }
        
        [Serializable]
        public class ControlPoint
        {
            public Vector4 position = new Vector4(0,0,0,3);
            public Vector3 rotation;
        }

        public void UpdateRiver(Float01 value)
        {
            riverProgression = value;
            GenerateRiver();
        }
    }
}