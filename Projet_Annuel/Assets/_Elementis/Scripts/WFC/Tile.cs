using System;

namespace _Elementis.Scripts.WFC
{
    public struct Tile : IEquatable<Tile>
    {
        public Tile(object value) => this.Value = value;

        public object Value { get; private set; }

        public bool Equals(Tile other) => object.Equals(this.Value, other.Value);

        public override bool Equals(object obj) => obj is Tile tile ? object.Equals(this.Value, tile.Value) : base.Equals(obj);

        public override int GetHashCode() => this.Value != null ? this.Value.GetHashCode() : 0;

        public override string ToString() => this.Value != null ? this.Value.ToString() : "null";

        public static bool operator ==(Tile a, Tile b) => object.Equals((object) a, (object) b);

        public static bool operator !=(Tile a, Tile b) => !(a == b);
    }
}