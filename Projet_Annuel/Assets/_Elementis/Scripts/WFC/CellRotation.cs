using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public struct CellRotation
    {
          short value;

        private CellRotation(short value)
        {
            this.value = value;
        }

        public static CellRotation Identity => new CellRotation(0x012);
        public static CellRotation ReflectX => new CellRotation(0x812);
        public static CellRotation ReflectY => new CellRotation(0x092);
        public static CellRotation ReflectZ => new CellRotation(0x01A);
        public static CellRotation RotateXZ => new CellRotation(0xA10);
        public static CellRotation RotateXY => new CellRotation(0x902);
        public static CellRotation RotateYZ => new CellRotation(0x0A1);

        // Ordered by all rotations, then all refelctions
        public static IEnumerable<CellRotation> All
        {
            get
            {
                var evenPermutations = new short[]
                {
                    0x012,
                    0x120,
                    0x201,
                };
                var oddPermutations = new short[]
                {
                    0x021,
                    0x102,
                    0x210,
                };
                var evenReflections = new short[]
                {
                    0x000,
                    0x088,
                    0x808,
                    0x880,
                };
                var oddReflections = new short[]
                {
                    0x008,
                    0x080,
                    0x800,
                    0x888,
                };

                foreach (var r in evenReflections)
                {
                    foreach (var p in evenPermutations)
                    {
                        yield return new CellRotation((short)(p + r));
                    }
                }
                foreach (var r in oddReflections)
                {
                    foreach (var p in oddPermutations)
                    {
                        yield return new CellRotation((short)(p + r));
                    }
                }

                foreach (var r in evenReflections)
                {
                    foreach (var p in oddPermutations)
                    {
                        yield return new CellRotation((short)(p + r));
                    }
                }
                foreach (var r in oddReflections)
                {
                    foreach (var p in evenPermutations)
                    {
                        yield return new CellRotation((short)(p + r));
                    }
                }
            }
        }

        internal Matrix4x4 ToMatrix()
        {
            Vector4 GetCol(int i)
            {
                var sign = (i & 8) == 0 ? 1 : -1;
                var row = i & 3;
                return new Vector4(
                    sign * (row == 0 ? 1 : 0),
                    sign * (row == 1 ? 1 : 0),
                    sign * (row == 2 ? 1 : 0)
                    );
            }
            return new Matrix4x4(
                GetCol(value >> 8),
                GetCol(value >> 4),
                GetCol(value >> 0),
                new Vector4(0, 0, 0, 1)
            );
        }


        internal MatrixInt3x3 ToMatrixInt()
        {
            Vector3Int GetCol(int i)
            {
                var sign = (i & 8) == 0 ? 1 : -1;
                var row = i & 3;
                return new Vector3Int(
                    sign * (row == 0 ? 1 : 0),
                    sign * (row == 1 ? 1 : 0),
                    sign * (row == 2 ? 1 : 0)
                    );
            }
            return new MatrixInt3x3
            {
                col1 = GetCol(value >> 8),
                col2 = GetCol(value >> 4),
                col3 = GetCol(value >> 0),
            };
        }

        internal static CellRotation FromMatrixInt(MatrixInt3x3 matrix)
        {
            int GetCol(Vector3Int c)
            {
                if(c.x != 0)
                {
                    return c.x > 0 ? 0 : 8;
                }
                else if(c.y != 0)
                {
                    return c.y > 0 ? 1 : 9;
                }
                else
                {
                    return c.z > 0 ? 2 : 10;
                }
            }
            return new CellRotation((short)(
                (GetCol(matrix.col1) << 8) |
                (GetCol(matrix.col2) << 4) |
                (GetCol(matrix.col3) << 0)
                ));
        }

        public bool IsReflection
        {
            get
            {
                var isOddPermutation = ((value & 3) + ((value & (3 << 4)) >> 3)) % 3 != 1;
                return isOddPermutation ^ ((value & (1 << 3)) != 0) ^ ((value & (1 << 7)) != 0) ^ ((value & (1 << 11)) != 0);
            }
        }

        public CellRotation Invert()
        {
            return FromMatrixInt(ToMatrixInt().T);
        }

        public override bool Equals(object obj)
        {
            return obj is CellRotation rotation &&
                   value == rotation.value;
        }

        public override int GetHashCode()
        {
            return -1584136870 + value.GetHashCode();
        }

        public static bool operator ==(CellRotation a, CellRotation b)
        {
            return a.value == b.value;
        }

        public static bool operator !=(CellRotation a, CellRotation b)
        {
            return a.value != b.value;
        }

        public static CellRotation operator*(CellRotation a, CellRotation b)
        {
            // bit fiddling version of FromMatrix(a.ToMatrix() * b.ToMatrix())

            // Which column in a to read, for a given axis
            var xOffset = (2 - ((b.value >> 8) & 3)) << 2;
            var yOffset = (2 - ((b.value >> 4) & 3)) << 2;
            var zOffset = (2 - ((b.value >> 0) & 3)) << 2;

            var col1 = ((a.value >> xOffset) & 15) ^ ((b.value >> 8) & (1 << 3));
            var col2 = ((a.value >> yOffset) & 15) ^ ((b.value >> 4) & (1 << 3));
            var col3 = ((a.value >> zOffset) & 15) ^ ((b.value >> 0) & (1 << 3));

            return new CellRotation((short)((col1 << 8) + (col2 << 4) + (col3 << 0)));
        }

        public static Vector3 operator *(CellRotation r, Vector3 v)
        {
            // bit fiddling version of r.ToMatrix().Multiply(v);

            var o = new Vector3();
            o[(r.value >> 8) & 3] = v.x * ((r.value & (1 << 11)) != 0 ? -1 : 1);
            o[(r.value >> 4) & 3] = v.y * ((r.value & (1 << 7)) != 0 ? -1 : 1);
            o[(r.value >> 0) & 3] = v.z * ((r.value & (1 << 3)) != 0 ? -1 : 1);
            return o;
        }

        public static Vector3Int operator *(CellRotation r, Vector3Int v)
        {
            // bit fiddling version of r.ToMatrix().Multiply(v);

            var o = new Vector3Int();
            o[(r.value >> 8) & 3] = v.x * ((r.value & (1 << 11)) != 0 ? -1 : 1);
            o[(r.value >> 4) & 3] = v.y * ((r.value & (1 << 7)) != 0 ? -1 : 1);
            o[(r.value >> 0) & 3] = v.z * ((r.value & (1 << 3)) != 0 ? -1 : 1);
            return o;
        }

        private static CellFaceDirection ToFaceDir(Vector3Int v)
        {
            if (v.x == 1)
                return CellFaceDirection.Right;
            if (v.x == -1)
                return CellFaceDirection.Left;

            if (v.y == 1)
                return CellFaceDirection.Up;
            if (v.y == -1)
                return CellFaceDirection.Down;

            if (v.z == 1)
                return CellFaceDirection.Forward;
            if (v.z == -1)
                return CellFaceDirection.Back;

            throw new Exception();
        }

        public static CellFaceDirection operator *(CellRotation r, CellFaceDirection dir)
        {
            return ToFaceDir(r * dir.Forward());
        }
    }
}