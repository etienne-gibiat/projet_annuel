using UnityEngine;

namespace _Elementis.Scripts.WFC
{
    internal struct ModelTile
    {
        public ModelTile(WfcTileBase tile, CellRotation rotation, Vector3Int offset)
        {
            Tile = tile;
            Rotation = rotation;
            Offset = offset;
        }

        public WfcTileBase Tile { get; set; }
        public CellRotation Rotation { get; set; }
        public Vector3Int Offset { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Rotation.GetHashCode();
                hash = hash * 23 + (Tile == null ? 0 : Tile.GetHashCode());
                hash = hash * 23 + Offset.GetHashCode();
                return hash;
            }
        }

        public bool Equals(ModelTile other)
        {
            return Rotation == other.Rotation && Tile == other.Tile && Offset == other.Offset;
        }

        public override bool Equals(object obj)
        {
            if (obj is ModelTile other)
            {
                return Equals(other);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        public override string ToString()
        {
            return Tile.name.ToString() + Offset.ToString() + Rotation.ToString();
        }
    }
}