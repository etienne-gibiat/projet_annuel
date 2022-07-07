using System;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public class TilePropagatorOptions
    {
        public int BackTrackDepth { get; set; }

        public ITileConstraint[] Constraints { get; set; }

        public ITopoArray<IDictionary<Tile, PriorityAndWeight>> Weights { get; set; }

        public Func<double> RandomDouble { get; set; }

        [Obsolete("Use RandomDouble")]
        public Random Random { get; set; }

        public PickHeuristicType PickHeuristicType { get; set; }

        public ModelConstraintAlgorithm ModelConstraintAlgorithm { get; set; }
    }
}