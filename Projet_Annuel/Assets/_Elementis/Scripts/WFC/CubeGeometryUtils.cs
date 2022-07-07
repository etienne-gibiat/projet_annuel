using System;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class CubeGeometryUtils
    {
        /// <summary>
        /// Given a FaceDetails on given face of the cube,
        /// rotates the cube, and returns the new face and correctly oriented FaceDetails
        /// </summary>
        internal static (CellFaceDirection, WfcFaceDetails) RotateBy(CellFaceDirection faceDir, WfcFaceDetails faceDetails, MatrixInt3x3 rotator)
        {
            var rotatedFaceDirForward = rotator.Multiply(faceDir.Forward());
            var rotatedFaceDirUp = rotator.Multiply(faceDir.Up());
            var rotatedFaceDirRight = rotator.Multiply(Vector3.Cross(faceDir.Forward(), faceDir.Up()));
            var rotatedFaceDir = FromNormal(rotatedFaceDirForward);
            var trueUp = rotatedFaceDir.Up();
            var trueForward = rotatedFaceDirForward; // =  rotatedFaceDir.Forward();
            var trueRight = Vector3.Cross(trueForward, trueUp);
            // Find the rotation that will map rotatedFaceDirUp to trueUp
            // and rotatedFaceDirRight to trueRight
            var dot = Vector3.Dot(rotatedFaceDirUp, trueUp);
            var cross = Vector3.Dot(rotatedFaceDirUp, trueRight);
            Rotation faceRot;
            if (dot == 1)
            {
                faceRot = new Rotation();
            }
            else if (dot == -1)
            {
                faceRot = new Rotation(180);
            }
            else if (cross == 1)
            {
                faceRot = new Rotation(270);
            }
            else if (cross == -1)
            {
                faceRot = new Rotation(90);
            }
            else
            {
                throw new Exception();
            }
            if (Vector3.Dot(Vector3.Cross(rotatedFaceDirForward, rotatedFaceDirUp), rotatedFaceDirRight) < 0)
            {
                faceRot = new Rotation(360 - faceRot.RotateCw, true);
            }
            faceRot = faceRot.Inverse();
            var rotatedFaceDetails = RotateBy(faceDetails, faceRot);

            return (rotatedFaceDir, rotatedFaceDetails);
        }
        
        internal static WfcFaceDetails RotateBy(WfcFaceDetails faceDetails, Rotation r)
        {
            var c = faceDetails.Clone();
            if (r.ReflectX)
            {
                c.ReflectX();
            }
            for (var i = 0; i < r.RotateCw / 90; i++)
            {
                c.RotateCw();
            }
            return c;
        }
        
        public static Vector3 GetCellCenter(Vector3Int cell, Vector3 origin, Vector3 tileSize)
        {
            return origin + Vector3.Scale(tileSize, cell);
        }
        
        /// <summary>
        /// Given a cube normal vector, converts it to the FaceDir enum
        /// </summary>
        internal static CellFaceDirection FromNormal(Vector3Int v)
        {
            if (v.x == 1) return CellFaceDirection.Right;
            if (v.x == -1) return CellFaceDirection.Left;
            if (v.y == 1) return CellFaceDirection.Up;
            if (v.y == -1) return CellFaceDirection.Down;
            if (v.z == 1) return CellFaceDirection.Forward;
            if (v.z == -1) return CellFaceDirection.Back;

            throw new Exception();
        }

        internal static bool InBounds(Vector3Int p, Vector3Int size)
        {
            if (p.x < 0) return false;
            if (p.x >= size.x) return false;
            if (p.y < 0) return false;
            if (p.y >= size.y) return false;
            if (p.z < 0) return false;
            if (p.z >= size.z) return false;

            return true;
        }
        
        internal static bool FindCell(
            Vector3 origin,
            Vector3 tileSize,
            Vector3 tileCenter, 
            Matrix4x4 tileLocalToGridMatrix, 
            out Vector3Int cell, 
            out CellRotation rotation)
        {
            var m = tileLocalToGridMatrix;

            Vector3Int Rotate(Vector3Int v)
            {
                var v1 = m.MultiplyVector(v);
                var v2 = new Vector3Int((int)Math.Round(v1.x), (int)Math.Round(v1.y), (int)Math.Round(v1.z));

                return v2;
            }

            // True if v is a unit vector along an axis
            bool Ok(Vector3Int v)
            {
                return Math.Abs(v.x) + Math.Abs(v.y) + Math.Abs(v.z) == 1;
            }

            var rotatedRight = Rotate(Vector3Int.right);
            var rotatedUp = Rotate(Vector3Int.up);
            var rotatedForward = Rotate(new Vector3Int(0, 0, 1));

            if (Ok(rotatedRight) && Ok(rotatedUp) && Ok(rotatedForward))
            {

                rotation = CellRotation.FromMatrixInt(new MatrixInt3x3
                {
                    col1 = rotatedRight,
                    col2 = rotatedUp,
                    col3 = rotatedForward,
                });

                var localPos = m.MultiplyPoint3x4(tileCenter);

                return FindCell(origin, tileSize, localPos, out cell);
            }
            else
            {
                cell = default;
                rotation = default;
                return false;
            }
        }

        internal static bool FindCell(Vector3 origin, Vector3 tileSize, Vector3 position, out Vector3Int cell)
        {
            position -= origin;
            var x = (int)Mathf.Round(position.x / tileSize.x);
            var y = (int)Mathf.Round(position.y / tileSize.y);
            var z = (int)Mathf.Round(position.z / tileSize.z);
            cell = new Vector3Int(x, y, z);
            return true;
        }
    }
}