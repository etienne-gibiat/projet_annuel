using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using _Elementis.Scripts.Procedural_Trees.Volumes;
using PGSauce.Core;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Elementis.Scripts.Procedural_Trees
{
    public class TreeGenerator : MonoBehaviour
    {
        private const string ParamsGroup = "Parameters";

        [FoldoutGroup(ParamsGroup), SerializeField]
        private float scale = 1f;
        [FoldoutGroup(ParamsGroup), SerializeField, Range(0, 3000)]
        private int nbAttractors = 400;
        [FoldoutGroup(ParamsGroup), SerializeField]
        private TreeVolume treeVolume;
        [FoldoutGroup(ParamsGroup), SerializeField, Range(0f, 0.5f)]
        private float branchLength = 0.2f;
        [FoldoutGroup(ParamsGroup), SerializeField, Range(0f, 1f)]
        private float timeBetweenIterations = 0.5f;
        [FoldoutGroup(ParamsGroup), SerializeField, Range(0f, 3f)]
        private float attractionRange = 0.1f;
        [FoldoutGroup(ParamsGroup), SerializeField, Range(0f, 2f)]
        private float killRange = 0.5f;
        [FoldoutGroup(ParamsGroup), SerializeField, Range(0f, 0.2f)]
        private float randomGrowth = 0.1f;

        [FoldoutGroup(ParamsGroup), SerializeField]
        private MinMax<int> leavesPerGermBranch;
        [SerializeField] private TreeLeavesManager treeLeavesManager;
        [SerializeField] private TreeMesh treeMesh;
        [SerializeField] private MinMax<float> germBranchSize;

        /// <summary>
        /// Local positions of the attractors
        /// </summary>
        private List<Vector3> _attractors;
        private List<int> _activeAttractors;
        private Branch _firstBranch;
        private List<Branch> _branches;
        private List<Branch> _extremities;
        private float _timeElapsedSinceLastIteration;
        private float _killRangeSqr;
        private float _attractionRangeSqr;
        private int _currentThird;

        private float _highestAttractorYDone;
        private float _distancePerThird;

        private float CurrentThirdMaxAttractorY => GetDistanceMaxY(_currentThird);

        [ShowInInspector] public int ExtremitiesCount => _extremities?.Count ?? 0;

        public int NbAttractors => nbAttractors;
        public float IterationTimeRatio => _timeElapsedSinceLastIteration / timeBetweenIterations;

        [ShowInInspector] public int BranchCount => _branches?.Count ?? 0;

        public float Scale => scale;

        private float BranchLength => branchLength * Scale;

        public float GermBranchRadius => treeLeavesManager.GermBranchRadius;

        public bool IsCurrentThirdFinished => CheckIfCurrentThirdIsFinished();

        public Vector3 PointOfInterest => transform.position.WithY(GetDistanceMaxY(Mathf.Max(_currentThird - 1, 0)));
        public Vector3 WorldCenter => treeVolume.GetWorldCenter(this);

        public MinMax<float> GermBranchSizeRange => germBranchSize;
        public IReadOnlyList<Branch> Branches => _branches;

        public MinMax<int> LeavesPerGermBranch => leavesPerGermBranch;

        private void Awake()
        {
            _highestAttractorYDone = transform.position.y;
            _currentThird = 0;
            _attractors = new List<Vector3>();
            _activeAttractors = new List<int>();
            _branches = new List<Branch>();
            _extremities = new List<Branch>();
            _timeElapsedSinceLastIteration = 0f;
            _killRangeSqr = killRange * killRange * Scale * Scale;
            _attractionRangeSqr = attractionRange * attractionRange * Scale * Scale;
        }

        private void Start()
        {
            _attractors = GenerateAttractors();

            var minY = transform.position.y;
            var maxY = _attractors.Aggregate(transform.position, (current, attr) =>
            {
                if (current.y > attr.y)
                {
                    return current;
                }

                return attr;
            }).y + 0.01f;

            _distancePerThird = (maxY - minY) / 3f;

            var position = transform.position;
            _firstBranch = new Branch(position, position + Vector3.up * BranchLength, Vector3.up);
            _branches.Add(_firstBranch);
            _extremities.Add(_firstBranch);
        }
        
        private bool CheckIfCurrentThirdIsFinished()
        {
            if (HasReachedMaxAttractorY)
            {
                var withGerms = _branches.Where(b => b.HasGerm).ToList();

                if (withGerms.Count == 0)
                {
                    return true;
                }

                var fullyGrown = withGerms.All(b => b.Germ.FullyGrown);

                if (fullyGrown)
                {
                    return true;
                }
            }

            return false;
        }

        private bool HasReachedMaxAttractorY
        {
            get { return _branches.Any(at => at.End.y >= CurrentThirdMaxAttractorY); }
        }

        private void Update()
        {
            if (IsCurrentThirdFinished)
            {
                return;
                /*
                var withGerms = _branches.Where(b => b.HasGerm).ToList();

                if (withGerms.Count == 0)
                {
                    return;
                }

                var fullyGrown = withGerms.All(b => b.Germ.FullyGrown);

                if (fullyGrown)
                {
                    return;
                }*/
            }
            
            _timeElapsedSinceLastIteration += Time.deltaTime;
            if (_timeElapsedSinceLastIteration > timeBetweenIterations)
            {
                _timeElapsedSinceLastIteration = 0f;
                
                if (nbAttractors > 0 && !HasReachedMaxAttractorY)
                {
                    DoIteration();
                }

                foreach (var branch in _branches.Where(b => b.HasGerm && b.Germ.BranchIsGrown))
                {
                    branch.Germ.LeavesAreGrown = true;
                }

                foreach (var branch in _branches.Where(b => b.HasGerm && !b.Germ.BranchIsGrown))
                {
                    branch.Germ.BeginGrowLeaves();
                }

                CreateLeavesGerms();
            }
            ToMesh();
            treeLeavesManager.UpdateLeaves(this);
        }

        


        private void OnDrawGizmos()
        {
            if (treeVolume)
            {
                treeVolume.DrawGizmos(this);
            }

            if (_activeAttractors != null)
            {
                for (var i = 0; i < _attractors.Count; i++)
                {
                    var attractor = GetWorldSpaceAttractor(i);
                    Gizmos.color = _activeAttractors.Contains(i) ? PgColors.Yellowish : PgColors.Redish;
                    Gizmos.DrawSphere(attractor, 0.05f * Scale);
                }

                Gizmos.color = PgColors.Blueish;
                for (int i = 1; i <= 3; i++)
                {
                    var y = GetDistanceMaxY(i);
                    var pos = transform.position.WithY(y);
                    Gizmos.DrawCube(pos, new Vector3(1,0,1));
                }
                
                Gizmos.color = PgColors.Redish;
                Gizmos.DrawSphere(PointOfInterest, 0.1f * Scale);
            }

            if (_branches != null)
            {
                foreach (var b in _branches) {
                    Gizmos.color = PgColors.Greenish;
                    Gizmos.DrawLine(b.Start, b.End);
                    var isExtremity = IsExtremity(b);
                    var isFinished = IsBranchFinished(b);

                    if (isFinished)
                    {
                        Gizmos.color = b.HasGerm ? Color.green : Color.red;
                    }
                    else
                    {
                        Gizmos.color = isExtremity ? PgColors.Purple : PgColors.Greenish;
                    }
                    
                    
                    Gizmos.DrawSphere(b.End, 0.05f * Scale);
                    Gizmos.DrawSphere(b.Start, 0.05f * Scale);
                    //Handles.Label((b.Start + b.End) / 2f, $"{b.DistanceFromRoot}");

                    if (b.HasGerm)
                    {
                        Gizmos.color = Color.blue;
                        Gizmos.DrawLine(b.Germ.Start, b.Germ.End);
                        
                        Gizmos.color = Color.blue;
                        foreach (var leaf in b.Germ.Leaves)
                        {
                            Gizmos.DrawSphere(leaf.transform.position, 0.025f * Scale);
                        }
                    }
                }
            }
        }

        private bool IsExtremity(Branch b)
        {
            return _extremities.Contains(b);
        }

        private bool IsBranchFinished(Branch b)
        {
            return _attractors.All(att =>(GetWorldSpaceAttractor(att)- b.End).sqrMagnitude > _attractionRangeSqr);
        }

        public Vector3 GetWorldSpaceAttractor(int i)
        {
            return GetWorldSpaceAttractor(_attractors[i]);
        }

        public Vector3 GetWorldSpaceAttractor(Vector3 localSpace)
        {
            return transform.GetWorldPosition(localSpace);
        }


        private void ToMesh()
        {
            treeMesh.ToMesh(this);
        }

        private void DoIteration()
        {
            GrowBranches();
            RemoveAttractorsInKillRange();
            UpdateAttractors();
            if (_activeAttractors.Count > 0)
            {
                CreateNewBranches();
            }
            else
            {
                for (int i = 0; i < _extremities.Count; i++)
                {
                    var extremity = _extremities[i];
                    var start = extremity.End;
                    var direction = extremity.GetGrowDirection(randomGrowth);
                    var end = start + direction * BranchLength;
                    var newBranch = new Branch(start, end, direction, extremity);
                    _branches.Add(newBranch);
                    _extremities[i] = extremity;
                }
            }
        }

        private void CreateLeavesGerms()
        {
            foreach (var branch in _branches.Where(branch => IsBranchFinished(branch) && !branch.IsGermStateDecided))
            {
                branch.IsGermStateDecided = true;
                
                if (Random.Range(0f, 1f) <= treeVolume.ProbabilityToHaveALeafGerm(this, branch.End) || IsExtremity(branch))
                {
                    branch.GrowGerm(this);
                }
            }
        }

        private void CreateNewBranches()
        {
            _extremities.Clear();
            var newBranches = new List<Branch>();
            foreach (var branch in _branches)
            {
                if (branch.HasAttractors)
                {
                    var dir = branch.GetGrowDirection(randomGrowth);
                    var newBranch = new Branch(branch.End, branch.End + dir * BranchLength, dir, branch);
                    newBranches.Add(newBranch);
                    _extremities.Add(newBranch);
                }
                else
                {
                    if (branch.IsLeaf)
                    {
                        _extremities.Add(branch);
                    }
                }
            }

            _branches.AddRange(newBranches);
        }

        private void UpdateAttractors()
        {
            if (_attractors.Count > 0)
            {
                _activeAttractors.Clear();
                foreach (var branch in _branches)
                {
                    branch.ClearAttractors();
                }

                for (var index = 0; index < _attractors.Count; index++)
                {
                    var attractor = GetWorldSpaceAttractor(index);
                    var closest = FindClosestBranchToAttractor(attractor);

                    if (closest != null)
                    {
                        closest.AddAttractor(attractor);
                        _activeAttractors.Add(index);
                    }
                }
            }
        }

        private Branch FindClosestBranchToAttractor(Vector3 attractor)
        {
            var min = float.MaxValue;
            Branch closest = null;
            foreach (var branch in _branches)
            {
                var distance = (branch.End - attractor).sqrMagnitude;
                if (distance < _attractionRangeSqr && distance < min)
                {
                    min = distance;
                    closest = branch;
                }
            }

            return closest;
        }

        private void GrowBranches()
        {
            foreach (var branch in _extremities)
            {
                branch.Grown = true;
            }
        }

        private void RemoveAttractorsInKillRange()
        {
            for (int i = _attractors.Count - 1; i >= 0; i--)
            {
                if (_branches.Select(branch => (branch.End - GetWorldSpaceAttractor(i)).sqrMagnitude)
                    .Any(distance => distance < _killRangeSqr))
                {
                    RemoveAttractorAtIndex(i);
                }
            }
        }
        
        private void RemoveAttractorAtIndex(int i)
        {
            var pos = _attractors[i];
            if (pos.y > _highestAttractorYDone)
            {
                _highestAttractorYDone = pos.y;
            }
            _attractors.RemoveAt(i);
            nbAttractors = NbAttractors - 1;
        }
        
        private float GetDistanceMaxY(int currentThird)
        {
            return transform.position.y + currentThird * _distancePerThird;
        }

        private List<Vector3> GenerateAttractors()
        {
            return treeVolume.GenerateRandomLocalSpaceAttractors(this);
        }

        [Button]
        public void GrowNextThird()
        {
            _currentThird++;
            if (_currentThird > 3)
            {
                _currentThird = 3;
            }
        }

        public SpriteRenderer GetNewLeaf()
        {
            var prefab = treeLeavesManager.LeavesPrefabs[treeLeavesManager.LeavesPrefabs.GetRandomIndex()];
            return Instantiate(prefab, treeLeavesManager.transform);
        }
    }
}