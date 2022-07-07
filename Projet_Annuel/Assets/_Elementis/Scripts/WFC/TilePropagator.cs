using System;
using System.Collections.Generic;
using System.Linq;

namespace _Elementis.Scripts.WFC
{
    public class TilePropagator
  {
    private readonly WavePropagator wavePropagator;
    private readonly ITopology topology;
    private readonly TileModel tileModel;
    private readonly TileModelMapping tileModelMapping;

    public TilePropagator(
      TileModel tileModel,
      ITopology topology,
      bool backtrack = false,
      ITileConstraint[] constraints = null)
      : this(tileModel, topology, new TilePropagatorOptions()
      {
        BackTrackDepth = backtrack ? -1 : 0,
        Constraints = constraints
      })
    {
    }

    [Obsolete("Use TilePropagatorOptions")]
    public TilePropagator(
      TileModel tileModel,
      ITopology topology,
      bool backtrack,
      ITileConstraint[] constraints,
      Random random)
      : this(tileModel, topology, new TilePropagatorOptions()
      {
        BackTrackDepth = backtrack ? -1 : 0,
        Constraints = constraints,
        Random = random
      })
    {
    }

    public TilePropagator(TileModel tileModel, ITopology topology, TilePropagatorOptions options)
    {
      TilePropagator propagator = this;
      this.tileModel = tileModel;
      this.topology = topology;
      tileModelMapping = tileModel.GetTileModelMapping(topology);
      ITopology patternTopology = tileModelMapping.PatternTopology;
      PatternModel patternModel = tileModelMapping.PatternModel;
      ITileConstraint[] constraints = options.Constraints;
      IWaveConstraint[] array = ((constraints != null ? constraints.Select(x => new TileConstraintAdaptor(x, propagator)).ToArray() : (IEnumerable<IWaveConstraint>) null) ?? Enumerable.Empty<IWaveConstraint>()).ToArray();
      Func<double> func1 = options.RandomDouble;
      if (func1 == null)
      {
        Random random = options.Random ?? new Random();
        func1 = () => random.NextDouble();
      }
      Func<double> func2 = func1;
      WavePropagatorOptions options1 = new WavePropagatorOptions()
      {
        BackTrackDepth = options.BackTrackDepth,
        RandomDouble = func2,
        Constraints = array,
        PickHeuristicFactory = w => propagator.MakePickHeuristic(w, options),
        Clear = false,
        ModelConstraintAlgorithm = options.ModelConstraintAlgorithm
      };
      wavePropagator = new WavePropagator(patternModel, patternTopology, options1);
      int num = (int) wavePropagator.Clear();
    }

    private IPickHeuristic MakePickHeuristic(
      WavePropagator wavePropagator,
      TilePropagatorOptions options)
    {
      FrequencySet[] frequencySets = options.Weights == null ? null : GetFrequencySets(options.Weights, tileModelMapping);
      Func<double> randomDouble = wavePropagator.RandomDouble;
      ITopology topology = wavePropagator.Topology;
      ITileConstraint[] constraints1 = options.Constraints;
      EdgedPathConstraint edgedPathConstraint = constraints1 != null ? constraints1.OfType<EdgedPathConstraint>().FirstOrDefault() : null;
      bool flag1 = edgedPathConstraint != null && edgedPathConstraint.UsePickHeuristic;
      ITileConstraint[] constraints2 = options.Constraints;
      ConnectedConstraint connectedConstraint = constraints2 != null ? constraints2.OfType<ConnectedConstraint>().FirstOrDefault() : null;
      bool flag2 = connectedConstraint != null && connectedConstraint.UsePickHeuristic;
      IRandomPicker randomPicker;
      if (options.PickHeuristicType == PickHeuristicType.Ordered)
        randomPicker = new OrderedRandomPicker(wavePropagator.Wave, wavePropagator.Frequencies, topology.Mask);
      else if (frequencySets != null)
      {
        ArrayPriorityEntropyTracker priorityEntropyTracker = new ArrayPriorityEntropyTracker(wavePropagator.Wave, frequencySets, topology.Mask);
        priorityEntropyTracker.Reset();
        wavePropagator.AddTracker(priorityEntropyTracker);
        randomPicker = priorityEntropyTracker;
      }
      else if (flag1 | flag2)
      {
        EntropyTracker entropyTracker = new EntropyTracker(wavePropagator.Wave, wavePropagator.Frequencies, topology.Mask);
        entropyTracker.Reset();
        wavePropagator.AddTracker(entropyTracker);
        randomPicker = entropyTracker;
      }
      else
      {
        HeapEntropyTracker heapEntropyTracker = new HeapEntropyTracker(wavePropagator.Wave, wavePropagator.Frequencies, topology.Mask, randomDouble);
        heapEntropyTracker.Reset();
        wavePropagator.AddTracker(heapEntropyTracker);
        randomPicker = heapEntropyTracker;
      }
      IPickHeuristic fallbackHeuristic = new RandomPickerHeuristic(randomPicker, randomDouble);
      if (flag1)
        fallbackHeuristic = edgedPathConstraint.GetHeuristic(randomPicker, randomDouble, this, tileModelMapping, fallbackHeuristic);
      if (flag2)
        fallbackHeuristic = connectedConstraint.GetHeuristic(randomPicker, randomDouble, this, tileModelMapping, fallbackHeuristic);
      return fallbackHeuristic;
    }

