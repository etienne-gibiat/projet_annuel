using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    internal class OneStepPatternModelConstraint : IPatternModelConstraint
  {
    private int[][][] propagatorArray;
    private WavePropagator propagator;
    private ITopology topology;
    private Wave wave;
    private int directionsCount;
    private int patternCount;
    private BitArray[][] propagatorArrayDense;
    private Stack<IndexPatternItem> toPropagate;

    public OneStepPatternModelConstraint(WavePropagator propagator, PatternModel model)
    {
      this.propagatorArray = model.Propagator;
      this.propagator = propagator;
      this.topology = propagator.Topology;
      this.directionsCount = propagator.Topology.DirectionsCount;
      this.patternCount = model.PatternCount;
      this.propagatorArrayDense = ((IEnumerable<int[][]>) model.Propagator).Select<int[][], BitArray[]>((Func<int[][], BitArray[]>) (a1 => ((IEnumerable<int[]>) a1).Select<int[], BitArray>((Func<int[], BitArray>) (x =>
      {
        BitArray bitArray = new BitArray(this.patternCount);
        foreach (int index in x)
          bitArray[index] = true;
        return bitArray;
      })).ToArray<BitArray>())).ToArray<BitArray[]>();
      this.toPropagate = new Stack<IndexPatternItem>();
    }

    public void Clear()
    {
      this.toPropagate.Clear();
      this.wave = this.propagator.Wave;
    }

    public void DoBan(int index, int pattern)
    {
      if (this.wave.GetPatternCount(index) != 2)
        return;
      for (int pattern1 = 0; pattern1 < this.patternCount; ++pattern1)
      {
        if (pattern1 != pattern && this.wave.Get(index, pattern1))
        {
          this.DoSelect(index, pattern1);
          break;
        }
      }
    }

    public void UndoBan(int index, int pattern)
    {
      if (this.toPropagate.Count <= 0)
        return;
      IndexPatternItem indexPatternItem = this.toPropagate.Peek();
      if (indexPatternItem.Index != index || indexPatternItem.Pattern != pattern)
        return;
      this.toPropagate.Pop();
    }

    public void DoSelect(int index, int pattern) => this.toPropagate.Push(new IndexPatternItem()
    {
      Index = index,
      Pattern = pattern
    });

    public void Propagate()
    {
      while (this.toPropagate.Count > 0)
      {
        IndexPatternItem indexPatternItem = this.toPropagate.Pop();
        int index1 = indexPatternItem.Index;
        int pattern = indexPatternItem.Pattern;
        int x;
        int y;
        int z;
        this.topology.GetCoord(index1, out x, out y, out z);
        for (int index2 = 0; index2 < this.directionsCount; ++index2)
        {
          int dest;
          EdgeLabel edgeLabel;
          if (this.topology.TryMove(x, y, z, (Direction) index2, out dest, out Direction _, out edgeLabel))
          {
            BitArray bitArray = this.propagatorArrayDense[pattern][(int) edgeLabel];
            for (int index3 = 0; index3 < this.patternCount; ++index3)
            {
              if (!bitArray[index3] && this.wave.Get(dest, index3) && this.propagator.InternalBan(dest, index3))
                this.propagator.SetContradiction();
            }
          }
        }
      }
    }
  }
}