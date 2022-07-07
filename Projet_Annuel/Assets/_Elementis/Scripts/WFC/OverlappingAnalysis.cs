using System;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
  internal struct PatternArray
  {
    public Tile[,,] Values;

    public int Width => this.Values.GetLength(0);

    public int Height => this.Values.GetLength(1);

    public int Depth => this.Values.GetLength(2);
  }
  
    internal static class OverlappingAnalysis
  {
    public static IEnumerable<ITopoArray<Tile>> GetRotatedSamples(
      ITopoArray<Tile> sample,
      TileRotation tileRotation = null)
    {
      tileRotation = tileRotation ?? new TileRotation();
      foreach (Rotation rotation in tileRotation.RotationGroup)
        yield return TopoArrayUtils.Rotate(sample, rotation, tileRotation);
    }

    public static void GetPatterns(
      ITopoArray<Tile> sample,
      int nx,
      int ny,
      int nz,
      bool periodicX,
      bool periodicY,
      bool periodicZ,
      Dictionary<PatternArray, int> patternIndices,
      List<PatternArray> patternArrays,
      List<double> frequencies)
    {
      int width = sample.Topology.Width;
      int height = sample.Topology.Height;
      int depth = sample.Topology.Depth;
      int num1 = periodicX ? width - 1 : width - nx;
      int num2 = periodicY ? height - 1 : height - ny;
      int num3 = periodicZ ? depth - 1 : depth - nz;
      for (int x = 0; x <= num1; ++x)
      {
        for (int y = 0; y <= num2; ++y)
        {
          for (int z = 0; z <= num3; ++z)
          {
            PatternArray pattern;
            if (OverlappingAnalysis.TryExtract(sample, nx, ny, nz, x, y, z, out pattern))
            {
              int index;
              if (!patternIndices.TryGetValue(pattern, out index))
              {
                index = patternIndices[pattern] = patternIndices.Count;
                patternArrays.Add(pattern);
                frequencies.Add(1.0);
              }
              else
                ++frequencies[index];
            }
          }
        }
      }
    }

    public static PatternArray PatternEdge(
      PatternArray patternArray,
      int dx,
      int dy,
      int dz)
    {
      PatternArray patternArray1 = patternArray;
      int length1 = patternArray1.Width - Math.Abs(dx);
      int num1 = Math.Max(0, dx);
      int length2 = patternArray1.Height - Math.Abs(dy);
      int num2 = Math.Max(0, dy);
      int length3 = patternArray1.Depth - Math.Abs(dz);
      int num3 = Math.Max(0, dz);
      PatternArray patternArray2 = new PatternArray()
      {
        Values = new Tile[length1, length2, length3]
      };
      for (int index1 = 0; index1 < length1; ++index1)
      {
        for (int index2 = 0; index2 < length2; ++index2)
        {
          for (int index3 = 0; index3 < length3; ++index3)
            patternArray2.Values[index1, index2, index3] = patternArray.Values[index1 + num1, index2 + num2, index3 + num3];
        }
      }
      return patternArray2;
    }

    private static bool TryExtract(
      ITopoArray<Tile> sample,
      int nx,
      int ny,
      int nz,
      int x,
      int y,
      int z,
      out PatternArray pattern)
    {
      int width = sample.Topology.Width;
      int height = sample.Topology.Height;
      int depth = sample.Topology.Depth;
      Tile[,,] tileArray = new Tile[nx, ny, nz];
      for (int index1 = 0; index1 < nx; ++index1)
      {
        int x1 = (x + index1) % width;
        for (int index2 = 0; index2 < ny; ++index2)
        {
          int y1 = (y + index2) % height;
          for (int index3 = 0; index3 < nz; ++index3)
          {
            int z1 = (z + index3) % depth;
            int index4 = sample.Topology.GetIndex(x1, y1, z1);
            if (!sample.Topology.ContainsIndex(index4))
            {
              pattern = new PatternArray();
              return false;
            }
            tileArray[index1, index2, index3] = sample.Get(x1, y1, z1);
          }
        }
      }
      pattern = new PatternArray() { Values = tileArray };
      return true;
    }
  }
}