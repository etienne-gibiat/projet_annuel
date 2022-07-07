using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    public class CubeCell : ICell
    {
        private static CubeCell instance;

        public static CubeCell Instance => instance ??= new CubeCell();


        public IEnumerable<CellFaceDirection> GetFaceDirs() => new[]
        {
            CellFaceDirection.Right,
            CellFaceDirection.Left,
            CellFaceDirection.Up,
            CellFaceDirection.Down,
            CellFaceDirection.Forward,
            CellFaceDirection.Back,
        };

        public IEnumerable<(CellFaceDirection, CellFaceDirection)> GetFaceDirPairs() => new[]
        {
            (CellFaceDirection.Right, CellFaceDirection.Left),
            (CellFaceDirection.Up, CellFaceDirection.Down),
            (CellFaceDirection.Forward, CellFaceDirection.Back),
        };

        public CellFaceDirection Invert(CellFaceDirection faceDir)
        {
            return (CellFaceDirection)(1 ^ (int)faceDir);
        }
        
        private static readonly IDictionary<WfcRotationGroup, CellRotation[]> RotationsByGroupType = new Dictionary<WfcRotationGroup, CellRotation[]>()
        {
            {
                WfcRotationGroup.None,
                new[]
                {
                    CellRotation.Identity,
                    CellRotation.ReflectX,
                }
            },
            {
                WfcRotationGroup.XZ,
                new[]
                {
                    CellRotation.Identity,
                    CellRotation.RotateXZ,
                    CellRotation.RotateXZ * CellRotation.RotateXZ,
                    CellRotation.RotateXZ * CellRotation.RotateXZ * CellRotation.RotateXZ,
                    CellRotation.ReflectX * CellRotation.Identity,
                    CellRotation.ReflectX * CellRotation.RotateXZ,
                    CellRotation.ReflectX * CellRotation.RotateXZ * CellRotation.RotateXZ,
                    CellRotation.ReflectX * CellRotation.RotateXZ * CellRotation.RotateXZ * CellRotation.RotateXZ,
                }
            },
            {
                WfcRotationGroup.XY,
                new[]
                {
                    CellRotation.Identity,
                    CellRotation.RotateXY,
                    CellRotation.RotateXY * CellRotation.RotateXY,
                    CellRotation.RotateXY * CellRotation.RotateXY * CellRotation.RotateXY,
                    CellRotation.ReflectX * CellRotation.Identity,
                    CellRotation.ReflectX * CellRotation.RotateXY,
                    CellRotation.ReflectX * CellRotation.RotateXY * CellRotation.RotateXY,
                    CellRotation.ReflectX * CellRotation.RotateXY * CellRotation.RotateXY * CellRotation.RotateXY,
                }
            },
            {
                WfcRotationGroup.YZ,
                new[]
                {
                    CellRotation.Identity,
                    CellRotation.RotateYZ,
                    CellRotation.RotateYZ * CellRotation.RotateYZ,
                    CellRotation.RotateYZ * CellRotation.RotateYZ * CellRotation.RotateYZ,
                    CellRotation.ReflectX * CellRotation.Identity,
                    CellRotation.ReflectX * CellRotation.RotateYZ,
                    CellRotation.ReflectX * CellRotation.RotateYZ * CellRotation.RotateYZ,
                    CellRotation.ReflectX * CellRotation.RotateYZ * CellRotation.RotateYZ * CellRotation.RotateYZ,
                }
            },
            {
                WfcRotationGroup.All,
                CellRotation.All.Select(x => x).ToArray()
            },
        };

        public IList<CellRotation> GetRotations(bool rotatable = true, bool reflectable = true,
            WfcRotationGroup rotationGroupType = WfcRotationGroup.All)
        {
            if (rotatable)
            {
                var rotations = RotationsByGroupType[rotationGroupType];
                if (reflectable)
                {
                    return rotations;
                }

                return new ArraySegment<CellRotation>(rotations, 0, rotations.Length / 2);
            }

            if (reflectable)
            {
                return new[]
                {
                    CellRotation.Identity,
                    CellRotation.ReflectX,
                };
            }
            
            return new[]
            {
                CellRotation.Identity,
            };
        }

        public CellRotation Multiply(CellRotation a, CellRotation b)
        {
            return a * b;
        }

        public CellRotation Invert(CellRotation a)
        {
            return a.Invert();
        }

        public CellRotation GetIdentity()
        {
            return CellRotation.Identity;
        }

        public CellFaceDirection Rotate(CellFaceDirection faceDir, CellRotation rotation)
        {
            return rotation * faceDir;
        }

        public (CellFaceDirection, WfcFaceDetails) RotateBy(CellFaceDirection faceDir, WfcFaceDetails faceDetails, CellRotation rot)
        {
            var (a, b) = CubeGeometryUtils.RotateBy(faceDir, faceDetails, rot.ToMatrixInt());
            return (a, b);
        }

        public Matrix4x4 GetMatrix(CellRotation cellRotation)
        {
            var m = cellRotation.ToMatrixInt();
            return new Matrix4x4(
                new Vector4(m.col1.x, m.col1.y, m.col1.z, 0),
                new Vector4(m.col2.x, m.col2.y, m.col2.z, 0),
                new Vector4(m.col3.x, m.col3.y, m.col3.z, 0),
                new Vector4(0, 0, 0, 1));
        }

        public bool TryMove(Vector3Int offset, CellFaceDirection dir, out Vector3Int dest)
        {
            dest = offset + (dir).Forward();
            return true;
        }

        public IEnumerable<CellFaceDirection> FindPath(Vector3Int startOffset, Vector3Int endOffset)
        {
            var offset = startOffset;
            while (offset.x < endOffset.x)
            {
                yield return CellFaceDirection.Right;
                offset.x += 1;
            }
            while (offset.x > endOffset.x)
            {
                yield return CellFaceDirection.Left;
                offset.x -= 1;
            }
            while (offset.y < endOffset.y)
            {
                yield return CellFaceDirection.Up;
                offset.y += 1;
            }
            while (offset.y > endOffset.y)
            {
                yield return CellFaceDirection.Down;
                offset.y -= 1;
            }
            while (offset.z < endOffset.z)
            {
                yield return CellFaceDirection.Forward;
                offset.z += 1;
            }
            while (offset.z > endOffset.z)
            {
                yield return CellFaceDirection.Back;
                offset.z -= 1;
            }
        }

        public Vector3 GetCellCenter(Vector3Int offset, Vector3 center, Vector3 tileSize)
        {
            return center + Vector3.Scale(offset, tileSize);
        }

        public bool TryMoveByOffset(Vector3Int startCell, Vector3Int startOffset, Vector3Int destOffset, CellRotation rotation,
            out Vector3Int destCell)
        {
            destCell = startCell + (rotation) * (destOffset - startOffset);
            return true;
        }

        public IDictionary<Vector3Int, Vector3Int> Realign(ISet<Vector3Int> shape, CellRotation rotation)
        {
            var m = (rotation).ToMatrixInt();
            var min = shape.Aggregate(Vector3Int.Min);
            var max = shape.Aggregate(Vector3Int.Max);
            var r1 = m.Multiply(min);
            var r2 = m.Multiply(max);
            var newMin = Vector3Int.Min(r1, r2);
            var translation = min - newMin;
            var result = shape.ToDictionary(offset => offset, offset => translation + m.Multiply(offset));
            if (!result.Values.All(shape.Contains))
                return null;
            return result;
        }
    }
}