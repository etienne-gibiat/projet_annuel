using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    internal static class WfcUtils
    {
        // NB: For convenience, the topology indices always match the grid indices
        public static ITopology GetTopology(IWfcGrid grid)
        {
            if (grid is CubeGrid cg)
            {
                return new GridTopology(cg.Size.x, cg.Size.y, cg.Size.z, false);
            }
            throw new Exception($"Unsupported Grid type {grid.GetType()}");
        }

        // This is tightly couped with GetTopology, should the be merged?
        public static TileModel GetTileModel(IWfcGrid grid, TileModelInfo tileModelInfo, WfcPalette palette)
        {
            var cellType = grid.CellType;
            var model = new AdjacentModel(DirectionSet.Cartesian3d);

            foreach (var (tile, frequency) in tileModelInfo.AllTiles)
            {
                model.SetFrequency(tile, frequency);
            }

            foreach (var ia in tileModelInfo.InternalAdjacencies)
            {
                var d = tileModelInfo.DirectionMapping[ia.GridDir];
                model.AddAdjacency(ia.Src, ia.Dest, d);
            }

            var adjacencies = cellType.GetFaceDirPairs().SelectMany(t => GetAdjacencies(palette, tileModelInfo.DirectionMapping[t.Item1], tileModelInfo.TilesByDirection[t.Item1], tileModelInfo.TilesByDirection[t.Item2])).ToList();

            foreach (var (t1, t2, d) in adjacencies)
            {
                model.AddAdjacency(t1, t2, d);
            }
            return model;
            throw new Exception($"Unsupported Grid type {grid.GetType()}");
        }

        private static IEnumerable<(Tile, Tile, T)> GetAdjacencies<T>(WfcPalette palette, T d, List<(WfcFaceDetails, Tile)> tiles1, List<(WfcFaceDetails, Tile)> tiles2)
        {
            foreach (var (fd1, t1) in tiles1)
            {
                foreach (var (fd2, t2) in tiles2)
                {
                    if (palette.Match(fd1, fd2))
                    {
                        yield return (t1, t2, d);
                    }
                }
            }
        }

        public static BiMap<CellFaceDirection, Direction> CubeMapping = new BiMap<CellFaceDirection, Direction>(new[]
        {
            (CellFaceDirection.Right, Direction.XPlus),
            (CellFaceDirection.Left, Direction.XMinus),
            (CellFaceDirection.Up, Direction.YPlus),
            (CellFaceDirection.Down, Direction.YMinus),
            (CellFaceDirection.Forward, Direction.ZPlus),
            (CellFaceDirection.Back, Direction.ZMinus),
        });
        public static BiMap<CellFaceDirection, Direction> GetDirectionMapping(ICell cellType)
        {
                return CubeMapping;
        }

        private static BiMap<(Direction, CellRotation), EdgeLabel> GetEdgeLabelMapping(IWfcGrid grid, BiMap<CellFaceDirection, Direction> directionMapping) {
            var i = 0;

            return new BiMap<(Direction, CellRotation), EdgeLabel>(
                    grid.CellType.GetFaceDirs()
                        .Select(faceDir => directionMapping[faceDir])
                        .SelectMany(dir => grid.GetMoveRotations().Select(rotation => ((dir, rotation), (EdgeLabel)(i++)))));
        }

        // Converts from an IWfcGrid to an ITopology
        public class GenericTopology : ITopology
        {
            private readonly IWfcGrid grid;
            private readonly BiMap<CellFaceDirection, Direction> directionMapping;
            private readonly BiMap<(Direction, CellRotation), EdgeLabel> edgeLabelMapping;
            private readonly Vector3Int size;
            private readonly int directionsCount;
            private readonly bool[] mask;

            public GenericTopology(IWfcGrid grid, bool[] mask = null)
            {
                this.grid = grid;
                directionMapping = GetDirectionMapping(grid.CellType);
                edgeLabelMapping = GetEdgeLabelMapping(grid, directionMapping);
                var minCell = grid.GetCells().Aggregate(Vector3Int.Max);
                var maxCell = grid.GetCells().Aggregate(Vector3Int.Max);
                if(minCell.x < 0 || minCell.y < 0|| minCell.z < 0)
                {
                    throw new NotImplementedException();
                }
                size = maxCell + Vector3Int.one;
                directionsCount = grid.CellType.GetFaceDirs().Count();
                if (mask == null)
                {

                    this.mask = new bool[grid.IndexCount];
                    foreach (var cell in grid.GetCells())
                    {
                        this.mask[grid.GetIndex(cell)] = true;
                    }
                }
                else
                {
                    this.mask = mask;
                }
            }

            public int IndexCount => grid.IndexCount;

            public int DirectionsCount => directionsCount;

            public int Width => size.x;

            public int Height => size.y;

            public int Depth => size.z;

            public bool[] Mask => mask;

            public void GetCoord(int index, out int x, out int y, out int z)
            {
                var v = grid.GetCell(index);
                x = v.x;
                y = v.y;
                z = v.z;
            }

            public int GetIndex(int x, int y, int z)
            {
                return grid.GetIndex(new Vector3Int(x, y, z));
            }

            public bool TryMove(int index, Direction direction, out int dest, out Direction inverseDirection, out EdgeLabel edgeLabel)
            {
                // TODO: Cache
                var cell = grid.GetCell(index);
                var faceDir = directionMapping[direction];
                if(!grid.TryMove(cell, faceDir, out var destCell, out var inverseFaceDir, out var cellRotation) || !grid.InBounds(destCell))
                {
                    dest = default;
                    inverseDirection = default;
                    edgeLabel = default;
                    return false;
                }
                dest = grid.GetIndex(destCell);
                inverseDirection = directionMapping[inverseFaceDir];
                edgeLabel = edgeLabelMapping[(direction, cellRotation)];
                return true;
            }

            // TODO: Perf
            public bool TryMove(int index, Direction direction, out int dest)
            {
                return TryMove(index, direction, out dest, out var _, out var _);
            }

            // TODO: Perf
            public bool TryMove(int x, int y, int z, Direction direction, out int dest, out Direction inverseDirection, out EdgeLabel edgeLabel)
            {
                return TryMove(grid.GetIndex(new Vector3Int(x, y, z)), direction, out dest, out inverseDirection, out edgeLabel);
            }

            // TODO: Perf
            public bool TryMove(int x, int y, int z, Direction direction, out int dest)
            {
                return TryMove(grid.GetIndex(new Vector3Int(x, y, z)), direction, out dest);
            }

            // TODO: Perf
            public bool TryMove(int x, int y, int z, Direction direction, out int destx, out int desty, out int destz)
            {
                var b = TryMove(grid.GetIndex(new Vector3Int(x, y, z)), direction, out var dest);
                var destCell = grid.GetCell(dest);
                destx = destCell.x;
                desty = destCell.y;
                destz = destCell.z;
                return b;
            }

            public ITopology WithMask(bool[] mask)
            {
                var m2 = new bool[IndexCount];
                for(var i=0;i<IndexCount;i++)
                {
                    m2[i] = this.mask[i] && mask[i];
                }
                return new GenericTopology(grid, m2);
            }
        }

        internal static Vector3Int? GetContradictionLocation(ITopoArray<ModelTile?> result, IWfcGrid grid)
        {
            var topology = result.Topology;
            var mask = topology.Mask ?? Enumerable.Range(0, topology.IndexCount).Select(x => true).ToArray();

            var empty = mask.ToArray();
            for (var x = 0; x < topology.Width; x++)
            {
                for (var y = 0; y < topology.Height; y++)
                {
                    for (var z = 0; z < topology.Depth; z++)
                    {
                        var p = new Vector3Int(x, y, z);
                        // Skip if already filled
                        if (!empty[grid.GetIndex(p)])
                            continue;
                        var modelTile = result.Get(x, y, z);
                        if (modelTile == null)
                            continue;
                        var tile = modelTile.Value.Tile;
                        if (tile == null)
                        {
                            return new Vector3Int(x, y, z);
                        }
                    }
                }
            }

            return null;
        }


    }
}