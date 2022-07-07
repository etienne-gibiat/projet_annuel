using System;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    internal class HeapEntropyTracker : ITracker, IRandomPicker
  {
    private readonly int patternCount;
    private readonly double[] frequencies;
    private readonly HeapEntropyTracker.EntropyValues[] entropyValues;
    private readonly double[] plogp;
    private readonly bool[] mask;
    private readonly Func<double> randomDouble;
    private readonly int indexCount;
    private readonly Wave wave;
    private Heap<HeapEntropyTracker.EntropyValues, double> heap;
    private ChangeTracker tracker;
    private const double Threshold = 1E-17;

    public HeapEntropyTracker(
      Wave wave,
      double[] frequencies,
      bool[] mask,
      Func<double> randomDouble)
    {
      this.frequencies = frequencies;
      this.patternCount = frequencies.Length;
      this.mask = mask;
      this.randomDouble = randomDouble;
      this.wave = wave;
      this.indexCount = wave.Indicies;
      this.plogp = new double[this.patternCount];
      for (int index = 0; index < this.patternCount; ++index)
      {
        double frequency = frequencies[index];
        double num = frequency > 0.0 ? frequency * Math.Log(frequency) : 0.0;
        this.plogp[index] = num;
      }
      this.entropyValues = new HeapEntropyTracker.EntropyValues[this.indexCount];
      this.heap = new Heap<HeapEntropyTracker.EntropyValues, double>(this.indexCount);
      this.tracker = new ChangeTracker(new TileModelMapping(), wave.Indicies);
    }

    public void DoBan(int index, int pattern)
    {
      this.entropyValues[index].Decrement(this.frequencies[pattern], this.plogp[pattern]);
      ((ITracker) this.tracker).DoBan(index, pattern);
    }

    public void Reset()
    {
      HeapEntropyTracker.EntropyValues other = new HeapEntropyTracker.EntropyValues();
      other.PlogpSum = 0.0;
      other.Sum = 0.0;
      other.Entropy = 0.0;
      for (int index = 0; index < this.patternCount; ++index)
      {
        double frequency = this.frequencies[index];
        double num = frequency > 0.0 ? frequency * Math.Log(frequency) : 0.0;
        other.PlogpSum += num;
        other.Sum += frequency;
      }
      other.RecomputeEntropy();
      this.heap.Clear();
      for (int index = 0; index < this.indexCount; ++index)
      {
        if (this.mask == null || this.mask[index])
        {
          HeapEntropyTracker.EntropyValues entropyValues = this.entropyValues[index] = new HeapEntropyTracker.EntropyValues(other);
          entropyValues.Index = index;
          entropyValues.Tiebreaker = this.randomDouble() * 1E-10;
          this.heap.Insert(entropyValues);
        }
      }
      ((ITracker) this.tracker).Reset();
    }

    public void UndoBan(int index, int pattern)
    {
      this.entropyValues[index].Increment(this.frequencies[pattern], this.plogp[pattern]);
      ((ITracker) this.tracker).UndoBan(index, pattern);
    }

    public int GetRandomIndex(Func<double> randomDouble, int[] externalPriority = null)
    {
      if ((double) this.tracker.ChangedCount > (double) this.wave.Indicies * 0.5 && this.tracker.ChangedCount > 1)
      {
        foreach (int changedIndex in this.tracker.GetChangedIndices())
          this.entropyValues[changedIndex].RecomputeEntropy();
        this.heap = new Heap<HeapEntropyTracker.EntropyValues, double>(Enumerable.Range(0, this.indexCount).Where<int>((Func<int, bool>) (index => this.mask == null || this.mask[index])).Where<int>((Func<int, bool>) (index =>
        {
          if (this.wave.GetPatternCount(index) > 1)
            return true;
          this.entropyValues[index].HeapIndex = -1;
          return false;
        })).Select<int, HeapEntropyTracker.EntropyValues>((Func<int, HeapEntropyTracker.EntropyValues>) (index => this.entropyValues[index])));
      }
      else
      {
        foreach (int changedIndex in this.tracker.GetChangedIndices())
        {
          HeapEntropyTracker.EntropyValues entropyValue = this.entropyValues[changedIndex];
          entropyValue.RecomputeEntropy();
          int patternCount = this.wave.GetPatternCount(changedIndex);
          if (entropyValue.HeapIndex == -1)
          {
            if (patternCount > 1)
              this.heap.Insert(entropyValue);
          }
          else if (patternCount <= 1)
          {
            this.heap.Delete(entropyValue);
            entropyValue.HeapIndex = -1;
          }
          else
            this.heap.ChangedKey(entropyValue);
        }
      }
      if (externalPriority != null)
        throw new Exception();
      return this.heap.Count == 0 ? -1 : this.heap.Peek().Index;
    }

    public void GetDistributionAt(int index, out double[] frequencies, out int[] patterns)
    {
      frequencies = this.frequencies;
      patterns = (int[]) null;
    }

    public int GetRandomPossiblePatternAt(int index, Func<double> randomDouble) => RandomPickerUtils.GetRandomPossiblePattern(this.wave, randomDouble, index, this.frequencies);

    private class EntropyValues : IHeapNode<double>
    {
      public double PlogpSum;
      public double Sum;
      public double Entropy;
      public double Tiebreaker;

      public int Index { get; set; }

      public int HeapIndex { get; set; }

      public double Key => this.Entropy + this.Tiebreaker;

      public EntropyValues()
      {
      }

      public EntropyValues(HeapEntropyTracker.EntropyValues other)
      {
        this.PlogpSum = other.PlogpSum;
        this.Sum = other.Sum;
        this.Entropy = other.Entropy;
      }

      public void RecomputeEntropy() => this.Entropy = Math.Log(this.Sum) - this.PlogpSum / this.Sum;

      public void Decrement(double p, double plogp)
      {
        this.PlogpSum -= plogp;
        this.Sum -= p;
      }

      public void Increment(double p, double plogp)
      {
        this.PlogpSum += plogp;
        this.Sum += p;
      }
    }
  }
}