    private void TileCoordToPatternCoord(
      int x,
      int y,
      int z,
      out int px,
      out int py,
      out int pz,
      out int offset)
    {
      tileModelMapping.GetTileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
    }

    private static FrequencySet[] GetFrequencySets(
      ITopoArray<IDictionary<Tile, PriorityAndWeight>> weights,
      TileModelMapping tileModelMapping)
    {
      FrequencySet[] frequencySetArray = new FrequencySet[tileModelMapping.PatternTopology.IndexCount];
      foreach (int index1 in tileModelMapping.PatternTopology.GetIndices())
      {
        if (tileModelMapping.PatternCoordToTileCoordIndexAndOffset != null)
          throw new NotImplementedException();
        int index2 = index1;
        int key = 0;
        IDictionary<Tile, PriorityAndWeight> dictionary = weights.Get(index2);
        double[] weights1 = new double[tileModelMapping.PatternModel.PatternCount];
        int[] priorities = new int[tileModelMapping.PatternModel.PatternCount];
        foreach (KeyValuePair<Tile, PriorityAndWeight> keyValuePair in dictionary)
        {
          int index3 = tileModelMapping.TilesToPatternsByOffset[key][keyValuePair.Key].Single();
          weights1[index3] = keyValuePair.Value.Weight;
          priorities[index3] = keyValuePair.Value.Priority;
        }
        frequencySetArray[index1] = new FrequencySet(weights1, priorities);
      }
      return frequencySetArray;
    }

    public ITopology Topology => topology;

    public Func<double> RandomDouble => wavePropagator.RandomDouble;

    public TileModel TileModel => tileModel;

    public Resolution Status => wavePropagator.Status;

    public int BacktrackCount => wavePropagator.BacktrackCount;

    public double GetProgress() => wavePropagator.Wave.GetProgress();

    public Resolution Clear() => wavePropagator.Clear();

    public void SetContradiction() => wavePropagator.SetContradiction();

    public Resolution Ban(int x, int y, int z, Tile tile)
    {
      int px;
      int py;
      int pz;
      int offset;
      TileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
      foreach (int pattern in tileModelMapping.GetPatterns(tile, offset))
      {
        Resolution resolution = wavePropagator.Ban(px, py, pz, pattern);
        if (resolution != Resolution.Undecided)
          return resolution;
      }
      return Resolution.Undecided;
    }

    public Resolution Ban(int x, int y, int z, IEnumerable<Tile> tiles) => Ban(x, y, z, CreateTileSet(tiles));

    public Resolution Ban(int x, int y, int z, TilePropagatorTileSet tiles)
    {
      int px;
      int py;
      int pz;
      int offset;
      TileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
      foreach (int pattern in tileModelMapping.GetPatterns(tiles, offset))
      {
        Resolution resolution = wavePropagator.Ban(px, py, pz, pattern);
        if (resolution != Resolution.Undecided)
          return resolution;
      }
      return Resolution.Undecided;
    }

