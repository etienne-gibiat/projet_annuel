using System;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    [Serializable]
    internal class MatrixInt3x3
    {
        public Vector3Int col1;
        public Vector3Int col2;
        public Vector3Int col3;

        public MatrixInt3x3 T => new MatrixInt3x3
        {
            col1 = new Vector3Int(col1.x, col2.x, col3.x),
            col2 = new Vector3Int(col1.y, col2.y, col3.y),
            col3 = new Vector3Int(col1.z, col2.z, col3.z),
        };

        public Vector3Int Multiply(Vector3Int v)
        {
            return col1 * v.x + col2 * v.y + col3 * v.z;
        }

        public Vector3 Multiply(Vector3 v)
        {
            return (Vector3)col1 * v.x + (Vector3)col2 * v.y + (Vector3)col3 * v.z;
        }

        public static MatrixInt3x3 operator*(MatrixInt3x3 a, MatrixInt3x3 b)
        {
            return new MatrixInt3x3
            {
                col1 = a.Multiply(b.col1),
                col2 = a.Multiply(b.col2),
                col3 = a.Multiply(b.col3),
            };
        }

        public override string ToString()
        {
            return $"{col1.x} {col2.x} {col3.x}\n{col1.y} {col2.y} {col3.y}\n{col1.z} {col2.z} {col3.z}";
        }
    }
}