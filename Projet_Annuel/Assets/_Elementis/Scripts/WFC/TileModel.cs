using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public abstract class TileModel
    {
        internal abstract TileModelMapping GetTileModelMapping(ITopology topology);

        public abstract IEnumerable<Tile> Tiles { get; }

        public abstract void MultiplyFrequency(Tile tile, double multiplier);

        public virtual void MultiplyFrequency(Tile tile, double multiplier, TileRotation tileRotation)
        {
            HashSet<Tile> tileSet = new HashSet<Tile>();
            foreach (Rotation rotation in tileRotation.RotationGroup)
            {
                Tile result;
                if (tileRotation.Rotate(tile, rotation, out result) && tileSet.Add(result))
                    this.MultiplyFrequency(result, multiplier);
            }
        }
    }
}