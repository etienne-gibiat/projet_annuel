using System;

namespace _Elementis.Scripts.WFC
{
    public static class TopoArrayUtils
  {
    public static (int, int) RotateVector(DirectionSetType type, int x, int y, Rotation rotation)
    {
      if (type == DirectionSetType.Cartesian2d || type == DirectionSetType.Cartesian3d)
        return TopoArrayUtils.SquareRotateVector(x, y, rotation);
      if (type == DirectionSetType.Hexagonal2d)
        return TopoArrayUtils.HexRotateVector(x, y, rotation);
      throw new Exception(string.Format("Unknown directions type {0}", (object) type));
    }

    public static (int, int) SquareRotateVector(int x, int y, Rotation rotation)
    {
      if (rotation.ReflectX)
        x = -x;
      switch (rotation.RotateCw)
      {
        case 0:
          return (x, y);
        case 90:
          return (-y, x);
        case 180:
          return (-x, -y);
        case 270:
          return (y, -x);
        default:
          throw new Exception(string.Format("Unexpected angle {0}", (object) rotation.RotateCw));
      }
    }

    public static (int, int) HexRotateVector(int x, int y, Rotation rotation)
    {
      int microRotate = rotation.RotateCw / 60 % 3;
      bool rotate180 = rotation.RotateCw / 60 % 2 == 1;
      return TopoArrayUtils.HexRotateVector(x, y, microRotate, rotate180, rotation.ReflectX);
    }

    private static (int, int) HexRotateVector(
      int x,
      int y,
      int microRotate,
      bool rotate180,
      bool reflectX)
    {
      if (reflectX)
        x = -x + y;
      int num1 = x - y;
      int num2 = -x;
      int num3 = y;
      int num4 = num1;
      switch (microRotate)
      {
        case 1:
          num1 = num3;
          num3 = num2;
          num2 = num4;
          break;
        case 2:
          num1 = num2;
          num2 = num3;
          num3 = num4;
          break;
      }
      if (rotate180)
      {
        int num5 = -num1;
        num2 = -num2;
        num3 = -num3;
      }
      x = -num2;
      y = num3;
      return (x, y);
    }

    public static Direction RotateDirection(
      DirectionSet directions,
      Direction direction,
      Rotation rotation)
    {
      int x1 = directions.DX[(int) direction];
      int y1 = directions.DY[(int) direction];
      int z = directions.DZ[(int) direction];
      (int x2, int y2) = TopoArrayUtils.RotateVector(directions.Type, x1, y1, rotation);
      return directions.GetDirection(x2, y2, z);
    }

    public static ITopoArray<Tile> Rotate(
      ITopoArray<Tile> original,
      Rotation rotation,
      TileRotation tileRotation = null)
    {
      DirectionSetType type = original.Topology.AsGridTopology().Directions.Type;
      switch (type)
      {
        case DirectionSetType.Cartesian2d:
        case DirectionSetType.Cartesian3d:
          return TopoArrayUtils.SquareRotate(original, rotation, tileRotation);
        case DirectionSetType.Hexagonal2d:
          return TopoArrayUtils.HexRotate(original, rotation, tileRotation);
        default:
          throw new Exception(string.Format("Unknown directions type {0}", (object) type));
      }
    }

    public static ITopoArray<T> Rotate<T>(
      ITopoArray<T> original,
      Rotation rotation,
      TopoArrayUtils.TileRotate<T> tileRotate = null)
    {
      DirectionSetType type = original.Topology.AsGridTopology().Directions.Type;
      switch (type)
      {
        case DirectionSetType.Cartesian2d:
        case DirectionSetType.Cartesian3d:
          return TopoArrayUtils.SquareRotate<T>(original, rotation, tileRotate);
        case DirectionSetType.Hexagonal2d:
          return TopoArrayUtils.HexRotate<T>(original, rotation, tileRotate);
        default:
          throw new Exception(string.Format("Unknown directions type {0}", (object) type));
      }
    }

    public static ITopoArray<Tile> SquareRotate(
      ITopoArray<Tile> original,
      Rotation rotation,
      TileRotation tileRotation = null)
    {
      return TopoArrayUtils.SquareRotate<Tile>(original, rotation, tileRotation == null ? (TopoArrayUtils.TileRotate<Tile>) null : new TopoArrayUtils.TileRotate<Tile>(TileRotate));

      bool TileRotate(Tile tile, out Tile result) => tileRotation.Rotate(tile, rotation, out result);
    }

