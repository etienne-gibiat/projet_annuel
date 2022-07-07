using System;
using System.Collections;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public struct DirectionSet : IEnumerable<Direction>, IEnumerable
  {
    public static readonly DirectionSet Cartesian2d = new DirectionSet()
    {
      DX = new int[4]{ 1, -1, 0, 0 },
      DY = new int[4]{ 0, 0, 1, -1 },
      DZ = new int[4],
      Count = 4,
      Type = DirectionSetType.Cartesian2d
    };
    public static readonly DirectionSet Hexagonal2d = new DirectionSet()
    {
      DX = new int[6]{ 1, -1, 0, 0, 1, -1 },
      DY = new int[6]{ 0, 0, 1, -1, 1, -1 },
      DZ = new int[6],
      Count = 6,
      Type = DirectionSetType.Hexagonal2d
    };
    public static readonly DirectionSet Hexagonal3d = new DirectionSet()
    {
      DX = new int[8]{ 1, -1, 0, 0, 0, 0, 1, -1 },
      DY = new int[8]{ 0, 0, 1, -1, 0, 0, 0, 0 },
      DZ = new int[8]{ 0, 0, 0, 0, 1, -1, 1, -1 },
      Count = 8,
      Type = DirectionSetType.Hexagonal3d
    };
    public static readonly DirectionSet Cartesian3d = new DirectionSet()
    {
      DX = new int[6]{ 1, -1, 0, 0, 0, 0 },
      DY = new int[6]{ 0, 0, 1, -1, 0, 0 },
      DZ = new int[6]{ 0, 0, 0, 0, 1, -1 },
      Count = 6,
      Type = DirectionSetType.Cartesian3d
    };

    public int[] DX { get; private set; }

    public int[] DY { get; private set; }

    public int[] DZ { get; private set; }

    public int Count { get; private set; }

    public DirectionSetType Type { get; private set; }

    public Direction Inverse(Direction d) => d ^ Direction.XMinus;

    public Direction GetDirection(int x, int y, int z = 0)
    {
      for (int index = 0; index < this.Count; ++index)
      {
        if (x == this.DX[index] && y == this.DY[index] && z == this.DZ[index])
          return (Direction) index;
      }
      throw new Exception(string.Format("No direction corresponds to ({0}, {1}, {2})", (object) x, (object) y, (object) z));
    }

    public IEnumerator<Direction> GetEnumerator()
    {
      for (int d = 0; d < this.Count; ++d)
        yield return (Direction) d;
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();
  }
}