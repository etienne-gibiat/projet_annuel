namespace _Elementis.Scripts.WFC
{
    public struct RotatedTile
    {
        public Tile Tile { get; set; }

        public Rotation Rotation { get; set; }

        public RotatedTile(Tile tile, Rotation rotation)
        {
            this.Tile = tile;
            this.Rotation = rotation;
        }

        public override int GetHashCode() => (17 * 23 + this.Rotation.GetHashCode()) * 23 + this.Tile.GetHashCode();

        public override bool Equals(object obj)
        {
            if (!(obj is RotatedTile rotatedTile))
                return base.Equals(obj);
            return this.Rotation.Equals(rotatedTile.Rotation) && this.Tile == rotatedTile.Tile;
        }

        public override string ToString() => this.Tile.ToString() + this.Rotation.ToString();
    }
}