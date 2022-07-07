using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public class SelectedChangeTracker : ITracker
  {
    private readonly TilePropagator tilePropagator;
    private readonly WavePropagator wavePropagator;
    private readonly TileModelMapping tileModelMapping;
    private readonly int[] patternCounts;
    private readonly Quadstate[] values;
    private readonly TilePropagatorTileSet tileSet;
    private readonly IQuadstateChanged onChange;

    internal SelectedChangeTracker(
      TilePropagator tilePropagator,
      WavePropagator wavePropagator,
      TileModelMapping tileModelMapping,
      TilePropagatorTileSet tileSet,
      IQuadstateChanged onChange)
    {
      this.tilePropagator = tilePropagator;
      this.wavePropagator = wavePropagator;
      this.tileModelMapping = tileModelMapping;
      this.tileSet = tileSet;
      this.onChange = onChange;
      this.patternCounts = new int[tilePropagator.Topology.IndexCount];
      this.values = new Quadstate[tilePropagator.Topology.IndexCount];
    }

    private Quadstate GetQuadstateInner(int index)
    {
      int patternCount1 = this.patternCounts[index];
      int patternIndex;
      this.tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out int _);
      int patternCount2 = this.wavePropagator.Wave.GetPatternCount(patternIndex);
      if (patternCount2 == 0)
        return Quadstate.Contradiction;
      if (patternCount1 == 0)
        return Quadstate.No;
      return patternCount2 == patternCount1 ? Quadstate.Yes : Quadstate.Maybe;
    }

    public Quadstate GetQuadstate(int index) => this.values[index];

    public bool IsSelected(int index) => this.GetQuadstate(index).IsYes();

    public void DoBan(int patternIndex, int pattern)
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
      if (this.tileModelMapping.GetPatterns(this.tileSet, offset).Contains(pattern))
        --this.patternCounts[index];
      this.DoNotify(index);
    }

    public void Reset()
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
          if (patterns.Contains(pattern) && wave.Get(patternIndex, pattern))
            ++num;
        }
        this.patternCounts[index] = num;
        this.values[index] = this.GetQuadstateInner(index);
      }
      this.onChange.Reset(this);
    }

    public void UndoBan(int patternIndex, int pattern)
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
      if (this.tileModelMapping.GetPatterns(this.tileSet, offset).Contains(pattern))
        ++this.patternCounts[index];
      this.DoNotify(index);
    }

    private void DoNotify(int index)
    {
      Quadstate quadstateInner = this.GetQuadstateInner(index);
      Quadstate before = this.values[index];
      if (quadstateInner == before)
        return;
      this.values[index] = quadstateInner;
      this.onChange.Notify(index, before, quadstateInner);
    }
  }
}