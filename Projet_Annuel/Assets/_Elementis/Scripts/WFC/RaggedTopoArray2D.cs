using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    internal class RaggedTopoArray2D<T> : ITopoArray<T>
    {
        private readonly T[][] values;

        public RaggedTopoArray2D(T[][] values, bool periodic)
        {
            int length = values.Length;
            this.Topology = (ITopology) new GridTopology(((IEnumerable<T[]>) values).Max<T[]>((Func<T[], int>) (a => a.Length)), length, periodic);
            this.values = values;
        }

        public RaggedTopoArray2D(T[][] values, ITopology topology)
        {
            this.Topology = topology;
            this.values = values;
        }

        public ITopology Topology { get; private set; }

        public T Get(int x, int y, int z) => this.values[y][x];

        public T Get(int index)
        {
            int x;
            int y;
            int z;
            this.Topology.GetCoord(index, out x, out y, out z);
            return this.Get(x, y, z);
        }
    }
}