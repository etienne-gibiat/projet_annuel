using System;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    internal class WavePropagator
    {
        private Wave wave;
        private IPatternModelConstraint patternModelConstraint;
        private int patternCount;
        private double[] frequencies;
        private Deque<IndexPatternItem> backtrackItems;
        private Deque<int> backtrackItemsLengths;
        private Deque<IndexPatternItem> prevChoices;
        private int droppedBacktrackItemsCount;
        private int backtrackCount;
        private int indexCount;
        private readonly bool backtrack;
        private readonly int backtrackDepth;
        private readonly IWaveConstraint[] constraints;
        private Func<double> randomDouble;
        private bool deferredConstraintsStep;
        private Resolution status;
        private ITopology topology;
        private int directionsCount;
        public readonly Func<WavePropagator, IPickHeuristic> pickHeuristicFactory;
        private List<ITracker> trackers;
        private IPickHeuristic pickHeuristic;

        public WavePropagator(PatternModel model, ITopology topology, WavePropagatorOptions options)
        {
            patternCount = model.PatternCount;
            frequencies = model.Frequencies;
            indexCount = topology.IndexCount;
            backtrack = (uint) options.BackTrackDepth > 0U;
            backtrackDepth = options.BackTrackDepth;
            constraints = options.Constraints ?? new IWaveConstraint[0];
            this.topology = topology;
            randomDouble = options.RandomDouble ?? new Random().NextDouble;
            directionsCount = topology.DirectionsCount;
            pickHeuristicFactory = options.PickHeuristicFactory;
            if (pickHeuristicFactory == null)
                pickHeuristicFactory = wavePropagator =>
                {
                    EntropyTracker entropyTracker = new EntropyTracker(wavePropagator.Wave, wavePropagator.Frequencies,
                        wavePropagator.Topology.Mask);
                    entropyTracker.Reset();
                    wavePropagator.AddTracker(entropyTracker);
                    return new RandomPickerHeuristic(entropyTracker,
                        randomDouble);
                };
            switch (options.ModelConstraintAlgorithm)
            {
                case ModelConstraintAlgorithm.Default:
                case ModelConstraintAlgorithm.Ac4:
                    patternModelConstraint = new Ac4PatternModelConstraint(this, model);
                    break;
                case ModelConstraintAlgorithm.Ac3:
                    patternModelConstraint = new Ac4PatternModelConstraint(this, model);
                    break;
                case ModelConstraintAlgorithm.OneStep:
                    patternModelConstraint =
                        new OneStepPatternModelConstraint(this, model);
                    break;
                default:
                    throw new Exception();
            }

            if (!options.Clear)
                return;
            int num = (int) Clear();
        }

        public Wave Wave => wave;

        public int IndexCount => indexCount;

        public ITopology Topology => topology;

        public Func<double> RandomDouble => randomDouble;

        public int PatternCount => patternCount;

        public double[] Frequencies => frequencies;

        public bool InternalBan(int index, int pattern)
        {
            if (backtrack)
                backtrackItems.Push(new IndexPatternItem()
                {
                    Index = index,
                    Pattern = pattern
                });
            patternModelConstraint.DoBan(index, pattern);
            bool flag = wave.RemovePossibility(index, pattern);
            foreach (ITracker tracker in trackers)
                tracker.DoBan(index, pattern);
            return flag;
        }

        public bool InternalSelect(int index, int chosenPattern)
        {
            for (int pattern = 0; pattern < patternCount; ++pattern)
            {
                if (pattern != chosenPattern && wave.Get(index, pattern) && InternalBan(index, pattern))
                    return true;
            }

            return false;
        }

        private void Observe(out int index, out int pattern)
        {
            pickHeuristic.PickObservation(out index, out pattern);
            if (index == -1 || !InternalSelect(index, pattern))
                return;
            status = Resolution.Contradiction;
        }

        public int GetDecidedPattern(int index)
        {
            int num = -2;
            for (int pattern = 0; pattern < patternCount; ++pattern)
            {
                if (wave.Get(index, pattern))
                {
                    if (num != -2)
                        return -1;
                    num = pattern;
                }
            }

            return num;
        }

        private void InitConstraints()
        {
            foreach (IWaveConstraint constraint in constraints)
            {
                constraint.Init(this);
                if (status != Resolution.Undecided)
                    break;
                patternModelConstraint.Propagate();
                if (status != Resolution.Undecided)
                    break;
            }
        }

        public void StepConstraints()
        {
            foreach (IWaveConstraint constraint in constraints)
            {
                constraint.Check(this);
                if (status != Resolution.Undecided)
                    return;
                patternModelConstraint.Propagate();
                if (status != Resolution.Undecided)
                    return;
            }

            deferredConstraintsStep = false;
        }

        public Resolution Status => status;

        public int BacktrackCount => backtrackCount;

        public Resolution Clear()
        {
            wave = new Wave(frequencies.Length, indexCount);
            if (backtrack)
            {
                backtrackItems = new Deque<IndexPatternItem>();
                backtrackItemsLengths = new Deque<int>();
                backtrackItemsLengths.Push(0);
                prevChoices = new Deque<IndexPatternItem>();
            }

            status = Resolution.Undecided;
            trackers = new List<ITracker>();
            pickHeuristic = pickHeuristicFactory(this);
            patternModelConstraint.Clear();
            if (status == Resolution.Contradiction)
                return status;
            InitConstraints();
            return status;
        }

        public void SetContradiction() => status = Resolution.Contradiction;

        public Resolution Ban(int x, int y, int z, int pattern)
        {
            int index = topology.GetIndex(x, y, z);
            if (wave.Get(index, pattern))
            {
                deferredConstraintsStep = true;
                if (InternalBan(index, pattern))
                    return status = Resolution.Contradiction;
            }

            patternModelConstraint.Propagate();
            return status;
        }

        public Resolution Step()
        {
            if (deferredConstraintsStep)
                StepConstraints();
            int index;
            if (status != Resolution.Undecided)
            {
                index = 0;
            }
            else
            {
                if (backtrack)
                {
                    backtrackItemsLengths.Push(droppedBacktrackItemsCount + backtrackItems.Count);
                    while (backtrackDepth != -1 && backtrackItemsLengths.Count > backtrackDepth)
                    {
                        int num = backtrackItemsLengths.Unshift();
                        prevChoices.Unshift();
                        backtrackItems.DropFirst(num - droppedBacktrackItemsCount);
                        droppedBacktrackItemsCount = num;
                    }
                }

                int pattern;
                Observe(out index, out pattern);
                if (backtrack)
                {
                    if (index != -1)
                        prevChoices.Push(new IndexPatternItem()
                        {
                            Index = index,
                            Pattern = pattern
                        });
                    else
                        backtrackItemsLengths.Pop();
                }
            }

            label_12:
            if (status == Resolution.Undecided)
                patternModelConstraint.Propagate();
            if (status == Resolution.Undecided)
                StepConstraints();
            if (index == -1 && status == Resolution.Undecided)
            {
                status = Resolution.Decided;
                return status;
            }

            if (!backtrack || status != Resolution.Contradiction)
                return status;
            index = 0;
            while (backtrackItemsLengths.Count != 1)
            {
                DoBacktrack();
                IndexPatternItem indexPatternItem = prevChoices.Pop();
                ++backtrackCount;
                status = Resolution.Undecided;
                if (InternalBan(indexPatternItem.Index, indexPatternItem.Pattern))
                    status = Resolution.Contradiction;
                if (status == Resolution.Undecided)
                    patternModelConstraint.Propagate();
                if (status != Resolution.Contradiction)
                    goto label_12;
            }

            return Resolution.Contradiction;
        }

        private void DoBacktrack()
        {
            int num = backtrackItemsLengths.Pop() - droppedBacktrackItemsCount;
            while (backtrackItems.Count > num)
            {
                IndexPatternItem indexPatternItem = backtrackItems.Pop();
                int index = indexPatternItem.Index;
                int pattern = indexPatternItem.Pattern;
                wave.AddPossibility(index, pattern);
                foreach (ITracker tracker in trackers)
                    tracker.UndoBan(index, pattern);
                patternModelConstraint.UndoBan(index, pattern);
            }
        }

        public void AddTracker(ITracker tracker) => trackers.Add(tracker);

        public void RemoveTracker(ITracker tracker) => trackers.Remove(tracker);

        public Resolution Run()
        {
            do
            {
                int num = (int) Step();
            } while (status == Resolution.Undecided);

            return status;
        }

        public ITopoArray<int> ToTopoArray() =>
            TopoArray.CreateByIndex<int>(GetDecidedPattern, topology);

        public ITopoArray<ISet<int>> ToTopoArraySets() => TopoArray.CreateByIndex<ISet<int>>(
            index =>
            {
                HashSet<int> intSet = new HashSet<int>();
                for (int pattern = 0; pattern < patternCount; ++pattern)
                {
                    if (wave.Get(index, pattern))
                        intSet.Add(pattern);
                }

                return intSet;
            }, topology);

        public IEnumerable<int> GetPossiblePatterns(int index)
        {
            for (int pattern = 0; pattern < patternCount; ++pattern)
            {
                if (wave.Get(index, pattern))
                    yield return pattern;
            }
        }
    }
}