using System;

namespace _Elementis.Scripts.WFC
{
    internal class OrderedRandomPicker : IRandomPicker
    {
        private readonly int patternCount;
        private readonly double[] frequencies;
        private readonly bool[] mask;
        private readonly int indices;
        private readonly Wave wave;

        public OrderedRandomPicker(Wave wave, double[] frequencies, bool[] mask)
        {
            this.frequencies = frequencies;
            this.patternCount = frequencies.Length;
            this.mask = mask;
            this.wave = wave;
            this.indices = wave.Indicies;
        }

        public int GetRandomIndex(Func<double> randomDouble, int[] externalPriority = null)
        {
            if (externalPriority != null)
                throw new NotSupportedException();
            for (int index = 0; index < this.indices; ++index)
            {
                if ((this.mask == null || this.mask[index]) && this.wave.GetPatternCount(index) > 1)
                    return index;
            }
            return -1;
        }

        public void GetDistributionAt(int index, out double[] frequencies, out int[] patterns)
        {
            frequencies = this.frequencies;
            patterns = (int[]) null;
        }

        public int GetRandomPossiblePatternAt(int index, Func<double> randomDouble) => RandomPickerUtils.GetRandomPossiblePattern(this.wave, randomDouble, index, this.frequencies);
    }
}