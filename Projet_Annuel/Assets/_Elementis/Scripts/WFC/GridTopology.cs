using System;

namespace _Elementis.Scripts.WFC
{
    public class GridTopology : ITopology
  {
    public GridTopology(int width, int height, bool periodic)
      : this(DirectionSet.Cartesian2d, width, height, 1, periodic, periodic, periodic)
    {
    }

    public GridTopology(int width, int height, int depth, bool periodic)
      : this(DirectionSet.Cartesian3d, width, height, depth, periodic, periodic, periodic)
    {
    }

    public GridTopology(
      DirectionSet directions,
      int width,
      int height,
      bool periodicX,
      bool periodicY,
      bool[] mask = null)
      : this(directions, width, height, 1, periodicX, periodicY, false, mask)
    {
    }

    public GridTopology(
      DirectionSet directions,
      int width,
      int height,
      int depth,
      bool periodicX,
      bool periodicY,
      bool periodicZ,
      bool[] mask = null)
    {
      this.Directions = directions;
      this.Width = width;
      this.Height = height;
      this.Depth = depth;
      this.PeriodicX = periodicX;
      this.PeriodicY = periodicY;
      this.PeriodicZ = periodicZ;
      this.Mask = mask;
    }

    public GridTopology WithMask(bool[] mask)
    {
      if (this.Width * this.Height * this.Depth != mask.Length)
        throw new Exception("Mask size doesn't fit the topology");
      return new GridTopology(this.Directions, this.Width, this.Height, this.Depth, this.PeriodicX, this.PeriodicY, this.PeriodicZ, mask);
    }

    ITopology ITopology.WithMask(bool[] mask) => (ITopology) this.WithMask(mask);

    public GridTopology WithMask(ITopoArray<bool> mask)
    {
      if (!this.IsSameSize(mask.Topology.AsGridTopology()))
        throw new Exception("Mask size doesn't fit the topology");
      bool[] mask1 = new bool[this.Width * this.Height * this.Depth];
      for (int z = 0; z < this.Depth; ++z)
      {
        for (int y = 0; y < this.Height; ++y)
        {
          for (int x = 0; x < this.Width; ++x)
            mask1[x + y * this.Width + z * this.Width * this.Height] = mask.Get(x, y, z);
        }
      }
      return this.WithMask(mask1);
    }

    public GridTopology WithSize(int width, int height, int depth = 1) => new GridTopology(this.Directions, width, height, depth, this.PeriodicX, this.PeriodicY, this.PeriodicZ);

    public GridTopology WithPeriodic(bool periodicX, bool periodicY, bool periodicZ = false) => new GridTopology(this.Directions, this.Width, this.Height, this.Depth, periodicX, periodicY, periodicZ, this.Mask);

    public DirectionSet Directions { get; set; }

    public int DirectionsCount => this.Directions.Count;

    public int Width { get; set; }

    public int Height { get; set; }

    public int Depth { get; set; }

    public bool PeriodicX { get; set; }

    public bool PeriodicY { get; set; }

    public bool PeriodicZ { get; set; }

    public bool[] Mask { get; set; }

    public int IndexCount => this.Width * this.Height * this.Depth;

    public bool IsSameSize(GridTopology other) => this.Width == other.Width && this.Height == other.Height && this.Depth == other.Depth;

    public int GetIndex(int x, int y, int z) => x + y * this.Width + z * this.Width * this.Height;

    public void GetCoord(int index, out int x, out int y, out int z)
    {
      x = index % this.Width;
      int num = index / this.Width;
      y = num % this.Height;
      z = num / this.Height;
    }

    public bool TryMove(
      int index,
      Direction direction,
      out int dest,
      out Direction inverseDirection,
      out EdgeLabel edgeLabel)
    {
      inverseDirection = this.Directions.Inverse(direction);
      edgeLabel = (EdgeLabel) direction;
      return this.TryMove(index, direction, out dest);
    }

    public bool TryMove(int index, Direction direction, out int dest)
    {
      int x;
      int y;
      int z;
      this.GetCoord(index, out x, out y, out z);
      return this.TryMove(x, y, z, direction, out dest);
    }

    public bool TryMove(
      int x,
      int y,
      int z,
      Direction direction,
      out int dest,
      out Direction inverseDirection,
      out EdgeLabel edgeLabel)
    {
      inverseDirection = this.Directions.Inverse(direction);
      edgeLabel = (EdgeLabel) direction;
      return this.TryMove(x, y, z, direction, out dest);
    }

    public bool TryMove(int x, int y, int z, Direction direction, out int dest)
    {
      if (this.TryMove(x, y, z, direction, out x, out y, out z))
      {
        dest = this.GetIndex(x, y, z);
        return true;
      }
      dest = -1;
      return false;
    }

    public bool TryMove(
      int x,
      int y,
      int z,
      Direction direction,
      out int destx,
      out int desty,
      out int destz)
    {
      int index = (int) direction;
      x += this.Directions.DX[index];
      y += this.Directions.DY[index];
      z += this.Directions.DZ[index];
      if (this.PeriodicX)
      {
        if (x < 0)
          x += this.Width;
        if (x >= this.Width)
          x -= this.Width;
      }
      else if (x < 0 || x >= this.Width)
      {
        destx = -1;
        desty = -1;
        destz = -1;
        return false;
      }
      if (this.PeriodicY)
      {
        if (y < 0)
          y += this.Height;
        if (y >= this.Height)
          y -= this.Height;
      }
      else if (y < 0 || y >= this.Height)
      {
        destx = -1;
        desty = -1;
        destz = -1;
        return false;
      }
      if (this.PeriodicZ)
      {
        if (z < 0)
          z += this.Depth;
        if (z >= this.Depth)
          z -= this.Depth;
      }
      else if (z < 0 || z >= this.Depth)
      {
        destx = -1;
        desty = -1;
        destz = -1;
        return false;
      }
      destx = x;
      desty = y;
      destz = z;
      return this.Mask == null || this.Mask[this.GetIndex(x, y, z)];
    }
  }
}