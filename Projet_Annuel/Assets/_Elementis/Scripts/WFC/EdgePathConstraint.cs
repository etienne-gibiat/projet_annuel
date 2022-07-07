using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    [Obsolete("Use ConnectedConstraint instead")]
  public class EdgedPathConstraint : ITileConstraint
  {
    private TilePropagatorTileSet pathTileSet;
    private SelectedTracker pathSelectedTracker;
    private TilePropagatorTileSet endPointTileSet;
    private SelectedTracker endPointSelectedTracker;
    private PathConstraintUtils.SimpleGraph graph;
    private IDictionary<Direction, TilePropagatorTileSet> tilesByExit;
    private IDictionary<Direction, SelectedTracker> trackerByExit;
    private static readonly int[] Empty = new int[0];

    private IDictionary<Tile, ISet<Direction>> actualExits { get; set; }

    public IDictionary<Tile, ISet<Direction>> Exits { get; set; }

    public Point[] EndPoints { get; set; }

    public ISet<Tile> EndPointTiles { get; set; }

    public TileRotation TileRotation { get; set; }

    public bool UsePickHeuristic { get; set; }

    public EdgedPathConstraint(
      IDictionary<Tile, ISet<Direction>> exits,
      Point[] endPoints = null,
      TileRotation tileRotation = null)
    {
      this.Exits = exits;
      this.EndPoints = endPoints;
      this.TileRotation = tileRotation;
    }

    public void Init(TilePropagator propagator)
    {
      ISet<Tile> tileSet;
      if (this.TileRotation != null)
      {
        this.actualExits = (IDictionary<Tile, ISet<Direction>>) new Dictionary<Tile, ISet<Direction>>();
        foreach (KeyValuePair<Tile, ISet<Direction>> exit in (IEnumerable<KeyValuePair<Tile, ISet<Direction>>>) this.Exits)
        {
          foreach (Rotation rotation in this.TileRotation.RotationGroup)
          {
            Rotation rot = rotation;
            Tile result;
            if (this.TileRotation.Rotate(exit.Key, rot, out result))
            {
              HashSet<Direction> directionSet = new HashSet<Direction>(exit.Value.Select<Direction, Direction>(new Func<Direction, Direction>(Rotate)));
              this.actualExits[result] = (ISet<Direction>) directionSet;
            }

            Direction Rotate(Direction d) => TopoArrayUtils.RotateDirection(propagator.Topology.AsGridTopology().Directions, d, rot);
          }
        }
        tileSet = this.EndPointTiles == null ? (ISet<Tile>) null : (ISet<Tile>) new HashSet<Tile>(this.TileRotation.RotateAll((IEnumerable<Tile>) this.EndPointTiles));
      }
      else
      {
        this.actualExits = this.Exits;
        tileSet = this.EndPointTiles;
      }
      this.pathTileSet = propagator.CreateTileSet((IEnumerable<Tile>) this.Exits.Keys);
      this.pathSelectedTracker = propagator.CreateSelectedTracker(this.pathTileSet);
      this.endPointTileSet = this.EndPointTiles != null ? propagator.CreateTileSet((IEnumerable<Tile>) tileSet) : (TilePropagatorTileSet) null;
      this.endPointSelectedTracker = this.EndPointTiles != null ? propagator.CreateSelectedTracker(this.endPointTileSet) : (SelectedTracker) null;
      this.graph = EdgedPathConstraint.CreateEdgedGraph(propagator.Topology);
      this.tilesByExit = (IDictionary<Direction, TilePropagatorTileSet>) this.actualExits.SelectMany<KeyValuePair<Tile, ISet<Direction>>, Tuple<Tile, Direction>>((Func<KeyValuePair<Tile, ISet<Direction>>, IEnumerable<Tuple<Tile, Direction>>>) (kv => kv.Value.Select<Direction, Tuple<Tile, Direction>>((Func<Direction, Tuple<Tile, Direction>>) (e => Tuple.Create<Tile, Direction>(kv.Key, e))))).GroupBy<Tuple<Tile, Direction>, Direction, Tile>((Func<Tuple<Tile, Direction>, Direction>) (x => x.Item2), (Func<Tuple<Tile, Direction>, Tile>) (x => x.Item1)).ToDictionary<IGrouping<Direction, Tile>, Direction, TilePropagatorTileSet>((Func<IGrouping<Direction, Tile>, Direction>) (g => g.Key), new Func<IGrouping<Direction, Tile>, TilePropagatorTileSet>(propagator.CreateTileSet));
      this.trackerByExit = (IDictionary<Direction, SelectedTracker>) this.tilesByExit.ToDictionary<KeyValuePair<Direction, TilePropagatorTileSet>, Direction, SelectedTracker>((Func<KeyValuePair<Direction, TilePropagatorTileSet>, Direction>) (kv => kv.Key), (Func<KeyValuePair<Direction, TilePropagatorTileSet>, SelectedTracker>) (kv => propagator.CreateSelectedTracker(kv.Value)));
      this.Check(propagator, true);
    }

    public void Check(TilePropagator propagator) => this.Check(propagator, false);

    private void Check(TilePropagator propagator, bool init)
    {
      ITopology topology = propagator.Topology;
      int num1 = topology.Width * topology.Height * topology.Depth;
      int num2 = topology.DirectionsCount + 1;
      bool[] walkable = new bool[num1 * num2];
      bool[] flagArray1 = new bool[num1 * num2];
      bool[] flagArray2 = new bool[num1 * num2];
      foreach (KeyValuePair<Direction, SelectedTracker> keyValuePair in (IEnumerable<KeyValuePair<Direction, SelectedTracker>>) this.trackerByExit)
      {
        Direction key = keyValuePair.Key;
        SelectedTracker selectedTracker = keyValuePair.Value;
        for (int index = 0; index < num1; ++index)
        {
          Quadstate quadstate = selectedTracker.GetQuadstate(index);
          walkable[(int) (index * num2 + 1 + key)] = quadstate.Possible();
          flagArray2[(int) (index * num2 + 1 + key)] = quadstate.IsYes();
        }
      }
      for (int index = 0; index < num1; ++index)
      {
        Quadstate quadstate = this.pathSelectedTracker.GetQuadstate(index);
        walkable[index * num2] = quadstate.Possible();
        flagArray1[index * num2] = quadstate.IsYes();
      }
      bool flag = this.EndPoints != null || this.EndPointTiles != null;
      bool[] relevant;
      if (!flag)
      {
        relevant = flagArray1;
      }
      else
      {
        relevant = new bool[num1 * num2];
        int num3 = 0;
        if (this.EndPoints != null)
        {
          foreach (Point endPoint in this.EndPoints)
          {
            int index = topology.GetIndex(endPoint.X, endPoint.Y, endPoint.Z);
            relevant[index * num2] = true;
            ++num3;
          }
        }
        if (this.EndPointTiles != null)
        {
          for (int index = 0; index < num1; ++index)
          {
            if (this.endPointSelectedTracker.IsSelected(index))
            {
              relevant[index * num2] = true;
              ++num3;
            }
          }
        }
        if (num3 == 0)
          return;
      }
      if (init)
      {
        for (int index = 0; index < num1; ++index)
        {
          if (relevant[index * num2])
          {
            int x;
            int y;
            int z;
            topology.GetCoord(index, out x, out y, out z);
            int num4 = (int) propagator.Select(x, y, z, this.pathTileSet);
          }
        }
      }
      PathConstraintUtils.AtrticulationPointsInfo articulationPoints = PathConstraintUtils.GetArticulationPoints(this.graph, walkable, relevant);
      bool[] isArticulation = articulationPoints.IsArticulation;
      if (articulationPoints.ComponentCount > 1)
      {
        propagator.SetContradiction();
      }
      else
      {
        for (int index1 = 0; index1 < num1; ++index1)
        {
          int x;
          int y;
          int z;
          topology.GetCoord(index1, out x, out y, out z);
          if (isArticulation[index1 * num2] && !flagArray1[index1 * num2])
          {
            int num5 = (int) propagator.Select(x, y, z, this.pathTileSet);
          }
          for (int index2 = 0; index2 < topology.DirectionsCount; ++index2)
          {
            TilePropagatorTileSet tiles;
            if (isArticulation[index1 * num2 + 1 + index2] && !flagArray2[index1 * num2 + 1 + index2] && this.tilesByExit.TryGetValue((Direction) index2, out tiles))
            {
              int num6 = (int) propagator.Select(x, y, z, tiles);
            }
          }
        }
        if (articulationPoints.ComponentCount <= 0)
          return;
        int?[] component = articulationPoints.Component;
        TilePropagatorTileSet tiles1 = flag ? this.endPointTileSet : this.pathTileSet;
        if (tiles1 == null)
          return;
        for (int index = 0; index < num1; ++index)
        {
          if (!component[index * num2].HasValue)
          {
            int x;
            int y;
            int z;
            topology.GetCoord(index, out x, out y, out z);
            int num7 = (int) propagator.Ban(x, y, z, tiles1);
          }
        }
      }
    }

    internal IPickHeuristic GetHeuristic(
      IRandomPicker randomPicker,
      Func<double> randomDouble,
      TilePropagator propagator,
      TileModelMapping tileModelMapping,
      IPickHeuristic fallbackHeuristic)
    {
      return (IPickHeuristic) new EdgedPathConstraint.FollowPathHeuristic(randomPicker, randomDouble, propagator, tileModelMapping, fallbackHeuristic, this);
    }

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
          int dest;
          Direction inverseDirection;
          if (topology.TryMove(index1, direction, out dest, out inverseDirection, out EdgeLabel _))
          {
            intList.Add(GetDirNodeId(index1, direction));
            numArray[GetDirNodeId(index1, direction)] = new int[2]
            {
              GetNodeId(index1),
              GetDirNodeId(dest, inverseDirection)
            };
          }
          else
            numArray[GetDirNodeId(index1, direction)] = EdgedPathConstraint.Empty;
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

    private class FollowPathHeuristic : IPickHeuristic
    {
      private readonly IRandomPicker randomPicker;
      private readonly Func<double> randomDouble;
      private readonly TilePropagator propagator;
      private readonly TileModelMapping tileModelMapping;
      private readonly IPickHeuristic fallbackHeuristic;
      private readonly EdgedPathConstraint pathConstraint;

      public FollowPathHeuristic(
        IRandomPicker randomPicker,
        Func<double> randomDouble,
        TilePropagator propagator,
        TileModelMapping tileModelMapping,
        IPickHeuristic fallbackHeuristic,
        EdgedPathConstraint pathConstraint)
      {
        this.randomPicker = randomPicker;
        this.randomDouble = randomDouble;
        this.propagator = propagator;
        this.tileModelMapping = tileModelMapping;
        this.fallbackHeuristic = fallbackHeuristic;
        this.pathConstraint = pathConstraint;
      }

      public void PickObservation(out int index, out int pattern)
      {
        ITopology topology = this.propagator.Topology;
        SelectedTracker t = this.pathConstraint.pathSelectedTracker;
        int[] array = Enumerable.Range(0, topology.IndexCount).Select<int, int>((Func<int, int>) (i =>
        {
          if (!topology.ContainsIndex(i))
            return -1;
          Quadstate quadstate = t.GetQuadstate(i);
          if (quadstate.IsYes())
            return 2;
          if (quadstate.IsNo())
            return 0;
          for (int index1 = 0; index1 < topology.DirectionsCount; ++index1)
          {
            int dest;
            Direction inverseDirection;
            SelectedTracker selectedTracker;
            if (topology.TryMove(i, (Direction) index1, out dest, out inverseDirection, out EdgeLabel _) && this.pathConstraint.trackerByExit.TryGetValue(inverseDirection, out selectedTracker) && selectedTracker.GetQuadstate(dest).IsYes())
              return 1;
          }
          return 0;
        })).ToArray<int>();
        if (this.tileModelMapping.PatternCoordToTileCoordIndexAndOffset != null)
          throw new NotImplementedException();
        int[] externalPriority = array;
        index = this.randomPicker.GetRandomIndex(this.randomDouble, externalPriority);
        if (index == -1)
        {
          this.fallbackHeuristic.PickObservation(out index, out pattern);
          this.propagator.Topology.GetCoord(index, out int _, out int _, out int _);
        }
        else
        {
          this.propagator.Topology.GetCoord(index, out int _, out int _, out int _);
          pattern = this.randomPicker.GetRandomPossiblePatternAt(index, this.randomDouble);
        }
      }
    }
  }
}