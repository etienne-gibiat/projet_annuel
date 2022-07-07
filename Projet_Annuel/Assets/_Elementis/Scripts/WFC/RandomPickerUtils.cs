using System;

namespace _Elementis.Scripts.WFC
{
    internal static class RandomPickerUtils
      {
        public static int GetRandomPossiblePattern(
          Wave wave,
          Func<double> randomDouble,
          int index,
          double[] frequencies)
        {
          int length = frequencies.Length;
          double num1 = 0.0;
          for (int pattern = 0; pattern < length; ++pattern)
          {
            if (wave.Get(index, pattern))
              num1 += frequencies[pattern];
          }
          double num2 = randomDouble() * num1;
          for (int pattern = 0; pattern < length; ++pattern)
          {
            if (wave.Get(index, pattern))
              num2 -= frequencies[pattern];
            if (num2 <= 0.0)
              return pattern;
          }
          return length - 1;
        }
    
        public static int GetRandomPossiblePattern(
          Wave wave,
          Func<double> randomDouble,
          int index,
          double[] frequencies,
          int[] patterns)
        {
          if (patterns == null)
            return RandomPickerUtils.GetRandomPossiblePattern(wave, randomDouble, index, frequencies);
          double num1 = 0.0;
          int length = frequencies.Length;
          for (int index1 = 0; index1 < length; ++index1)
          {
            int pattern = patterns[index1];
            if (wave.Get(index, pattern))
              num1 += frequencies[index1];
          }
          double num2 = randomDouble() * num1;
          for (int index2 = 0; index2 < length; ++index2)
          {
            int pattern = patterns[index2];
            if (wave.Get(index, pattern))
              num2 -= frequencies[index2];
            if (num2 <= 0.0)
              return pattern;
          }
          return patterns[patterns.Length - 1];
        }
      }
}