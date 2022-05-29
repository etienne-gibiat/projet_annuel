namespace Jobberwocky.GeometryAlgorithms.Source.Core
{
    public struct EdgeInt
    {
        public EdgeInt(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public string GetKey()
        {
            return X >= Y ? X.ToString() + Y.ToString() : Y.ToString() + X.ToString();
        }
    }
}