    public static ITopoArray<T> SquareRotate<T>(
      ITopoArray<T> original,
      Rotation rotation,
      TopoArrayUtils.TileRotate<T> tileRotate = null)
    {
      return rotation.IsIdentity ? original : TopoArrayUtils.RotateInner<T>(original, new Func<int, int, (int, int)>(MapCoord), tileRotate);

      (int, int) MapCoord(int x, int y) => TopoArrayUtils.SquareRotateVector(x, y, rotation);
    }

    public static ITopoArray<Tile> HexRotate(
      ITopoArray<Tile> original,
      Rotation rotation,
      TileRotation tileRotation = null)
    {
      return TopoArrayUtils.HexRotate<Tile>(original, rotation, tileRotation == null ? (TopoArrayUtils.TileRotate<Tile>) null : new TopoArrayUtils.TileRotate<Tile>(TileRotate));

      bool TileRotate(Tile tile, out Tile result) => tileRotation.Rotate(tile, rotation, out result);
    }

    public static ITopoArray<T> HexRotate<T>(
      ITopoArray<T> original,
      Rotation rotation,
      TopoArrayUtils.TileRotate<T> tileRotate = null)
    {
      if (rotation.IsIdentity)
        return original;
      int microRotate = rotation.RotateCw / 60 % 3;
      bool rotate180 = rotation.RotateCw / 60 % 2 == 1;
      return TopoArrayUtils.RotateInner<T>(original, new Func<int, int, (int, int)>(MapCoord), tileRotate);

      (int, int) MapCoord(int x, int y) => TopoArrayUtils.HexRotateVector(x, y, microRotate, rotate180, rotation.ReflectX);
    }

    private static ITopoArray<T> RotateInner<T>(
      ITopoArray<T> original,
      Func<int, int, (int, int)> mapCoord,
      TopoArrayUtils.TileRotate<T> tileRotate = null)
    {
      GridTopology topology = original.Topology.AsGridTopology();
      (int val1_1, int val1_2) = mapCoord(0, 0);
      (int val2_1, int val2_2) = mapCoord(topology.Width - 1, 0);
      (int val1_3, int val1_4) = mapCoord(topology.Width - 1, topology.Height - 1);
      (int val2_3, int val2_4) = mapCoord(0, topology.Height - 1);
      int num1 = Math.Min(Math.Min(val1_1, val2_1), Math.Min(val1_3, val2_3));
      int num2 = Math.Max(Math.Max(val1_1, val2_1), Math.Max(val1_3, val2_3));
      int num3 = Math.Min(Math.Min(val1_2, val2_2), Math.Min(val1_4, val2_4));
      int num4 = Math.Max(Math.Max(val1_2, val2_2), Math.Max(val1_4, val2_4));
      int num5 = -num1;
      int num6 = -num3;
      int width = num2 - num1 + 1;
      int num7 = num3;
      int height = num4 - num7 + 1;
      int depth = topology.Depth;
      bool[] mask = new bool[width * height * depth];
      GridTopology gridTopology = new GridTopology(topology.Directions, width, height, topology.Depth, false, false, false, mask);
      T[,,] values = new T[width, height, depth];
      for (int z = 0; z < topology.Depth; ++z)
      {
        for (int y1 = 0; y1 < topology.Height; ++y1)
        {
          for (int x1 = 0; x1 < topology.Width; ++x1)
          {
            (int num16, int num17) = mapCoord(x1, y1);
            int x2 = num16 + num5;
            int y2 = num17 + num6;
            int index = gridTopology.GetIndex(x2, y2, z);
            T result = original.Get(x1, y1, z);
            bool flag = true;
            if (tileRotate != null)
              flag = tileRotate(result, out result);
            values[x2, y2, z] = result;
            mask[index] = flag && topology.ContainsIndex(topology.GetIndex(x1, y1, z));
          }
        }
      }
      return (ITopoArray<T>) new TopoArray3D<T>(values, (ITopology) gridTopology);
    }

    public delegate bool TileRotate<T>(T tile, out T result);
  }
}