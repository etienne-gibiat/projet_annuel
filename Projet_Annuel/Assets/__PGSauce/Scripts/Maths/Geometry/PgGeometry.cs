using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PGSauce.Geometry
{
    public static class PgGeometry
    {
        /// <summary>
        /// Where is p in relation to a-b
        /// positive -> to the right
        /// zero -> on the line
        /// negative -> to the left
        /// </summary>
        /// <param name="a">Point A of the line</param>
        /// <param name="b">Point B of the line</param>
        /// <param name="p">Tested point</param>
        /// <returns>A determinant</returns>
        public static float WhereIsPointInRelationToLine(Vector2 a, Vector2 b, Vector2 p)
        {
            float determinant = (a.x - p.x) * (b.y - p.y) - (a.y - p.y) * (b.x - p.x);

            return determinant;
        }
        
        /// <summary>
        /// Where is p in relation to line
        /// positive -> to the right
        /// zero -> on the line
        /// negative -> to the left
        /// </summary>
        /// <param name="line">Line</param>
        /// <param name="p">Tested point</param>
        /// <returns>A determinant</returns>
        public static float WhereIsPointInRelationToLine(this Line line, Vector2 p)
        {
            return WhereIsPointInRelationToLine(line.A, line.B, p);
        }

        public static Vector3 GetRandomPointInsidePolygon(Polygon polygon)
        {
            //https://dev.to/bogdanalexandru/generating-random-points-within-a-polygon-in-unity-nce
            var triangle = PickBiasedRandomTriangle(polygon);
            return triangle.RandomPoint();
        }

        public static Triangle PickBiasedRandomTriangle(Polygon polygon)
        {
            var polyArea = polygon.Area();
            var rng = Random.Range(0f, polyArea);
            for (var i = 0; i < polygon.Triangles.Count; ++i)
            {
                // TriArea() is an extension method
                // that uses the cross product formula
                // to calculate the triangle's area.
                if (rng < polygon.Triangles[i].Area())
                {
                    return polygon.Triangles[i];
                }
                rng -= polygon.Triangles[i].Area();
            }
            // Should normally not get here
            // so this is not 100% correct.
            // You'd need to consider floating
            // point arithmetic imprecision.
            //
            // But this is a good compromise 
            // that nonetheless gives good 
            // results.
            return polygon.Triangles.Last();
        }
    }
}