    public Resolution Select(int x, int y, int z, Tile tile)
    {
      int px;
      int py;
      int pz;
      int offset;
      TileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
      ISet<int> patterns = tileModelMapping.GetPatterns(tile, offset);
      for (int pattern = 0; pattern < wavePropagator.PatternCount; ++pattern)
      {
        if (!patterns.Contains(pattern))
        {
          Resolution resolution = wavePropagator.Ban(px, py, pz, pattern);
          if (resolution != Resolution.Undecided)
            return resolution;
        }
      }
      return Resolution.Undecided;
    }

    public Resolution Select(int x, int y, int z, IEnumerable<Tile> tiles) => Select(x, y, z, CreateTileSet(tiles));

    public Resolution Select(int x, int y, int z, TilePropagatorTileSet tiles)
    {
      int px;
      int py;
      int pz;
      int offset;
      TileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
      ISet<int> patterns = tileModelMapping.GetPatterns(tiles, offset);
      for (int pattern = 0; pattern < wavePropagator.PatternCount; ++pattern)
      {
        if (!patterns.Contains(pattern))
        {
          Resolution resolution = wavePropagator.Ban(px, py, pz, pattern);
          if (resolution != Resolution.Undecided)
            return resolution;
        }
      }
      return Resolution.Undecided;
    }

    public Resolution Step() => wavePropagator.Step();

    public void StepConstraints() => wavePropagator.StepConstraints();

    public Resolution Run() => wavePropagator.Run();

    public SelectedTracker CreateSelectedTracker(TilePropagatorTileSet tileSet)
    {
      SelectedTracker selectedTracker = new SelectedTracker(this, wavePropagator, tileModelMapping, tileSet);
      ((ITracker) selectedTracker).Reset();
      wavePropagator.AddTracker(selectedTracker);
      return selectedTracker;
    }

    public SelectedChangeTracker CreateSelectedChangeTracker(
      TilePropagatorTileSet tileSet,
      IQuadstateChanged onChange)
    {
      SelectedChangeTracker selectedChangeTracker = new SelectedChangeTracker(this, wavePropagator, tileModelMapping, tileSet, onChange);
      selectedChangeTracker.Reset();
      wavePropagator.AddTracker(selectedChangeTracker);
      return selectedChangeTracker;
    }

    public ChangeTracker CreateChangeTracker()
    {
      ChangeTracker changeTracker = new ChangeTracker(tileModelMapping);
      ((ITracker) changeTracker).Reset();
      wavePropagator.AddTracker(changeTracker);
      return changeTracker;
    }

    public TilePropagatorTileSet CreateTileSet(IEnumerable<Tile> tiles) => tileModelMapping.CreateTileSet(tiles);

    public bool IsSelected(int x, int y, int z, Tile tile)
    {
      bool isSelected;
      GetBannedSelected(x, y, z, tile, out bool _, out isSelected);
      return isSelected;
    }

    public bool IsBanned(int x, int y, int z, Tile tile)
    {
      bool isBanned;
      GetBannedSelected(x, y, z, tile, out isBanned, out bool _);
      return isBanned;
    }

    public void GetBannedSelected(
      int x,
      int y,
      int z,
      Tile tile,
      out bool isBanned,
      out bool isSelected)
    {
      int px;
      int py;
      int pz;
      int offset;
      TileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
      ISet<int> patterns;
      try
      {
        patterns = tileModelMapping.TilesToPatternsByOffset[offset][tile];
      }
      catch (KeyNotFoundException ex)
      {
        throw new KeyNotFoundException(string.Format("Couldn't find pattern for tile {0} at offset {1}", tile, offset));
      }
      GetBannedSelectedInternal(px, py, pz, patterns, out isBanned, out isSelected);
    }

    public void GetBannedSelected(
      int x,
      int y,
      int z,
      IEnumerable<Tile> tiles,
      out bool isBanned,
      out bool isSelected)
    {
      GetBannedSelected(x, y, z, CreateTileSet(tiles), out isBanned, out isSelected);
    }

