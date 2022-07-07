using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    internal class Ac4PatternModelConstraint : IPatternModelConstraint
  {
    private int[][][] propagatorArray;
    private int patternCount;
    private BitArray[][] propagatorArrayDense;
    private readonly WavePropagator propagator;
    private readonly int directionsCount;
    private readonly ITopology topology;
    private int indexCount;
    private Stack<IndexPatternItem> toPropagate;
    private int[,,] compatible;

    public Ac4PatternModelConstraint(WavePropagator propagator, PatternModel model)
    {
      this.toPropagate = new Stack<IndexPatternItem>();
      this.propagator = propagator;
      this.propagatorArray = model.Propagator;
      this.patternCount = model.PatternCount;
      this.propagatorArrayDense = ((IEnumerable<int[][]>) model.Propagator).Select<int[][], BitArray[]>((Func<int[][], BitArray[]>) (a1 => ((IEnumerable<int[]>) a1).Select<int[], BitArray>((Func<int[], BitArray>) (x =>
      {
        BitArray bitArray = new BitArray(this.patternCount);
        foreach (int index in x)
          bitArray[index] = true;
        return bitArray;
      })).ToArray<BitArray>())).ToArray<BitArray[]>();
      this.topology = propagator.Topology;
      this.indexCount = this.topology.IndexCount;
      this.directionsCount = this.topology.DirectionsCount;
    }

    public void Clear()
    {
      this.toPropagate.Clear();
      this.compatible = new int[this.indexCount, this.patternCount, this.directionsCount];
      int[] numArray = new int[this.directionsCount];
      for (int index1 = 0; index1 < this.indexCount; ++index1)
      {
        if (this.topology.ContainsIndex(index1))
        {
          for (int index2 = 0; index2 < this.directionsCount; ++index2)
          {
            EdgeLabel edgeLabel;
            numArray[index2] = this.topology.TryMove(index1, (Direction) index2, out int _, out Direction _, out edgeLabel) ? (int) edgeLabel : -1;
          }
          for (int pattern = 0; pattern < this.patternCount; ++pattern)
          {
            for (int index3 = 0; index3 < this.directionsCount; ++index3)
            {
              int index4 = numArray[index3];
              if (index4 >= 0)
              {
                int length = this.propagatorArray[pattern][index4].Length;
                this.compatible[index1, pattern, index3] = length;
                if (length == 0 && this.propagator.Wave.Get(index1, pattern))
                {
                  if (this.propagator.InternalBan(index1, pattern))
                  {
                    this.propagator.SetContradiction();
                    break;
                  }
                  break;
                }
              }
            }
          }
        }
      }
    }

    public void DoBan(int index, int pattern)
    {
      for (int index1 = 0; index1 < this.directionsCount; ++index1)
        this.compatible[index, pattern, index1] -= this.patternCount;
      this.toPropagate.Push(new IndexPatternItem()
      {
        Index = index,
        Pattern = pattern
      });
    }

    public void DoSelect(int index, int pattern)
    {
      for (int index1 = 0; index1 < this.patternCount; ++index1)
      {
        if (index1 != pattern)
        {
          for (int index2 = 0; index2 < this.directionsCount; ++index2)
          {
            if (this.compatible[index, index1, index2] > 0)
              this.compatible[index, index1, index2] -= this.patternCount;
          }
        }
      }
      this.toPropagate.Push(new IndexPatternItem()
      {
        Index = index,
        Pattern = ~pattern
      });
    }

    public void UndoBan(int index, int pattern)
    {
      for (int index1 = 0; index1 < this.directionsCount; ++index1)
        this.compatible[index, pattern, index1] += this.patternCount;
      if (this.toPropagate.Count > 0)
      {
        IndexPatternItem indexPatternItem = this.toPropagate.Peek();
        if (indexPatternItem.Index == index && indexPatternItem.Pattern == pattern)
        {
          this.toPropagate.Pop();
          return;
        }
      }
      for (int index2 = 0; index2 < this.directionsCount; ++index2)
      {
        int dest;
        Direction inverseDirection;
        EdgeLabel edgeLabel;
        if (this.topology.TryMove(index, (Direction) index2, out dest, out inverseDirection, out edgeLabel))
        {
          foreach (int index3 in this.propagatorArray[pattern][(int) edgeLabel])
            ++this.compatible[dest, index3, (int) inverseDirection];
        }
      }
    }

    private void PropagateBanCore(int[] patterns, int i2, int d)
    {
      foreach (int pattern in patterns)
      {
        if (--this.compatible[i2, pattern, d] == 0 && this.propagator.InternalBan(i2, pattern))
          this.propagator.SetContradiction();
      }
    }

    private void PropagateSelectCore(BitArray patternsDense, int i2, int id)
    {
      for (int index = 0; index < this.patternCount; ++index)
      {
        bool flag = patternsDense[index];
        int num = (this.compatible[i2, index, id] > 0 ? 0 : -this.patternCount) + (flag ? 1 : 0);
        this.compatible[i2, index, id] = num;
        if (num == 0 && this.propagator.InternalBan(i2, index))
          this.propagator.SetContradiction();
      }
    }

    public void Propagate()
    {
      while (this.toPropagate.Count > 0)
      {
        IndexPatternItem indexPatternItem = this.toPropagate.Pop();
        int x;
        int y;
        int z;
        this.topology.GetCoord(indexPatternItem.Index, out x, out y, out z);
        if (indexPatternItem.Pattern >= 0)
        {
          for (int index = 0; index < this.directionsCount; ++index)
          {
            int dest;
            Direction inverseDirection;
            EdgeLabel edgeLabel;
            if (this.topology.TryMove(x, y, z, (Direction) index, out dest, out inverseDirection, out edgeLabel))
              this.PropagateBanCore(this.propagatorArray[indexPatternItem.Pattern][(int) edgeLabel], dest, (int) inverseDirection);
          }
        }
        else
        {
          int index1 = ~indexPatternItem.Pattern;
          for (int index2 = 0; index2 < this.directionsCount; ++index2)
          {
            int dest;
            Direction inverseDirection;
            EdgeLabel edgeLabel;
            if (this.topology.TryMove(x, y, z, (Direction) index2, out dest, out inverseDirection, out edgeLabel))
              this.PropagateSelectCore(this.propagatorArrayDense[index1][(int) edgeLabel], dest, (int) inverseDirection);
          }
        }
        if (this.propagator.Status == Resolution.Contradiction)
          break;
      }
    }
  }
}