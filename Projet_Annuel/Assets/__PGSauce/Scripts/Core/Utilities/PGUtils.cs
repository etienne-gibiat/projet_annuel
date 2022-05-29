using UnityEngine;

namespace PGSauce.Core.Utilities
{
    public static class PGUtils
    {
        public static Vector3 RandomVector3(float xRange, float yRange, float zRange, float xOffset = 0, float yOffset = 0, float zOffset = 0)
        {
            if (xOffset < 0)
            {
                xOffset *= -1;
            }
            if (yOffset < 0)
            {
                yOffset *= -1;
            }
            if (zOffset < 0)
            {
                zOffset *= -1;
            }
            return RandomVector3(
                new MinMax<Vector3>()
                    {min = new Vector3(-xRange, -yRange, -zRange), max = new Vector3(xRange, yRange, zRange)},
                new Vector3(xOffset, yOffset, zOffset));
        }

            public static Vector3 RandomVector3(MinMax<Vector3> minMax, Vector3 offset)
            {
                var vec = new Vector3(Random.Range(minMax.min.x, minMax.max.x), Random.Range(minMax.min.y, minMax.max.y), Random.Range(minMax.min.z, minMax.max.z));
                return vec + offset;
            }
            
            public static Vector3 RandomVector3(MinMax<Vector3> minMax)
            {
                return RandomVector3(minMax, Vector3.zero);
            }

            /// <summary>
            /// Knowing the three axes of a point, get the corresponding quaternion
            /// </summary>
            /// <param name="right"></param>
            /// <param name="up"></param>
            /// <param name="forward"></param>
            /// <returns></returns>
            public static Quaternion GetRotationWithWorldAxes(Vector3 right, Vector3 up, Vector3 forward)
            {
                var mat = new Matrix4x4();
                mat.SetColumn(0, right);
                mat.SetColumn(1, up);
                mat.SetColumn(2, forward);
                return QuaternionFromMatrix(mat);
            }
            
            public static Quaternion QuaternionFromMatrix(Matrix4x4 m) {
                // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
                Quaternion q = new Quaternion();
                q.w = Mathf.Sqrt( Mathf.Max( 0, 1 + m[0,0] + m[1,1] + m[2,2] ) ) / 2;
                q.x = Mathf.Sqrt( Mathf.Max( 0, 1 + m[0,0] - m[1,1] - m[2,2] ) ) / 2;
                q.y = Mathf.Sqrt( Mathf.Max( 0, 1 - m[0,0] + m[1,1] - m[2,2] ) ) / 2;
                q.z = Mathf.Sqrt( Mathf.Max( 0, 1 - m[0,0] - m[1,1] + m[2,2] ) ) / 2;
                q.x *= Mathf.Sign( q.x * ( m[2,1] - m[1,2] ) );
                q.y *= Mathf.Sign( q.y * ( m[0,2] - m[2,0] ) );
                q.z *= Mathf.Sign( q.z * ( m[1,0] - m[0,1] ) );
                return q;
            }
    }
}