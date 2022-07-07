namespace _Elementis.Scripts.WFC
{
    public struct Point
    {
        public int X;
        public int Y;
        public int Z;

        public Point(int x, int y, int z = 0)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }
    }
}