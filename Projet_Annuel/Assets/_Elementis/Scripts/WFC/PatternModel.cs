namespace _Elementis.Scripts.WFC
{
    internal class PatternModel
    {
        public int[][][] Propagator { get; set; }

        public double[] Frequencies { get; set; }

        public int PatternCount => this.Frequencies.Length;
    }
}