using System;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public static class TopologyExtensions
    {
        public static GridTopology AsGridTopology(this ITopology topology) => topology is GridTopology gridTopology ? gridTopology : throw new Exception("Expected a grid-based topology");

        public static bool ContainsIndex(this ITopology topology, int index)
        {
            bool[] mask = topology.Mask;
            return mask == null || mask[index];
        }

        public static IEnumerable<int> GetIndices(this ITopology topology)
        {
            int indexCount = topology.IndexCount;
            bool[] mask = topology.Mask;
            for (int i = 0; i < indexCount; ++i)
            {
                if (mask == null || mask[i])
                    yield return i;
            }
        }
    }
}