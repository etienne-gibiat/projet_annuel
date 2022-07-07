using System;
using DeBroglie;

namespace _Elementis.Scripts.WFC
{
    public static class TopoArray
    {
        public static ITopoArray<T> Create<T>(T[] values, ITopology topology) => (ITopoArray<T>) new TopoArray1D<T>(values, topology);

        public static ITopoArray<T> Create<T>(T[,] values, bool periodic) => (ITopoArray<T>) new TopoArray2D<T>(values, periodic);

        public static ITopoArray<T> Create<T>(T[,] values, ITopology topology) => (ITopoArray<T>) new TopoArray2D<T>(values, topology);

        public static ITopoArray<T> Create<T>(T[][] values, bool periodic) => (ITopoArray<T>) new RaggedTopoArray2D<T>(values, periodic);

        public static ITopoArray<T> Create<T>(T[][] values, ITopology topology) => (ITopoArray<T>) new RaggedTopoArray2D<T>(values, topology);

        public static ITopoArray<T> Create<T>(T[,,] values, bool periodic) => (ITopoArray<T>) new TopoArray3D<T>(values, periodic);

        public static ITopoArray<T> Create<T>(T[,,] values, ITopology topology) => (ITopoArray<T>) new TopoArray3D<T>(values, topology);

        public static ITopoArray<T> FromConstant<T>(T value, ITopology topology) => (ITopoArray<T>) new TopoArrayConstant<T>(value, topology);

        public static ITopoArray<T> CreateByPoint<T>(Func<Point, T> f, ITopology topology)
        {
            T[,,] values = new T[topology.Width, topology.Height, topology.Depth];
            for (int z = 0; z < topology.Depth; ++z)
            {
                for (int y = 0; y < topology.Height; ++y)
                {
                    for (int x = 0; x < topology.Width; ++x)
                    {
                        int index = topology.GetIndex(x, y, z);
                        if (topology.ContainsIndex(index))
                            values[x, y, z] = f(new Point(x, y, z));
                    }
                }
            }
            return TopoArray.Create<T>(values, topology);
        }

        public static ITopoArray<T> CreateByIndex<T>(Func<int, T> f, ITopology topology)
        {
            T[] values = new T[topology.IndexCount];
            foreach (int index in topology.GetIndices())
                values[index] = f(index);
            return TopoArray.Create<T>(values, topology);
        }
    }
}