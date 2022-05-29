using System;
using System.Collections.Generic;
using System.Linq;
using _Elementis.Scripts.Procedural_Trees.Volumes;
using PGSauce.Core;
using PGSauce.Core.Extensions;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees
{
    public class TreeGenerator : MonoBehaviour
    {
        private const string ParamsGroup = "Parameters";
        
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
        [SerializeField] private TreeMesh treeMesh;

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

        public int NbAttractors => nbAttractors;
        public IReadOnlyList<Branch> Branches => _branches;
        public float IterationTimeRatio => _timeElapsedSinceLastIteration / timeBetweenIterations;

        [ShowInInspector] public int BranchCount => _branches != null ? _branches.Count : 0;

        private void Awake()
        {
            _attractors = new List<Vector3>();
            _activeAttractors = new List<int>();
            _branches = new List<Branch>();
            _extremities = new List<Branch>();
            _timeElapsedSinceLastIteration = 0f;
            _killRangeSqr = killRange * killRange;
            _attractionRangeSqr = attractionRange * attractionRange;
        }

        private void Start()
        {
            _attractors = GenerateAttractors();
            var position = transform.position;
            _firstBranch = new Branch(position, position + Vector3.up * branchLength, Vector3.up);
            _branches.Add(_firstBranch);
            _extremities.Add(_firstBranch);
        }

        private void Update()
        {
            if (nbAttractors <= 0)
            {
                return;
            }
            _timeElapsedSinceLastIteration += Time.deltaTime;
            if (_timeElapsedSinceLastIteration > timeBetweenIterations)
            {
                _timeElapsedSinceLastIteration = 0f;

                DoIteration();
            }

            ToMesh();
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
                    Gizmos.DrawSphere(attractor, 0.05f);
                }
            }

            if (_branches != null)
            {
                foreach (var b in _branches) {
                    Gizmos.color = PgColors.Greenish;
                    Gizmos.DrawLine(b.Start, b.End);
                    Gizmos.color = PgColors.Purple;
                    Gizmos.DrawSphere(b.End, 0.05f);
                    Gizmos.DrawSphere(b.Start, 0.05f);
                }
            }
        }

        private Vector3 GetWorldSpaceAttractor(int i)
        {
            return transform.GetWorldPosition(_attractors[i]);
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
                    var end = start + direction * branchLength;
                    var newBranch = new Branch(start, end, direction, extremity);
                    _branches.Add(newBranch);
                    _extremities[i] = extremity;
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
                    var newBranch = new Branch(branch.End, branch.End + dir * branchLength, dir, branch);
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
            _attractors.RemoveAt(i);
            nbAttractors = NbAttractors - 1;
        }

        private List<Vector3> GenerateAttractors()
        {
            return treeVolume.GenerateRandomLocalSpaceAttractors(this);
        }
    }
}