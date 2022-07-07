namespace _Elementis.Scripts.WFC
{
    internal class TopoArray3D<T> : ITopoArray<T>
    {
        private readonly T[,,] values;

        public TopoArray3D(T[,,] values, bool periodic)
        {
            this.Topology = (ITopology) new GridTopology(values.GetLength(0), values.GetLength(1), values.GetLength(2), periodic);
            this.values = values;
        }

        public TopoArray3D(T[,,] values, ITopology topology)
        {
            this.Topology = topology;
            this.values = values;
        }

        public ITopology Topology { get; private set; }

        public T Get(int x, int y, int z) => this.values[x, y, z];

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