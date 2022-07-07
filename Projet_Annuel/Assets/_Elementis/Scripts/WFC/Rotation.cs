using System;

namespace _Elementis.Scripts.WFC
{
    public struct Rotation : IEquatable<Rotation>
    {
        public Rotation(int rotateCw = 0, bool reflectX = false)
        {
            this.RotateCw = rotateCw;
            this.ReflectX = reflectX;
        }

        public int RotateCw { get; }

        public bool ReflectX { get; }

        public bool IsIdentity => this.RotateCw == 0 && !this.ReflectX;

        public Rotation Inverse() => new Rotation(this.ReflectX ? this.RotateCw : (360 - this.RotateCw) % 360, this.ReflectX);

        public static Rotation operator *(Rotation a, Rotation b) => new Rotation(((b.ReflectX ? -a.RotateCw : a.RotateCw) + b.RotateCw + 360) % 360, a.ReflectX ^ b.ReflectX);

        public override bool Equals(object obj) => obj is Rotation other && this.Equals(other);

        public override int GetHashCode() => this.RotateCw * 2 + (this.ReflectX ? 1 : 0);

        public override string ToString() => "!" + (this.ReflectX ? "x" : "") + this.RotateCw.ToString();

        public bool Equals(Rotation other) => this.RotateCw == other.RotateCw && this.ReflectX == other.ReflectX;

        public static bool operator ==(Rotation a, Rotation b) => a.Equals(b);

        public static bool operator !=(Rotation a, Rotation b) => !a.Equals(b);
    }
}