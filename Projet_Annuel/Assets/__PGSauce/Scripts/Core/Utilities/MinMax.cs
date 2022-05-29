using System;

namespace PGSauce.Core.Utilities
{
    [Serializable]
    public struct MinMax<T>
    {
        public T min;
        public T max;

        public override string ToString()
        {
            return $"Min : {min}, Max : {max}";
        }
    }
}