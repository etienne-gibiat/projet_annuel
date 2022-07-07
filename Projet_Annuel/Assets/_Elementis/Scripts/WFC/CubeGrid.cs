using System.Collections.Generic;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    internal class CubeGrid : IWfcGrid
    {
        private readonly Vector3 origin;
        private readonly Vector3Int size;
        private readonly Vector3 tileSize;

        public CubeGrid(Vector3 origin, Vector3Int size, Vector3 tileSize)
        {
            this.origin = origin;
            this.size = size;
            this.tileSize = tileSize;
        }

        public Vector3Int Size => size;

        public int IndexCount => size.x * size.y * size.z;

        public ICell CellType => CubeCell.Instance;

        public Vector3Int GetCell(int index)
        {
            var x = index % size.x;
            var i = index / size.x;
            var y = i % size.y;
            var z = i / size.y;
            return new Vector3Int(x, y, z);
        }

        public IEnumerable<Vector3Int> GetCells()
        {
            for (var x = 0; x < size.x; x++)
            {
                for (var y = 0; y < size.y; y++)
                {
                    for (var z = 0; z < size.z; z++)
                    {
                        yield return new Vector3Int(x, y, z);
                    }
                }
            }
        }

        public int GetIndex(Vector3Int cell)
        {
            return cell.x + cell.y * size.x + cell.z * size.x * size.y;
        }

        public bool InBounds(Vector3Int cell)
        {
            return CubeGeometryUtils.InBounds(cell, size);
        }

        public Vector3 GetCellCenter(Vector3Int cell)
        {
            return CubeGeometryUtils.GetCellCenter(cell, origin, tileSize);
        }

        public TRS GetTRS(Vector3Int cell)
        {
            return new TRS(GetCellCenter(cell));
        }

        public bool FindCell(
            Vector3 tileCenter,
            Matrix4x4 tileLocalToGridMatrix,
            out Vector3Int cell,
            out CellRotation rotation)
        {
            return CubeGeometryUtils.FindCell(
                origin,
                tileSize,
                tileCenter,
                tileLocalToGridMatrix,
                out cell,
                out rotation);
        }

        public bool FindCell(Vector3 position, out Vector3Int cell)
        {
            return CubeGeometryUtils.FindCell(origin, tileSize, position, out cell);
        }

        public bool TryMove(Vector3Int cell, CellFaceDirection faceDir, out Vector3Int dest, out CellFaceDirection inverseFaceDir, out CellRotation rotation)
        {
            rotation = CellRotation.Identity;
            inverseFaceDir = CellType.Invert(faceDir);
            switch((CellFaceDirection)faceDir)
            {
                case CellFaceDirection.Right: cell.x += 1; break;
                case CellFaceDirection.Left: cell.x -= 1; break;
                case CellFaceDirection.Up: cell.y += 1; break;
                case CellFaceDirection.Down: cell.y -= 1; break;
                case CellFaceDirection.Forward: cell.z += 1; break;
                case CellFaceDirection.Back: cell.z -= 1; break;
            }
            dest = cell;
            return true;
        }

        public bool TryMoveByOffset(Vector3Int startCell, Vector3Int startOffset, Vector3Int destOffset, CellRotation startRotation, out Vector3Int dest, out CellRotation destRotation)
        {
            var cubeRotation = (CellRotation)startRotation;
            dest = startCell + cubeRotation * (destOffset - startOffset);
            destRotation = cubeRotation;
            return true;
        }

        public bool TryMoveByOffset(Vector3Int cell, Vector3Int offset, CellRotation offsetRotation, out Vector3Int dest)
        {
            dest = cell + ((CellRotation)offsetRotation) * offset;
            return true;
        }

        public IEnumerable<CellFaceDirection> GetValidFaceDirs(Vector3Int cell)
        {
            return CubeCell.Instance.GetFaceDirs();
        }

        public IEnumerable<CellRotation> GetMoveRotations()
        {
            yield return CellRotation.Identity;
        }

        public IEnumerable<Vector3Int> GetCellsIntersectsApprox(Bounds bounds, bool useBounds)
        {
            if (CubeGeometryUtils.FindCell(origin, tileSize, bounds.min, out var minCell) &&
                CubeGeometryUtils.FindCell(origin, tileSize, bounds.max, out var maxCell))
            {
                if (useBounds)
                {
                    // Filter to in bounds
                    minCell = Vector3Int.Max(minCell, Vector3Int.zero);
                    maxCell = Vector3Int.Min(maxCell, size - Vector3Int.one);
                }

                // Loop over cels
                for (var x = minCell.x; x <= maxCell.x; x++)
                {
                    for (var y = minCell.y; y <= maxCell.y; y++)
                    {
                        for (var z = minCell.z; z <= maxCell.z; z++)
                        {
                            yield return new Vector3Int(x, y, z); ;
                        }
                    }
                }
            }
        }
    }
}