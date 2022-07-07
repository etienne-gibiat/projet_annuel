using System;

namespace _Elementis.Scripts.WFC
{
     internal class ArrayPriorityEntropyTracker : ITracker, IRandomPicker
  {
    private readonly int patternCount;
    private readonly FrequencySet[] frequencySets;
    private readonly ArrayPriorityEntropyTracker.EntropyValues[] entropyValues;
    private readonly bool[] mask;
    private readonly int indices;
    private readonly Wave wave;

    public ArrayPriorityEntropyTracker(Wave wave, FrequencySet[] frequencySets, bool[] mask)
    {
      this.frequencySets = frequencySets;
      this.mask = mask;
      this.wave = wave;
      this.indices = wave.Indicies;
      this.entropyValues = new ArrayPriorityEntropyTracker.EntropyValues[this.indices];
    }

    public void DoBan(int index, int pattern)
    {
      FrequencySet frequencySet = this.frequencySets[index];
      if (!this.entropyValues[index].Decrement(frequencySet.priorityIndices[pattern], frequencySet.frequencies[pattern], frequencySet.plogp[pattern]))
        return;
      this.PriorityReset(index);
    }

    public void Reset()
    {
      ArrayPriorityEntropyTracker.EntropyValues entropyValues;
      entropyValues.PriorityIndex = 0;
      entropyValues.PlogpSum = 0.0;
      entropyValues.Sum = 0.0;
      entropyValues.Count = 0;
      entropyValues.Entropy = 0.0;
      for (int index = 0; index < this.indices; ++index)
      {
        this.entropyValues[index] = entropyValues;
        if (this.frequencySets[index] != null)
          this.PriorityReset(index);
      }
    }

    private void PriorityReset(int index)
    {
      FrequencySet frequencySet = this.frequencySets[index];
      ref ArrayPriorityEntropyTracker.EntropyValues local1 = ref this.entropyValues[index];
      local1.PlogpSum = 0.0;
      local1.Sum = 0.0;
      local1.Count = 0;
      local1.Entropy = 0.0;
      for (; local1.PriorityIndex < frequencySet.groups.Length; ++local1.PriorityIndex)
      {
        ref FrequencySet.Group local2 = ref frequencySet.groups[local1.PriorityIndex];
        for (int index1 = 0; index1 < local2.patternCount; ++index1)
        {
          if (this.wave.Get(index, local2.patterns[index1]))
          {
            local1.Sum += local2.frequencies[index1];
            local1.PlogpSum += local2.plogp[index1];
            ++local1.Count;
          }
        }
        if (local1.Count != 0)
        {
          local1.RecomputeEntropy();
          break;
        }
      }
    }

    public void UndoBan(int index, int pattern)
    {
      FrequencySet frequencySet = this.frequencySets[index];
      if (!this.entropyValues[index].Increment(frequencySet.priorityIndices[pattern], frequencySet.frequencies[pattern], frequencySet.plogp[pattern]))
        return;
      this.PriorityReset(index);
    }

    public int GetRandomIndex(Func<double> randomDouble, int[] externalPriority = null)
    {
      if (externalPriority != null)
        throw new NotImplementedException();
      int num1 = -1;
      int num2 = int.MaxValue;
      double num3 = double.PositiveInfinity;
      int num4 = 0;
      for (int index = 0; index < this.indices; ++index)
      {
        if (this.mask == null || this.mask[index])
        {
          int patternCount = this.wave.GetPatternCount(index);
          int priorityIndex = this.entropyValues[index].PriorityIndex;
          double entropy = this.entropyValues[index].Entropy;
          if (patternCount > 1)
          {
            if (priorityIndex < num2 || priorityIndex == num2 && entropy < num3)
            {
              num4 = 1;
              num3 = entropy;
              num2 = priorityIndex;
            }
            else if (priorityIndex == num2 && entropy == num3)
              ++num4;
          }
        }
      }
      int num5 = (int) ((double) num4 * randomDouble());
      for (int index = 0; index < this.indices; ++index)
      {
        if (this.mask == null || this.mask[index])
        {
          int patternCount = this.wave.GetPatternCount(index);
          int priorityIndex = this.entropyValues[index].PriorityIndex;
          double entropy = this.entropyValues[index].Entropy;
          if (patternCount > 1 && priorityIndex == num2 && entropy == num3)
          {
            if (num5 == 0)
            {
              num1 = index;
              break;
            }
            --num5;
          }
        }
      }
      return num1;
    }

    public void GetDistributionAt(int index, out double[] frequencies, out int[] patterns)
    {
      ref FrequencySet.Group local = ref this.frequencySets[index].groups[this.entropyValues[index].PriorityIndex];
      frequencies = local.frequencies;
      patterns = local.patterns;
    }

    public int GetRandomPossiblePatternAt(int index, Func<double> randomDouble)
    {
      double[] frequencies;
      int[] patterns;
      this.GetDistributionAt(index, out frequencies, out patterns);
      return RandomPickerUtils.GetRandomPossiblePattern(this.wave, randomDouble, index, frequencies, patterns);
    }

    private struct EntropyValues
    {
      public int PriorityIndex;
      public double PlogpSum;
      public double Sum;
      public int Count;
      public double Entropy;

      public void RecomputeEntropy() => this.Entropy = Math.Log(this.Sum) - this.PlogpSum / this.Sum;

      public bool Decrement(int priorityIndex, double p, double plogp)
      {
        if (priorityIndex == this.PriorityIndex)
        {
          this.PlogpSum -= plogp;
          this.Sum -= p;
          --this.Count;
          if (this.Count == 0)
          {
            ++this.PriorityIndex;
            return true;
          }
          this.RecomputeEntropy();
        }
        return false;
      }

      public bool Increment(int priorityIndex, double p, double plogp)
      {
        if (priorityIndex == this.PriorityIndex)
        {
          this.PlogpSum += plogp;
          this.Sum += p;
          ++this.Count;
          this.RecomputeEntropy();
        }
        if (priorityIndex >= this.PriorityIndex)
          return false;
        this.PriorityIndex = priorityIndex;
        return true;
      }
    }
  }
}