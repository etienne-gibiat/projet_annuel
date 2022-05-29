using UnityEngine;

namespace Jobberwocky.GeometryAlgorithms.Source.Core
{
    class Utils
    {
        public static Vector3 ToCoordinateSystemDefault(Vector3 vector, CoordinateSystem coordinateSystem)
        {
            var newVector = new Vector3();

            switch (coordinateSystem)
            {
                case CoordinateSystem.XZY:
                    newVector = new Vector3(vector.x, vector.z, vector.y);
                    break;
                case CoordinateSystem.YXZ:
                    newVector = new Vector3(vector.y, vector.x, vector.z);
                    break;
                case CoordinateSystem.YZX:
                    newVector = new Vector3(vector.z, vector.x, vector.y);
                    break;
                case CoordinateSystem.ZXY:
                    newVector = new Vector3(vector.y, vector.z, vector.x);
                    break;
                case CoordinateSystem.ZYX:
                    newVector = new Vector3(vector.z, vector.y, vector.x);
                    break;
                case CoordinateSystem.XYZ:
                default:
                    newVector = vector;
                    break;
            }

            return newVector;
        }
        
        public static Vector3 FromCoordinateSystemDefaultTo(Vector3 vector, CoordinateSystem coordinateSystem)
        {
            var newVector = new Vector3();

            switch (coordinateSystem)
            {
                case CoordinateSystem.XZY:
                    newVector = new Vector3(vector.x, vector.z, vector.y);
                    break;
                case CoordinateSystem.YXZ:
                    newVector = new Vector3(vector.y, vector.x, vector.z);
                    break;
                case CoordinateSystem.YZX:
                    newVector = new Vector3(vector.y, vector.z, vector.x);
                    break;
                case CoordinateSystem.ZXY:
                    newVector = new Vector3(vector.z, vector.x, vector.y);
                    break;
                case CoordinateSystem.ZYX:
                    newVector = new Vector3(vector.z, vector.y, vector.x);
                    break;
                case CoordinateSystem.XYZ:
                default:
                    newVector = vector;
                    break;
            }

            return newVector;
        }
    }
}
