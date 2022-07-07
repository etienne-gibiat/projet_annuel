using System.Collections;

namespace _Elementis.Scripts.WFC
{
    public class Wave
    {
        private readonly int patternCount;
        private readonly BitArray possibilities;
        private readonly int[] patternCounts;
        private readonly int indices;

        public Wave(int patternCount, int indices)
        {
            this.patternCount = patternCount;
            this.indices = indices;
            this.possibilities = new BitArray(indices * patternCount, true);
            this.patternCounts = new int[indices];
            for (int index = 0; index < indices; ++index)
                this.patternCounts[index] = patternCount;
        }

        public int Indicies => this.indices;

        public bool Get(int index, int pattern) => this.possibilities[index * this.patternCount + pattern];

        public int GetPatternCount(int index) => this.patternCounts[index];

        public bool RemovePossibility(int index, int pattern)
        {
            this.possibilities[index * this.patternCount + pattern] = false;
            return --this.patternCounts[index] == 0;
        }

        public void AddPossibility(int index, int pattern)
        {
            this.possibilities[index * this.patternCount + pattern] = true;
            ++this.patternCounts[index];
        }

        public double GetProgress()
        {
            int num = 0;
            foreach (bool possibility in this.possibilities)
            {
                if (!possibility)
                    ++num;
            }
            return (double) num / (double) (this.patternCount - 1) / (double) this.indices;
        }
    }
}