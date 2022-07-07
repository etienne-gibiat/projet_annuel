using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
  
  public class EdgedPathSpec : IPathSpec
  {
    public IDictionary<Tile, ISet<Direction>> Exits { get; set; }

    public Point[] RelevantCells { get; set; }

    public ISet<Tile> RelevantTiles { get; set; }

    public TileRotation TileRotation { get; set; }

    public IPathView MakeView(TilePropagator tilePropagator) => (IPathView) new EdgedPathView(this, tilePropagator);
  }
  
    internal class EdgedPathView : IPathView
  {
    private ISet<Tile> endPointTiles;
    private IDictionary<Tile, ISet<Direction>> exits;
    private TilePropagatorTileSet pathTileSet;
    private SelectedTracker pathSelectedTracker;
    private TilePropagatorTileSet endPointTileSet;
    private SelectedTracker endPointSelectedTracker;
    private Dictionary<Direction, TilePropagatorTileSet> tileSetByExit;
    private Dictionary<Direction, SelectedTracker> trackerByExit;
    private bool hasEndPoints;
    private TilePropagator propagator;
    private ITopology topology;
    private List<int> endPointIndices;
    private static readonly int[] Empty = new int[0];

    public EdgedPathView(EdgedPathSpec spec, TilePropagator propagator)
    {
      if (spec.TileRotation != null)
      {
        this.exits = (IDictionary<Tile, ISet<Direction>>) new Dictionary<Tile, ISet<Direction>>();
        foreach (KeyValuePair<Tile, ISet<Direction>> exit in (IEnumerable<KeyValuePair<Tile, ISet<Direction>>>) spec.Exits)
        {
          foreach (Rotation rotation in spec.TileRotation.RotationGroup)
          {
            Rotation rot = rotation;
            Tile result;
            if (spec.TileRotation.Rotate(exit.Key, rot, out result))
            {
              HashSet<Direction> directionSet = new HashSet<Direction>(exit.Value.Select<Direction, Direction>(new Func<Direction, Direction>(Rotate)));
              this.exits[result] = (ISet<Direction>) directionSet;
            }

            Direction Rotate(Direction d) => TopoArrayUtils.RotateDirection(propagator.Topology.AsGridTopology().Directions, d, rot);
          }
        }
        this.endPointTiles = spec.RelevantTiles == null ? (ISet<Tile>) null : (ISet<Tile>) new HashSet<Tile>(spec.TileRotation.RotateAll((IEnumerable<Tile>) spec.RelevantTiles));
      }
      else
      {
        this.exits = spec.Exits;
        this.endPointTiles = spec.RelevantTiles;
      }
      this.pathTileSet = propagator.CreateTileSet((IEnumerable<Tile>) this.exits.Keys);
      this.pathSelectedTracker = propagator.CreateSelectedTracker(this.pathTileSet);
      this.Graph = EdgedPathView.CreateEdgedGraph(propagator.Topology);
      this.propagator = propagator;
      this.topology = propagator.Topology;
      int nodesPerIndex = this.GetNodesPerIndex();
      this.CouldBePath = new bool[propagator.Topology.IndexCount * nodesPerIndex];
      this.MustBePath = new bool[propagator.Topology.IndexCount * nodesPerIndex];
      this.tileSetByExit = this.exits.SelectMany<KeyValuePair<Tile, ISet<Direction>>, Tuple<Tile, Direction>>((Func<KeyValuePair<Tile, ISet<Direction>>, IEnumerable<Tuple<Tile, Direction>>>) (kv => kv.Value.Select<Direction, Tuple<Tile, Direction>>((Func<Direction, Tuple<Tile, Direction>>) (e => Tuple.Create<Tile, Direction>(kv.Key, e))))).GroupBy<Tuple<Tile, Direction>, Direction, Tile>((Func<Tuple<Tile, Direction>, Direction>) (x => x.Item2), (Func<Tuple<Tile, Direction>, Tile>) (x => x.Item1)).ToDictionary<IGrouping<Direction, Tile>, Direction, TilePropagatorTileSet>((Func<IGrouping<Direction, Tile>, Direction>) (g => g.Key), new Func<IGrouping<Direction, Tile>, TilePropagatorTileSet>(propagator.CreateTileSet));
      this.trackerByExit = this.tileSetByExit.ToDictionary<KeyValuePair<Direction, TilePropagatorTileSet>, Direction, SelectedTracker>((Func<KeyValuePair<Direction, TilePropagatorTileSet>, Direction>) (kv => kv.Key), (Func<KeyValuePair<Direction, TilePropagatorTileSet>, SelectedTracker>) (kv => propagator.CreateSelectedTracker(kv.Value)));
      this.hasEndPoints = spec.RelevantCells != null || spec.RelevantTiles != null;
      if (this.hasEndPoints)
      {
        this.CouldBeRelevant = new bool[propagator.Topology.IndexCount * nodesPerIndex];
        this.MustBeRelevant = new bool[propagator.Topology.IndexCount * nodesPerIndex];
        this.endPointIndices = spec.RelevantCells == null ? (List<int>) null : ((IEnumerable<Point>) spec.RelevantCells).Select<Point, int>((Func<Point, int>) (p => propagator.Topology.GetIndex(p.X, p.Y, p.Z))).ToList<int>();
        this.endPointTileSet = this.endPointTiles != null ? propagator.CreateTileSet((IEnumerable<Tile>) this.endPointTiles) : (TilePropagatorTileSet) null;
        this.endPointSelectedTracker = this.endPointTiles != null ? propagator.CreateSelectedTracker(this.endPointTileSet) : (SelectedTracker) null;
      }
      else
      {
        this.CouldBeRelevant = this.CouldBePath;
        this.MustBeRelevant = this.MustBePath;
        this.endPointTileSet = this.pathTileSet;
      }
    }

    public SelectedTracker PathSelectedTracker => this.pathSelectedTracker;

    public Dictionary<Direction, SelectedTracker> TrackerByExit => this.trackerByExit;

    public Dictionary<Direction, TilePropagatorTileSet> TileSetByExit => this.tileSetByExit;

    public PathConstraintUtils.SimpleGraph Graph { get; }

    public bool[] CouldBePath { get; }

    public bool[] MustBePath { get; }

    public bool[] CouldBeRelevant { get; }

    public bool[] MustBeRelevant { get; }

    public void Update()
    {
      int nodesPerIndex = this.GetNodesPerIndex();
      int indexCount = this.topology.IndexCount;
      foreach (KeyValuePair<Direction, SelectedTracker> keyValuePair in this.trackerByExit)
      {
        Direction key = keyValuePair.Key;
        SelectedTracker selectedTracker = keyValuePair.Value;
        for (int index = 0; index < indexCount; ++index)
        {
          Quadstate quadstate = selectedTracker.GetQuadstate(index);
          this.CouldBePath[(int) (index * nodesPerIndex + 1 + key)] = quadstate.Possible();
          this.MustBePath[(int) (index * nodesPerIndex + 1 + key)] = quadstate.IsYes();
        }
      }
      for (int index = 0; index < indexCount; ++index)
      {
        Quadstate quadstate = this.pathSelectedTracker.GetQuadstate(index);
        this.CouldBePath[index * nodesPerIndex] = quadstate.Possible();
        this.MustBePath[index * nodesPerIndex] = quadstate.IsYes();
      }
      if (!this.hasEndPoints)
        return;
      if (this.endPointIndices != null)
      {
        foreach (int endPointIndex in this.endPointIndices)
          this.CouldBeRelevant[endPointIndex * nodesPerIndex] = this.MustBeRelevant[endPointIndex * nodesPerIndex] = true;
      }
      if (this.endPointSelectedTracker == null)
        return;
      for (int index = 0; index < indexCount; ++index)
      {
        Quadstate quadstate = this.endPointSelectedTracker.GetQuadstate(index);
        this.CouldBeRelevant[index * nodesPerIndex] = quadstate.Possible();
        this.MustBeRelevant[index * nodesPerIndex] = quadstate.IsYes();
      }
    }

    public void SelectPath(int node)
    {
      (int index, Direction? nullable) = this.Unpack(node, this.GetNodesPerIndex());
      int x;
      int y;
      int z;
      this.topology.GetCoord(index, out x, out y, out z);
      if (!nullable.HasValue)
      {
        int num1 = (int) this.propagator.Select(x, y, z, this.pathTileSet);
      }
      else
      {
        TilePropagatorTileSet tiles;
        if (this.MustBePath[node] || !this.tileSetByExit.TryGetValue(nullable.Value, out tiles))
          return;
        int num2 = (int) this.propagator.Select(x, y, z, tiles);
      }
    }

    public void BanPath(int node)
    {
      (int index, Direction? nullable) = this.Unpack(node, this.GetNodesPerIndex());
      int x;
      int y;
      int z;
      this.topology.GetCoord(index, out x, out y, out z);
      if (!nullable.HasValue)
      {
        int num1 = (int) this.propagator.Ban(x, y, z, this.pathTileSet);
      }
      else
      {
        TilePropagatorTileSet tiles;
        if (!this.tileSetByExit.TryGetValue(nullable.Value, out tiles))
          return;
        int num2 = (int) this.propagator.Ban(x, y, z, tiles);
      }
    }

    public void BanRelevant(int node)
    {
      (int index, Direction? nullable) = this.Unpack(node, this.GetNodesPerIndex());
      int x;
      int y;
      int z;
      this.topology.GetCoord(index, out x, out y, out z);
      if (nullable.HasValue)
        return;
      int num = (int) this.propagator.Ban(x, y, z, this.endPointTileSet);
    }

    private int GetNodesPerIndex() => this.topology.DirectionsCount + 1;

    private (int, Direction?) Unpack(int node, int nodesPerIndex)
    {
      int num1 = node / nodesPerIndex;
      int num2 = node - num1 * nodesPerIndex;
      return num2 == 0 ? (num1, new Direction?()) : (num1, new Direction?((Direction) (num2 - 1)));
    }

    private int Pack(int index, Direction? dir, int nodesPerIndex) => index * nodesPerIndex + (!dir.HasValue ? 0 : (int) (1 + dir.Value));

    private static PathConstraintUtils.SimpleGraph CreateEdgedGraph(
      ITopology topology)
    {
      int nodesPerIndex = topology.DirectionsCount + 1;
      int length = topology.IndexCount * nodesPerIndex;
      int[][] numArray = new int[length][];
      foreach (int index1 in topology.GetIndices())
      {
        List<int> intList = new List<int>();
        for (int index2 = 0; index2 < topology.DirectionsCount; ++index2)
        {
          Direction direction = (Direction) index2;
          intList.Add(GetDirNodeId(index1, direction));
          int dest;
          Direction inverseDirection;
          if (topology.TryMove(index1, direction, out dest, out inverseDirection, out EdgeLabel _))
            numArray[GetDirNodeId(index1, direction)] = new int[2]
            {
              GetNodeId(index1),
              GetDirNodeId(dest, inverseDirection)
            };
          else
            numArray[GetDirNodeId(index1, direction)] = new int[1]
            {
              GetNodeId(index1)
            };
        }
        numArray[GetNodeId(index1)] = intList.ToArray();
      }
      return new PathConstraintUtils.SimpleGraph()
      {
        NodeCount = length,
        Neighbours = numArray
      };

      int GetNodeId(int index) => index * nodesPerIndex;

      int GetDirNodeId(int index, Direction direction) => (int) (index * nodesPerIndex + 1 + direction);
    }
  }
}