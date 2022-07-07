using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
     public class ChangeTracker : ITracker
  {
    private readonly TileModelMapping tileModelMapping;
    private readonly int indexCount;
    private List<int> changedIndices;
    private List<int> changedIndices2;
    private int generation;
    private int[] lastChangedGeneration;

    internal ChangeTracker(TileModelMapping tileModelMapping)
      : this(tileModelMapping, tileModelMapping.PatternTopology.IndexCount)
    {
    }

    internal ChangeTracker(TileModelMapping tileModelMapping, int indexCount)
    {
      this.tileModelMapping = tileModelMapping;
      this.indexCount = indexCount;
    }

    public int ChangedCount => this.changedIndices.Count;

    public IEnumerable<int> GetChangedIndices()
    {
      List<int> changedIndices1 = this.changedIndices;
      List<int> changedIndices2 = this.changedIndices2;
      List<int> changedIndices3 = this.changedIndices;
      this.changedIndices = changedIndices2;
      this.changedIndices2 = changedIndices3;
      this.changedIndices.Clear();
      ++this.generation;
      if (this.generation == int.MaxValue)
        throw new Exception(string.Format("Change Tracker doesn't support more than {0} executions", (object) int.MaxValue));
      return this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset == null ? (IEnumerable<int>) changedIndices1 : changedIndices1.SelectMany<int, int>((Func<int, IEnumerable<int>>) (i => this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset.Get(i).Select<(Point, int, int), int>((Func<(Point, int, int), int>) (x => x.Item2))));
    }

    void ITracker.DoBan(int index, int pattern)
    {
      if (this.lastChangedGeneration[index] == this.generation)
        return;
      this.lastChangedGeneration[index] = this.generation;
      this.changedIndices.Add(index);
    }

    void ITracker.Reset()
    {
      this.changedIndices = new List<int>();
      this.changedIndices2 = new List<int>();
      this.lastChangedGeneration = new int[this.indexCount];
      this.generation = 1;
    }

    void ITracker.UndoBan(int index, int pattern)
    {
      if (this.lastChangedGeneration[index] == this.generation)
        return;
      this.lastChangedGeneration[index] = this.generation;
      this.changedIndices.Add(index);
    }
  }
}