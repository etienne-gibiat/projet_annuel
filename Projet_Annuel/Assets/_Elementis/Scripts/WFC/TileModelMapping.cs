using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace _Elementis.Scripts.WFC
{
    internal struct TileModelMapping
  {
    private static readonly ISet<int> EmptyPatternSet = (ISet<int>) new HashSet<int>();
    private static ISet<int> Empty = (ISet<int>) new HashSet<int>();

    public ITopology PatternTopology { get; set; }

    public PatternModel PatternModel { get; set; }

    public IDictionary<int, IReadOnlyDictionary<Tile, ISet<int>>> TilesToPatternsByOffset { get; set; }

    public IDictionary<int, IReadOnlyDictionary<int, Tile>> PatternsToTilesByOffset { get; set; }

    public ITopoArray<(Point, int, int)> TileCoordToPatternCoordIndexAndOffset { get; set; }

    public ITopoArray<List<(Point, int, int)>> PatternCoordToTileCoordIndexAndOffset { get; set; }

    public void GetTileCoordToPatternCoord(
      int x,
      int y,
      int z,
      out int px,
      out int py,
      out int pz,
      out int offset)
    {
      if (this.TileCoordToPatternCoordIndexAndOffset == null)
      {
        px = x;
        py = y;
        pz = z;
        offset = 0;
      }
      else
      {
        (Point point2, int _, int num2) = this.TileCoordToPatternCoordIndexAndOffset.Get(x, y, z);
        px = point2.X;
        py = point2.Y;
        pz = point2.Z;
        offset = num2;
      }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void GetTileCoordToPatternCoord(int index, out int patternIndex, out int offset)
    {
      if (this.TileCoordToPatternCoordIndexAndOffset == null)
      {
        patternIndex = index;
        offset = 0;
      }
      else
        (_, patternIndex, offset) = this.TileCoordToPatternCoordIndexAndOffset.Get(index);
    }

    public TilePropagatorTileSet CreateTileSet(IEnumerable<Tile> tiles)
    {
      TilePropagatorTileSet propagatorTileSet = new TilePropagatorTileSet(tiles);
      if (propagatorTileSet.Tiles.Count == 1)
      {
        Tile key1 = propagatorTileSet.Tiles.First<Tile>();
        foreach (int key2 in (IEnumerable<int>) this.TilesToPatternsByOffset.Keys)
        {
          ISet<int> intSet;
          propagatorTileSet.OffsetToPatterns[key2] = this.TilesToPatternsByOffset[key2].TryGetValue(key1, out intSet) ? intSet : TileModelMapping.EmptyPatternSet;
        }
      }
      return propagatorTileSet;
    }

    private static ISet<int> GetPatterns(
      IReadOnlyDictionary<Tile, ISet<int>> tilesToPatterns,
      Tile tile)
    {
      ISet<int> intSet;
      return !tilesToPatterns.TryGetValue(tile, out intSet) ? TileModelMapping.Empty : intSet;
    }

    public ISet<int> GetPatterns(Tile tile, int offset) => TileModelMapping.GetPatterns(this.TilesToPatternsByOffset[offset], tile);

    public ISet<int> GetPatterns(TilePropagatorTileSet tileSet, int offset)
    {
      ISet<int> intSet;
      if (!tileSet.OffsetToPatterns.TryGetValue(offset, out intSet))
      {
        IReadOnlyDictionary<Tile, ISet<int>> tilesToPatterns = this.TilesToPatternsByOffset[offset];
        intSet = (ISet<int>) new HashSet<int>(tileSet.Tiles.SelectMany<Tile, int>((Func<Tile, IEnumerable<int>>) (tile => (IEnumerable<int>) TileModelMapping.GetPatterns(tilesToPatterns, tile))));
        tileSet.OffsetToPatterns[offset] = intSet;
      }
      return intSet;
    }
  }
}