    public void GetBannedSelected(
      int x,
      int y,
      int z,
      TilePropagatorTileSet tiles,
      out bool isBanned,
      out bool isSelected)
    {
      int px;
      int py;
      int pz;
      int offset;
      TileCoordToPatternCoord(x, y, z, out px, out py, out pz, out offset);
      ISet<int> patterns = tileModelMapping.GetPatterns(tiles, offset);
      GetBannedSelectedInternal(px, py, pz, patterns, out isBanned, out isSelected);
    }

    internal Quadstate GetSelectedQuadstate(
      int x,
      int y,
      int z,
      TilePropagatorTileSet tiles)
    {
      bool isBanned;
      bool isSelected;
      GetBannedSelected(x, y, z, tiles, out isBanned, out isSelected);
      return isSelected ? (isBanned ? Quadstate.Contradiction : Quadstate.Yes) : (isBanned ? Quadstate.No : Quadstate.Maybe);
    }

    private void GetBannedSelectedInternal(
      int px,
      int py,
      int pz,
      ISet<int> patterns,
      out bool isBanned,
      out bool isSelected)
    {
      int index = wavePropagator.Topology.GetIndex(px, py, pz);
      Wave wave = wavePropagator.Wave;
      int patternCount = wavePropagator.PatternCount;
      isBanned = true;
      isSelected = true;
      for (int pattern = 0; pattern < patternCount; ++pattern)
      {
        if (wave.Get(index, pattern))
        {
          if (patterns.Contains(pattern))
            isBanned = false;
          else
            isSelected = false;
        }
      }
    }

    public Tile GetTile(int index, Tile undecided = default (Tile), Tile contradiction = default (Tile))
    {
      int patternIndex;
      int offset;
      tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out offset);
      int decidedPattern = wavePropagator.GetDecidedPattern(patternIndex);
      switch (decidedPattern)
      {
        case -2:
          return contradiction;
        case -1:
          return undecided;
        default:
          return tileModelMapping.PatternsToTilesByOffset[offset][decidedPattern];
      }
    }

    public T GetValue<T>(int index, T undecided = default, T contradiction = default)
    {
      int patternIndex;
      int offset;
      tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out offset);
      int decidedPattern = wavePropagator.GetDecidedPattern(patternIndex);
      switch (decidedPattern)
      {
        case -2:
          return contradiction;
        case -1:
          return undecided;
        default:
          return (T) tileModelMapping.PatternsToTilesByOffset[offset][decidedPattern].Value;
      }
    }

    public ISet<Tile> GetPossibleTiles(int index)
    {
      int patternIndex;
      int offset;
      tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out offset);
      IEnumerable<int> possiblePatterns = wavePropagator.GetPossiblePatterns(patternIndex);
      HashSet<Tile> tileSet = new HashSet<Tile>();
      IReadOnlyDictionary<int, Tile> readOnlyDictionary = tileModelMapping.PatternsToTilesByOffset[offset];
      foreach (int key in possiblePatterns)
        tileSet.Add(readOnlyDictionary[key]);
      return tileSet;
    }

    public ISet<T> GetPossibleValues<T>(int index)
    {
      int patternIndex;
      int offset;
      tileModelMapping.GetTileCoordToPatternCoord(index, out patternIndex, out offset);
      IEnumerable<int> possiblePatterns = wavePropagator.GetPossiblePatterns(patternIndex);
      HashSet<T> objSet = new HashSet<T>();
      IReadOnlyDictionary<int, Tile> readOnlyDictionary = tileModelMapping.PatternsToTilesByOffset[offset];
      foreach (int key in possiblePatterns)
        objSet.Add((T) readOnlyDictionary[key].Value);
      return objSet;
    }

    public ITopoArray<Tile> ToArray(Tile undecided = default (Tile), Tile contradiction = default (Tile)) => TopoArray.CreateByIndex(index => GetTile(index, undecided, contradiction), topology);

    public ITopoArray<T> ToValueArray<T>(T undecided, T contradiction) => TopoArray.CreateByIndex(index => GetValue(index, undecided, contradiction), topology);

    public ITopoArray<ISet<Tile>> ToArraySets() => TopoArray.CreateByIndex(new Func<int, ISet<Tile>>(GetPossibleTiles), topology);

    public ITopoArray<ISet<T>> ToValueSets<T>() => TopoArray.CreateByIndex(new Func<int, ISet<T>>(GetPossibleValues<T>), topology);
  }
}