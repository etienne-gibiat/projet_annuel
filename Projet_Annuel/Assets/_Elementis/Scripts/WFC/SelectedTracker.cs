using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace _Elementis.Scripts.WFC
{
    public class SelectedTracker : ITracker
  {
    private readonly TilePropagator tilePropagator;
    private readonly WavePropagator wavePropagator;
    private readonly TileModelMapping tileModelMapping;
    private readonly int[] patternCounts;
    private readonly TilePropagatorTileSet tileSet;

    internal SelectedTracker(
      TilePropagator tilePropagator,
      WavePropagator wavePropagator,
      TileModelMapping tileModelMapping,
      TilePropagatorTileSet tileSet)
    {
      this.tilePropagator = tilePropagator;
      this.wavePropagator = wavePropagator;
      this.tileModelMapping = tileModelMapping;
      this.tileSet = tileSet;
      this.patternCounts = new int[tilePropagator.Topology.IndexCount];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Quadstate GetQuadstate(int index)
    {
      int patternCount = this.patternCounts[index];
      if (patternCount == 0)
        return Quadstate.No;
      int patternIndex;
      this.tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out int _);
      return this.wavePropagator.Wave.GetPatternCount(patternIndex) == patternCount ? Quadstate.Yes : Quadstate.Maybe;
    }

    public bool IsSelected(int index) => this.GetQuadstate(index).IsYes();

    void ITracker.DoBan(int patternIndex, int pattern)
    {
      if (this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset == null)
      {
        this.DoBan(patternIndex, pattern, patternIndex, 0);
      }
      else
      {
        foreach ((Point _, int index2, int offset2) in this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset.Get(patternIndex))
          this.DoBan(patternIndex, pattern, index2, offset2);
      }
    }

    private void DoBan(int patternIndex, int pattern, int index, int offset)
    {
      if (!this.tileModelMapping.GetPatterns(this.tileSet, offset).Contains(pattern))
        return;
      --this.patternCounts[index];
    }

    void ITracker.Reset()
    {
      Wave wave = this.wavePropagator.Wave;
      foreach (int index in this.tilePropagator.Topology.GetIndices())
      {
        int patternIndex;
        int offset;
        this.tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out offset);
        ISet<int> patterns = this.tileModelMapping.GetPatterns(this.tileSet, offset);
        int num = 0;
        foreach (int pattern in (IEnumerable<int>) patterns)
        {
          if (wave.Get(patternIndex, pattern))
            ++num;
        }
        this.patternCounts[index] = num;
      }
    }

    void ITracker.UndoBan(int patternIndex, int pattern)
    {
      if (this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset == null)
      {
        this.UndoBan(patternIndex, pattern, patternIndex, 0);
      }
      else
      {
        foreach ((Point _, int index2, int offset2) in this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset.Get(patternIndex))
          this.UndoBan(patternIndex, pattern, index2, offset2);
      }
    }

    private void UndoBan(int patternIndex, int pattern, int index, int offset)
    {
      if (!this.tileModelMapping.GetPatterns(this.tileSet, offset).Contains(pattern))
        return;
      ++this.patternCounts[index];
    }
  }
}