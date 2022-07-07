using System;

namespace _Elementis.Scripts.WFC
{
    internal class WavePropagatorOptions
    {
        public int BackTrackDepth { get; set; }

        public IWaveConstraint[] Constraints { get; set; }

        public Func<double> RandomDouble { get; set; }

        public Func<WavePropagator, IPickHeuristic> PickHeuristicFactory { get; set; }

        public bool Clear { get; set; } = true;

        public ModelConstraintAlgorithm ModelConstraintAlgorithm { get; set; }
    }
}