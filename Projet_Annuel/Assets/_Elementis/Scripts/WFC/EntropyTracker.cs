using System;

namespace _Elementis.Scripts.WFC
{
  internal class EntropyTracker : ITracker, IRandomPicker
  {
    private readonly int patternCount;
    private readonly double[] frequencies;
    private readonly EntropyTracker.EntropyValues[] entropyValues;
    private readonly double[] plogp;
    private readonly bool[] mask;
    private readonly int indices;
    private readonly Wave wave;

    public EntropyTracker(Wave wave, double[] frequencies, bool[] mask)
    {
      this.frequencies = frequencies;
      this.patternCount = frequencies.Length;
      this.mask = mask;
      this.wave = wave;
      this.indices = wave.Indicies;
      this.plogp = new double[this.patternCount];
      for (int index = 0; index < this.patternCount; ++index)
      {
        double frequency = frequencies[index];
        double num = frequency > 0.0 ? frequency * Math.Log(frequency) : 0.0;
        this.plogp[index] = num;
      }

      this.entropyValues = new EntropyTracker.EntropyValues[this.indices];
    }

    public void DoBan(int index, int pattern) =>
      this.entropyValues[index].Decrement(this.frequencies[pattern], this.plogp[pattern]);

    public void Reset()
    {
      EntropyTracker.EntropyValues entropyValues;
      entropyValues.PlogpSum = 0.0;
      entropyValues.Sum = 0.0;
      entropyValues.Entropy = 0.0;
      for (int index = 0; index < this.patternCount; ++index)
      {
        double frequency = this.frequencies[index];
        double num = frequency > 0.0 ? frequency * Math.Log(frequency) : 0.0;
        entropyValues.PlogpSum += num;
        entropyValues.Sum += frequency;
      }

      entropyValues.RecomputeEntropy();
      for (int index = 0; index < this.indices; ++index)
        this.entropyValues[index] = entropyValues;
    }

    public void UndoBan(int index, int pattern) =>
      this.entropyValues[index].Increment(this.frequencies[pattern], this.plogp[pattern]);

    public int GetRandomIndex(Func<double> randomDouble, int[] externalPriority = null)
    {
      int num1 = -1;
      int num2 = int.MinValue;
      double num3 = double.PositiveInfinity;
      int num4 = 0;
      for (int index = 0; index < this.indices; ++index)
      {
        if (this.mask == null || this.mask[index])
        {
          int patternCount = this.wave.GetPatternCount(index);
          int num5 = externalPriority == null ? 0 : externalPriority[index];
          double entropy = this.entropyValues[index].Entropy;
          if (patternCount > 1)
          {
            if (num5 > num2 || num5 == num2 && entropy < num3)
            {
              num4 = 1;
              num2 = num5;
              num3 = entropy;
            }
            else if (num5 == num2 && entropy == num3)
              ++num4;
          }
        }
      }

      int num6 = (int) ((double) num4 * randomDouble());
      for (int index = 0; index < this.indices; ++index)
      {
        if (this.mask == null || this.mask[index])
        {
          int patternCount = this.wave.GetPatternCount(index);
          int num7 = externalPriority == null ? 0 : externalPriority[index];
          double entropy = this.entropyValues[index].Entropy;
          if (patternCount > 1 && num7 == num2 && entropy == num3)
          {
            if (num6 == 0)
            {
              num1 = index;
              break;
            }

            --num6;
          }
        }
      }

      return num1;
    }

    public void GetDistributionAt(int index, out double[] frequencies, out int[] patterns)
    {
      frequencies = this.frequencies;
      patterns = (int[]) null;
    }

    public int GetRandomPossiblePatternAt(int index, Func<double> randomDouble) =>
      RandomPickerUtils.GetRandomPossiblePattern(this.wave, randomDouble, index, this.frequencies);

    private struct EntropyValues
    {
      public double PlogpSum;
      public double Sum;
      public double Entropy;

      public void RecomputeEntropy() => this.Entropy = Math.Log(this.Sum) - this.PlogpSum / this.Sum;

      public void Decrement(double p, double plogp)
      {
        this.PlogpSum -= plogp;
        this.Sum -= p;
        this.RecomputeEntropy();
      }

      public void Increment(double p, double plogp)
      {
        this.PlogpSum += plogp;
        this.Sum += p;
        this.RecomputeEntropy();
      }
    }
  }
}