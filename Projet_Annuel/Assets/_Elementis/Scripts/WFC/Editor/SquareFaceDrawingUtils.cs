using System;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public static class SquareFaceDrawingUtils
    {
        public static Vector2Int ToSubFaceVector(SubFace subface)
        {
            var i = (int)subface;
            return new Vector2Int((i % 3) - 1, (i / 3) - 1);
            throw new Exception();
        }

        private static SubFace FromSubFaceVector(Vector2Int p)
        {
            return (SubFace)p.x + p.y * 3 + 4;
        }

        public static Vector3[] GetSubFaceVertices(SubFace subface, Vector3 faceCenter, Vector3 halfUp, Vector3 halfRight)
        {
            var p = ToSubFaceVector(subface);

            var corner = faceCenter + (-1 + (p.y + 1) * 2.0f / 3.0f) * halfUp + (-1 + (p.x + 1) * 2.0f / 3.0f) * halfRight;
            var v1 = corner;
            var v2 = corner + 2.0f / 3.0f * halfRight;
            var v4 = corner + 2.0f / 3.0f * halfUp;
            var v3 = corner + +2.0f / 3.0f * halfRight + 2.0f / 3.0f * halfUp;

            return new[] { v1, v2, v3, v4 };
        }

        public static Vector3[] GetFaceVertices(Vector3 faceCenter, Vector3 halfUp, Vector3 halfRight)
        {
            var corner = faceCenter + -1 * halfUp + -1 * halfRight;
            var v1 = corner;
            var v2 = corner + 2.0f * halfRight;
            var v4 = corner + 2.0f * halfUp;
            var v3 = corner + 2.0f * halfRight + 2.0f * halfUp;

            return new[] { v1, v2, v3, v4 };
        }



        public static SubFace RoundSubFace(Vector2 p2, PaintMode paintMode)
        {
            switch (paintMode)
            {
                case PaintMode.Vertex:
                    return FromSubFaceVector(new Vector2Int(
                        (int)(p2.x + 1) * 2 - 1,
                        (int)(p2.y + 1) * 2 - 1));
                case PaintMode.Edge:
                    if (Math.Abs(p2.x) > Math.Abs(p2.y))
                    {
                        return FromSubFaceVector(new Vector2Int(Math.Sign(p2.x), 0));
                    }
                    else
                    {
                        return FromSubFaceVector(new Vector2Int(0, Math.Sign(p2.y)));
                    }
                default:
                    return FromSubFaceVector(new Vector2Int(
                        (int)((p2.x + 1) / 2.0f * 3.0f) - 1,
                        (int)((p2.y + 1) / 2.0f * 3.0f) - 1));
            }
        }
    }
}