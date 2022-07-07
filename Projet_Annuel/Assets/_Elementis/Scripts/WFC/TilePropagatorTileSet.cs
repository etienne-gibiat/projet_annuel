using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    public class TilePropagatorTileSet
    {
        internal TilePropagatorTileSet(IEnumerable<Tile> tiles)
        {
            this.Tiles = (IReadOnlyCollection<Tile>) tiles.ToArray<Tile>();
            this.OffsetToPatterns = new Dictionary<int, ISet<int>>();
        }

        public IReadOnlyCollection<Tile> Tiles { get; }

        internal Dictionary<int, ISet<int>> OffsetToPatterns { get; }

        public override string ToString() => string.Join<Tile>(",", (IEnumerable<Tile>) this.Tiles);
    }
}