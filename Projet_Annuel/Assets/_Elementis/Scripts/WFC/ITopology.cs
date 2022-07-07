namespace _Elementis.Scripts.WFC
{
    public interface ITopology
    {
        int IndexCount { get; }

        int DirectionsCount { get; }

        int Width { get; }

        int Height { get; }

        int Depth { get; }

        bool TryMove(
            int index,
            Direction direction,
            out int dest,
            out Direction inverseDirection,
            out EdgeLabel edgeLabel);

        bool TryMove(int index, Direction direction, out int dest);

        bool TryMove(
            int x,
            int y,
            int z,
            Direction direction,
            out int dest,
            out Direction inverseDirection,
            out EdgeLabel edgeLabel);

        bool TryMove(int x, int y, int z, Direction direction, out int dest);

        bool TryMove(
            int x,
            int y,
            int z,
            Direction direction,
            out int destx,
            out int desty,
            out int destz);

        bool[] Mask { get; }

        int GetIndex(int x, int y, int z);

        void GetCoord(int index, out int x, out int y, out int z);

        ITopology WithMask(bool[] mask);
    }
}