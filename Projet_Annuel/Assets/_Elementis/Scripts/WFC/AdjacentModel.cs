using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    public class AdjacentModel : TileModel
  {
    private DirectionSet directions;
    private Dictionary<Tile, int> tilesToPatterns;
    private List<double> frequencies;
    private List<HashSet<int>[]> propagator;

    public static AdjacentModel Create<T>(T[,] sample, bool periodic) => AdjacentModel.Create<T>((ITopoArray<T>) new TopoArray2D<T>(sample, periodic));

    public static AdjacentModel Create<T>(ITopoArray<T> sample) => new AdjacentModel(sample.ToTiles<T>());

    public AdjacentModel()
    {
      this.tilesToPatterns = new Dictionary<Tile, int>();
      this.frequencies = new List<double>();
      this.propagator = new List<HashSet<int>[]>();
    }

    public AdjacentModel(DirectionSet directions)
      : this()
    {
      this.SetDirections(directions);
    }

    public AdjacentModel(ITopoArray<Tile> sample)
      : this()
    {
      this.AddSample(sample);
    }

    public override IEnumerable<Tile> Tiles => (IEnumerable<Tile>) this.tilesToPatterns.Keys;

    public void SetDirections(DirectionSet directions)
    {
      if (this.directions.Type != DirectionSetType.Unknown && this.directions.Type != directions.Type)
        throw new Exception(string.Format("Cannot set directions to {0}, it has already been set to {1}", (object) directions.Type, (object) this.directions.Type));
      this.directions = directions;
    }

    private void RequireDirections()
    {
      if (this.directions.Type == DirectionSetType.Unknown)
        throw new Exception("Directions must be set before calling this method");
    }

    public void SetFrequency(Tile tile, double frequency, TileRotation tileRotation)
    {
      List<Tile> list = tileRotation.RotateAll(tile).ToList<Tile>();
      foreach (Tile tile1 in list)
        this.frequencies[this.GetPattern(tile1)] = 0.0;
      double num = frequency / (double) list.Count;
      foreach (Tile tile2 in list)
        this.frequencies[this.GetPattern(tile2)] += num;
    }

    public void SetFrequency(Tile tile, double frequency) => this.frequencies[this.GetPattern(tile)] = frequency;

    public void SetUniformFrequency()
    {
      foreach (Tile tile in this.Tiles)
        this.SetFrequency(tile, 1.0);
    }

    public void AddAdjacency(
      IList<Tile> src,
      IList<Tile> dest,
      Direction dir,
      TileRotation tileRotation = null)
    {
      this.RequireDirections();
      int index = (int) dir;
      int x = this.directions.DX[index];
      int y = this.directions.DY[index];
      int z = this.directions.DZ[index];
      this.AddAdjacency(src, dest, x, y, z, tileRotation);
    }

    public void AddAdjacency(
      IList<Tile> src,
      IList<Tile> dest,
      int x,
      int y,
      int z,
      TileRotation tileRotation = null)
    {
      this.RequireDirections();
      tileRotation = tileRotation ?? new TileRotation();
      foreach (Rotation rotation in tileRotation.RotationGroup)
      {
        (int x3, int y3) = TopoArrayUtils.RotateVector(this.directions.Type, x, y, rotation);
        this.AddAdjacency((IList<Tile>) tileRotation.Rotate((IEnumerable<Tile>) src, rotation).ToList<Tile>(), (IList<Tile>) tileRotation.Rotate((IEnumerable<Tile>) dest, rotation).ToList<Tile>(), x3, y3, z);
      }
    }

    public void AddAdjacency(IList<Tile> src, IList<Tile> dest, int x, int y, int z)
    {
      this.RequireDirections();
      this.AddAdjacency(src, dest, this.directions.GetDirection(x, y, z));
    }

    public void AddAdjacency(IList<Tile> src, IList<Tile> dest, Direction dir)
    {
      this.RequireDirections();
      foreach (Tile src1 in (IEnumerable<Tile>) src)
      {
        foreach (Tile dest1 in (IEnumerable<Tile>) dest)
          this.AddAdjacency(src1, dest1, dir);
      }
    }

    public void AddAdjacency(Tile src, Tile dest, int x, int y, int z)
    {
      this.RequireDirections();
      Direction direction = this.directions.GetDirection(x, y, z);
      this.AddAdjacency(src, dest, direction);
    }

    public void AddAdjacency(Tile src, Tile dest, Direction d)
    {
      Direction direction = this.directions.Inverse(d);
      int pattern1 = this.GetPattern(src);
      int pattern2 = this.GetPattern(dest);
      this.propagator[pattern1][(int) d].Add(pattern2);
      this.propagator[pattern2][(int) direction].Add(pattern1);
    }

    public void AddAdjacency(AdjacentModel.Adjacency adjacency) => this.AddAdjacency((IList<Tile>) adjacency.Src, (IList<Tile>) adjacency.Dest, adjacency.Direction);

    public bool IsAdjacent(Tile src, Tile dest, Direction d)
    {
      int pattern1 = this.GetPattern(src);
      int pattern2 = this.GetPattern(dest);
      return this.propagator[pattern1][(int) d].Contains(pattern2);
    }

    public void AddSample(ITopoArray<Tile> sample, TileRotation tileRotation = null)
    {
      foreach (ITopoArray<Tile> rotatedSample in OverlappingAnalysis.GetRotatedSamples(sample, tileRotation))
        this.AddSample(rotatedSample);
    }

    public void AddSample(ITopoArray<Tile> sample)
    {
      GridTopology topology = sample.Topology.AsGridTopology();
      this.SetDirections(topology.Directions);
      int width = topology.Width;
      int height = topology.Height;
      int depth = topology.Depth;
      int count = topology.Directions.Count;
      for (int z = 0; z < depth; ++z)
      {
        for (int y = 0; y < height; ++y)
        {
          for (int x = 0; x < width; ++x)
          {
            int index1 = topology.GetIndex(x, y, z);
            if (topology.ContainsIndex(index1))
            {
              int pattern1 = this.GetPattern(sample.Get(x, y, z));
              ++this.frequencies[pattern1];
              for (int index2 = 0; index2 < count; ++index2)
              {
                int destx;
                int desty;
                int destz;
                if (topology.TryMove(x, y, z, (Direction) index2, out destx, out desty, out destz))
                {
                  int pattern2 = this.GetPattern(sample.Get(destx, desty, destz));
                  this.propagator[pattern1][index2].Add(pattern2);
                }
              }
            }
          }
        }
      }
    }

    internal override TileModelMapping GetTileModelMapping(ITopology topology)
    {
      GridTopology gridTopology = topology.AsGridTopology();
      this.RequireDirections();
      this.SetDirections(gridTopology.Directions);
      if (this.frequencies.Sum() == 0.0)
        throw new Exception("No tiles have assigned frequences.");
      PatternModel patternModel = new PatternModel()
      {
        Propagator = this.propagator.Select<HashSet<int>[], int[][]>((Func<HashSet<int>[], int[][]>) (x => ((IEnumerable<HashSet<int>>) x).Select<HashSet<int>, int[]>((Func<HashSet<int>, int[]>) (y => y.ToArray<int>())).ToArray<int[]>())).ToArray<int[][]>(),
        Frequencies = this.frequencies.ToArray()
      };
      Dictionary<int, IReadOnlyDictionary<Tile, ISet<int>>> dictionary1 = new Dictionary<int, IReadOnlyDictionary<Tile, ISet<int>>>()
      {
        {
          0,
          (IReadOnlyDictionary<Tile, ISet<int>>) this.tilesToPatterns.ToDictionary<KeyValuePair<Tile, int>, Tile, ISet<int>>((Func<KeyValuePair<Tile, int>, Tile>) (kv => kv.Key), (Func<KeyValuePair<Tile, int>, ISet<int>>) (kv => (ISet<int>) new HashSet<int>()
          {
            kv.Value
          }))
        }
      };
      Dictionary<int, IReadOnlyDictionary<int, Tile>> dictionary2 = new Dictionary<int, IReadOnlyDictionary<int, Tile>>()
      {
        {
          0,
          (IReadOnlyDictionary<int, Tile>) this.tilesToPatterns.ToDictionary<KeyValuePair<Tile, int>, int, Tile>((Func<KeyValuePair<Tile, int>, int>) (x => x.Value), (Func<KeyValuePair<Tile, int>, Tile>) (x => x.Key))
        }
      };
      return new TileModelMapping()
      {
        PatternTopology = (ITopology) gridTopology,
        PatternModel = patternModel,
        PatternsToTilesByOffset = (IDictionary<int, IReadOnlyDictionary<int, Tile>>) dictionary2,
        TilesToPatternsByOffset = (IDictionary<int, IReadOnlyDictionary<Tile, ISet<int>>>) dictionary1,
        TileCoordToPatternCoordIndexAndOffset = (ITopoArray<(Point, int, int)>) null
      };
    }

    public override void MultiplyFrequency(Tile tile, double multiplier) => this.frequencies[this.tilesToPatterns[tile]] *= multiplier;

    private int GetPattern(Tile tile)
    {
      int count = this.directions.Count;
      int index1;
      if (!this.tilesToPatterns.TryGetValue(tile, out index1))
      {
        index1 = this.tilesToPatterns[tile] = this.tilesToPatterns.Count;
        this.frequencies.Add(0.0);
        this.propagator.Add(new HashSet<int>[count]);
        for (int index2 = 0; index2 < count; ++index2)
          this.propagator[index1][index2] = new HashSet<int>();
      }
      return index1;
    }

    public class Adjacency
    {
      public Tile[] Src { get; set; }

      public Tile[] Dest { get; set; }

      public Direction Direction { get; set; }
    }
  }
}