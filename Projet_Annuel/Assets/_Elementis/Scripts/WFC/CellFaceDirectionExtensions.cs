using System;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public static class CellFaceDirectionExtensions
    {
        /// <returns>Returns (0, 1, 0) vector for most faces, and returns (0, 0, 1) for the top/bottom faces.</returns>
        public static Vector3Int Up(this CellFaceDirection faceDir)
        {
            switch (faceDir)
            {
                case CellFaceDirection.Left:
                case CellFaceDirection.Right:
                case CellFaceDirection.Forward:
                case CellFaceDirection.Back:
                    return Vector3Int.up;
                case CellFaceDirection.Up:
                case CellFaceDirection.Down:
                    return new Vector3Int(0, 0, 1);
            }
            throw new Exception();
        }

        /// <returns>The normal vector for a given face.</returns>
        public static Vector3Int Forward(this CellFaceDirection faceDir)
        {
            switch (faceDir)
            {
                case CellFaceDirection.Left: return Vector3Int.left;
                case CellFaceDirection.Right: return Vector3Int.right;
                case CellFaceDirection.Up: return Vector3Int.up;
                case CellFaceDirection.Down: return Vector3Int.down;
                case CellFaceDirection.Forward: return new Vector3Int(0, 0, 1);
                case CellFaceDirection.Back: return new Vector3Int(0, 0, -1);
            }
            throw new Exception();
        }

        /// <returns>Returns the face dir with the opposite normal vector.</returns>
        public static CellFaceDirection Inverted(this CellFaceDirection faceDir)
        {

            switch (faceDir)
            {
                case CellFaceDirection.Left: return CellFaceDirection.Right;
                case CellFaceDirection.Right: return CellFaceDirection.Left;
                case CellFaceDirection.Up: return CellFaceDirection.Down;
                case CellFaceDirection.Down: return CellFaceDirection.Up;
                case CellFaceDirection.Forward: return CellFaceDirection.Back;
                case CellFaceDirection.Back: return CellFaceDirection.Forward;
            }
            throw new Exception();
        }
    }
}