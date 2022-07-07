using System;

namespace _Elementis.Scripts.WFC
{
    internal struct IndexPatternItem : IEquatable<IndexPatternItem>
    {
        public int Index { get; set; }

        public int Pattern { get; set; }

        public bool Equals(IndexPatternItem other) => other.Index == this.Index && other.Pattern == this.Pattern;

        public override bool Equals(object obj) => obj is IndexPatternItem other && this.Equals(other);

        public override int GetHashCode()
        {
            int num1 = this.Index;
            int num2 = num1.GetHashCode() * 17;
            num1 = this.Pattern;
            int hashCode = num1.GetHashCode();
            return num2 + hashCode;
        }

        public static bool operator ==(IndexPatternItem a, IndexPatternItem b) => a.Equals(b);

        public static bool operator !=(IndexPatternItem a, IndexPatternItem b) => !a.Equals(b);
    }
}