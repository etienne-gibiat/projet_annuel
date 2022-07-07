using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    
    public class CubeCellDrawing : ICellDrawing
    {
        public static CubeCellDrawing Instance = new CubeCellDrawing();

        public bool Is2D => false;

        public Vector3[] GetSubFaceVertices(Vector3Int offset, CellFaceDirection dir, SubFace subface, Vector3 center, Vector3 tileSize)
        {
            var u = new DrawingUtil(center, tileSize);

            var up = ((CellFaceDirection)dir).Up();
            GetFaceCenterAndNormal(offset, dir, center, tileSize, out var faceCenter, out var forward);
            var right = Vector3.Cross(forward, up);

            return SquareFaceDrawingUtils.GetSubFaceVertices(subface, faceCenter, u.ScaleByHSize(up), u.ScaleByHSize(right));
        }


        public Vector3[] GetFaceVertices(Vector3Int offset, CellFaceDirection dir, Vector3 center, Vector3 tileSize)
        {
            var u = new DrawingUtil(center, tileSize);

            var up = ((CellFaceDirection)dir).Up();
            GetFaceCenterAndNormal(offset, dir, center, tileSize, out var faceCenter, out var forward);
            var right = Vector3.Cross(forward, up);

            return SquareFaceDrawingUtils.GetFaceVertices(faceCenter, u.ScaleByHSize(up), u.ScaleByHSize(right));

        }

        public void GetFaceCenterAndNormal(Vector3Int offset, CellFaceDirection dir, Vector3 center, Vector3 tileSize, out Vector3 faceCenter, out Vector3 faceNormal)
        {
            var u = new DrawingUtil(center, tileSize);

            var forward = ((CellFaceDirection)dir).Forward();

            faceCenter = center + u.ScaleBySize(offset) + u.ScaleByHSize(forward);
            faceNormal = forward;
        }


        public RaycastCellHit Raycast(Ray ray, Vector3Int offset, Vector3 center, Vector3 tileSize, float? minDistance, float? maxDistance)
        {
            var u = new DrawingUtil(center, tileSize);

            var currentHit = Raycast(center + u.ScaleBySize(offset), u.hsize, ray, out var currentHitCellDir, out var currentHitPoint);


            if (!currentHit)
            {
                return null;
            }

            var currentHitDir = (CellFaceDirection)currentHitCellDir;

            var d2 = (currentHitPoint - ray.origin).sqrMagnitude;

            if (maxDistance != null && d2 >= maxDistance * maxDistance)
            {
                return null;
            }

            if (minDistance != null && d2 <= minDistance * minDistance)
            {
                return null;
            }

            var hit = new RaycastCellHit
            {
                dir = currentHitCellDir,
                point = currentHitPoint,
            };

            var up = currentHitDir.Up();
            var forward = currentHitDir.Forward();
            var right = Vector3.Cross(forward, up);

            var p = currentHitPoint - (center + u.ScaleBySize(offset) + u.ScaleByHSize(forward));
            p = new Vector3(p.x / u.hsize.x, p.y / u.hsize.y, p.z / u.hsize.z);
            var p2 = new Vector2(Vector3.Dot(p, right), Vector3.Dot(p, up));
            hit.subface = p2;
            return hit;
        }

        // Casts a ray at an axis aligned box, and reutrns which face and where the hit is.
        private bool Raycast(Vector3 center, Vector3 hsize, Ray ray, out CellFaceDirection faceDir, out Vector3 point)
        {
            var dir = ray.direction.normalized;
            // r.dir is unit direction vector of ray
            var dirfrac = new Vector3(1 / dir.x, 1 / dir.y, 1 / dir.z);
            // lb is the corner of AABB with minimal coordinates - left bottom, rt is maximal corner
            // r.org is origin of ray
            Vector3 t1 = Vector3.Scale(center - hsize - ray.origin, dirfrac);
            Vector3 t2 = Vector3.Scale(center + hsize - ray.origin, dirfrac);

            var t3 = Vector3.Min(t1, t2);
            float tmin = Max(t3);
            float tmax = Min(Vector3.Max(t1, t2));

            float t;
            // if tmax < 0, ray (line) is intersecting AABB, but the whole AABB is behind us
            if (tmax < 0)
            {
                t = tmax;
                faceDir = default;
                point = default;
                return false;
            }

            // if tmin > tmax, ray doesn't intersect AABB
            if (tmin > tmax)
            {
                t = tmax;
                faceDir = default;
                point = default;
                return false;
            }

            t = tmin;
            point = ray.origin + t * dir;
            faceDir = tmin == t3.x ? (dir.x > 0 ? (CellFaceDirection)CellFaceDirection.Left : (CellFaceDirection)CellFaceDirection.Right)
                : tmin == t3.y ? (dir.y > 0 ? (CellFaceDirection)CellFaceDirection.Down : (CellFaceDirection)CellFaceDirection.Up)
                : (dir.z > 0 ? (CellFaceDirection)CellFaceDirection.Back : (CellFaceDirection)CellFaceDirection.Forward);
            return true;
        }

        public SubFace RoundSubFace(Vector3Int offset, CellFaceDirection dir, Vector2 p2, PaintMode paintMode)
        {
            return SquareFaceDrawingUtils.RoundSubFace(p2, paintMode);
        }

        public bool IsAffected(Vector3Int parentOffset, CellFaceDirection parentFaceDir, SubFace parentSubface, Vector3Int childOffset, CellFaceDirection childFaceDir, SubFace childSubface, PaintMode paintMode)
        {
            var p2 = SquareFaceDrawingUtils.ToSubFaceVector(parentSubface);
            var p1 = SquareFaceDrawingUtils.ToSubFaceVector(childSubface);


            var up = ((CellFaceDirection)childFaceDir).Up();
            var forward = ((CellFaceDirection)childFaceDir).Forward();
            var right = Vector3.Cross(forward, up);


            var up2 = ((CellFaceDirection)parentFaceDir).Up();
            var forward2 = ((CellFaceDirection)parentFaceDir).Forward();
            var right2 = Vector3.Cross(forward2, up2);

            Vector3 v1, v2;

            switch (paintMode)
            {
                case PaintMode.Pencil:
                    return childOffset == parentOffset && childFaceDir == parentFaceDir && p1 == p2;
                case PaintMode.Vertex:
                    v1 = childOffset * 2 + forward + right * p1.x + up * p1.y;
                    v2 = parentOffset * 2 + forward2 + right2 * p2.x + up2 * p2.y;
                    return (v1 - v2).sqrMagnitude < 0.01;
                case PaintMode.Edge:
                    var v = forward2 + p2.x * right2 + p2.y * (Vector3)up2;
                    var isX = Math.Abs(Vector3.Dot(v, right));
                    var isY = Math.Abs(Vector3.Dot(v, up));
                    v1 = childOffset * 2 + forward + right * p1.x * isX + (Vector3)up * p1.y * isY;
                    v2 = parentOffset * 2 + forward2 + right2 * p2.x + up2 * p2.y;
                    return (v1 - v2).sqrMagnitude < 0.01;
                case PaintMode.Face:
                case PaintMode.Add:
                    return childOffset == parentOffset && childFaceDir == parentFaceDir;
                case PaintMode.Remove:
                    return childOffset == parentOffset;
            }
            throw new Exception();
        }


        public IEnumerable<SubFace> GetSubFaces(Vector3Int offset, CellFaceDirection dir)
        {
            yield return SubFace.BottomLeft;
            yield return SubFace.Bottom;
            yield return SubFace.BottomRight;
            yield return SubFace.Left;
            yield return SubFace.Center;
            yield return SubFace.Right;
            yield return SubFace.TopLeft;
            yield return SubFace.Top;
            yield return SubFace.TopRight;
        }

        public void SetSubFaceValue(WfcFaceDetails faceDetails, SubFace subface, int value)
        {
            switch (subface)
            {
                case SubFace.BottomLeft: faceDetails.bottomLeft = value; break;
                case SubFace.Bottom: faceDetails.bottom = value; break;
                case SubFace.BottomRight: faceDetails.bottomRight = value; break;
                case SubFace.Left: faceDetails.left = value; break;
                case SubFace.Center: faceDetails.center = value; break;
                case SubFace.Right: faceDetails.right = value; break;
                case SubFace.TopLeft: faceDetails.topLeft = value; break;
                case SubFace.Top: faceDetails.top = value; break;
                case SubFace.TopRight: faceDetails.topRight = value; break;
            }
        }

        public int GetSubFaceValue(WfcFaceDetails faceDetails, SubFace subface)
        {
            switch (subface)
            {
                case SubFace.BottomLeft: return faceDetails.bottomLeft;
                case SubFace.Bottom: return faceDetails.bottom;
                case SubFace.BottomRight: return faceDetails.bottomRight;
                case SubFace.Left: return faceDetails.left;
                case SubFace.Center: return faceDetails.center;
                case SubFace.Right: return faceDetails.right;
                case SubFace.TopLeft: return faceDetails.topLeft;
                case SubFace.Top: return faceDetails.top;
                case SubFace.TopRight: return faceDetails.topRight;
            }
            return 0;
        }

        public Vector3Int Move(Vector3Int offset, CellFaceDirection dir)
        {
            return offset + ((CellFaceDirection)dir).Forward();
        }

        private static float Max(Vector3 v)
        {
            return Mathf.Max(v.x, v.y, v.z);
        }

        private static float Min(Vector3 v)
        {
            return Mathf.Min(v.x, v.y, v.z);
        }

        private struct DrawingUtil
        {
            public Vector3 center;
            public Vector3 size;
            public Vector3 hsize;

            public DrawingUtil(Vector3 center, Vector3 tileSize)
            {
                this.center = center;
                this.size = tileSize;
                this.hsize = size * 0.5f;
            }


            public Vector3 ScaleByHSize(Vector3 v)
            {
                return new Vector3(v.x * hsize.x, v.y * hsize.y, v.z * hsize.z);
            }

            public Vector3 ScaleBySize(Vector3 v)
            {
                return new Vector3(v.x * size.x, v.y * size.y, v.z * size.z);
            }
        }
    }
}