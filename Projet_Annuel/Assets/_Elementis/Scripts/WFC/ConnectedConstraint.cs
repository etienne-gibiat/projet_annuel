using System;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
  public interface IPathSpec
  {
    IPathView MakeView(TilePropagator tilePropagator);
  }
  
    public class ConnectedConstraint : ITileConstraint
  {
    private IPathView pathView;
    private bool pathViewIsFresh;

    public IPathSpec PathSpec { get; set; }

    public bool UsePickHeuristic { get; set; }

    public void Init(TilePropagator propagator)
    {
      if (this.pathViewIsFresh)
        this.pathViewIsFresh = false;
      else
        this.pathView = this.PathSpec.MakeView(propagator);
      this.pathView.Init();
    }

    public void Check(TilePropagator propagator)
    {
      this.pathView.Update();
      PathConstraintUtils.AtrticulationPointsInfo articulationPoints = PathConstraintUtils.GetArticulationPoints(this.pathView.Graph, this.pathView.CouldBePath, this.pathView.MustBeRelevant);
      bool[] isArticulation = articulationPoints.IsArticulation;
      if (articulationPoints.ComponentCount > 1)
      {
        propagator.SetContradiction();
      }
      else
      {
        for (int index = 0; index < this.pathView.Graph.NodeCount; ++index)
        {
          if (isArticulation[index] && !this.pathView.MustBePath[index])
            this.pathView.SelectPath(index);
        }
        if (articulationPoints.ComponentCount <= 0)
          return;
        int?[] component = articulationPoints.Component;
        for (int index = 0; index < this.pathView.Graph.NodeCount; ++index)
        {
          if (!component[index].HasValue && this.pathView.CouldBeRelevant[index])
            this.pathView.BanRelevant(index);
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
      this.pathView = this.PathSpec.MakeView(propagator);
      this.pathViewIsFresh = true;
      if (this.pathView is EdgedPathView pathView)
        return (IPickHeuristic) new ConnectedConstraint.FollowPathHeuristic(randomPicker, randomDouble, propagator, tileModelMapping, fallbackHeuristic, pathView);
      throw new NotImplementedException();
    }

    private class FollowPathHeuristic : IPickHeuristic
    {
      private readonly IRandomPicker randomPicker;
      private readonly Func<double> randomDouble;
      private readonly TilePropagator propagator;
      private readonly TileModelMapping tileModelMapping;
      private readonly IPickHeuristic fallbackHeuristic;
      private readonly EdgedPathView edgedPathView;

      public FollowPathHeuristic(
        IRandomPicker randomPicker,
        Func<double> randomDouble,
        TilePropagator propagator,
        TileModelMapping tileModelMapping,
        IPickHeuristic fallbackHeuristic,
        EdgedPathView edgedPathView)
      {
        this.randomPicker = randomPicker;
        this.randomDouble = randomDouble;
        this.propagator = propagator;
        this.tileModelMapping = tileModelMapping;
        this.fallbackHeuristic = fallbackHeuristic;
        this.edgedPathView = edgedPathView;
      }

      public void PickObservation(out int index, out int pattern)
      {
        ITopology topology = this.propagator.Topology;
        SelectedTracker t = this.edgedPathView.PathSelectedTracker;
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
            if (topology.TryMove(i, (Direction) index1, out dest, out inverseDirection, out EdgeLabel _) && this.edgedPathView.TrackerByExit.TryGetValue(inverseDirection, out selectedTracker) && selectedTracker.GetQuadstate(dest).IsYes())
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