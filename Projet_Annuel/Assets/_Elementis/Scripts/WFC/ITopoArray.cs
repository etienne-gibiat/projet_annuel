using System;

namespace _Elementis.Scripts.WFC
{
    public interface ITopoArray<T>
    {
        ITopology Topology { get; }

        T Get(int x, int y, int z = 0);

        T Get(int index);
    }
    
    public static class TopoArrayExtensions
    {
        public static T[,] ToArray2d<T>(this ITopoArray<T> topoArray)
        {
            int width = topoArray.Topology.Width;
            int height = topoArray.Topology.Height;
            T[,] objArray = new T[width, height];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                    objArray[x, y] = topoArray.Get(x, y);
            }
            return objArray;
        }

        public static T[,,] ToArray3d<T>(this ITopoArray<T> topoArray)
        {
            int width = topoArray.Topology.Width;
            int height = topoArray.Topology.Height;
            int depth = topoArray.Topology.Depth;
            T[,,] objArray = new T[width, height, depth];
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    for (int z = 0; z < depth; ++z)
                        objArray[x, y, z] = topoArray.Get(x, y, z);
                }
            }
            return objArray;
        }

        public static ITopoArray<U> Map<T, U>(this ITopoArray<T> topoArray, Func<T, U> func)
        {
            U[] values = new U[topoArray.Topology.IndexCount];
            foreach (int index in topoArray.Topology.GetIndices())
                values[index] = func(topoArray.Get(index));
            return (ITopoArray<U>) new TopoArray1D<U>(values, topoArray.Topology);
        }

        public static ITopoArray<Tile> ToTiles<T>(this ITopoArray<T> topoArray) => topoArray.Map<T, Tile>((Func<T, Tile>) (v => new Tile((object) v)));
    }
}