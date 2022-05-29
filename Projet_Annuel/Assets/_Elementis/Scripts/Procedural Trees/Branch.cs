using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees
{
    public class Branch
    {
        public Vector3 Start { get; set; }
        public Vector3 End { get; set; }
        private Vector3 Direction { get; set; }
        private Branch Parent { get; set; }
        private List<Branch> Children { get; set; }
        private List<Vector3> Attractors { get; set; }
        public bool Grown { get; set; }
        public bool HasAttractors => Attractors.Count > 0;

        private int AttractorsNumber => Attractors.Count;
        private int DistanceFromRoot { get; set; }
        public bool IsLeaf => Children.Count == 0;

        public Branch(Vector3 start, Vector3 end, Vector3 direction, Branch parent = null)
        {
            Start = start;
            End = end;
            Direction = direction;
            Parent = parent;
            DistanceFromRoot = 0;
            Children = new List<Branch>();
            Attractors = new List<Vector3>();

            if (Parent != null)
            {
                DistanceFromRoot = Parent.DistanceFromRoot + 1;
                Parent.AddChild(this);
            }
        }

        private void AddChild(Branch branch)
        {
            Children.Add(branch);
        }

        public void ClearAttractors()
        {
            Attractors.Clear();
        }

        public void AddAttractor(Vector3 attractor)
        {
            Attractors.Add(attractor);
        }

        
        public Vector3 GetGrowDirection(float randomGrowth)
        {
            if (!HasAttractors)
            {
                return (Direction + RandomGrowthVector(randomGrowth)).normalized;
            }
            
            var dir = Attractors.Aggregate(Vector3.zero, (current, attr) => current + (attr - End).normalized);
            dir /= AttractorsNumber;
            // random growth
            dir += RandomGrowthVector(randomGrowth);
            return dir.normalized;
        }

        private Vector3 RandomGrowthVector(float randomGrowth)
        {
            var alpha = Random.Range(0f, Mathf.PI);
            var theta = Random.Range(0f, Mathf.PI * 2f);

            var pt = new Vector3(
                Mathf.Cos(theta) * Mathf.Sin(alpha),
                Mathf.Sin(theta) * Mathf.Sin(alpha),
                Mathf.Cos(alpha)
            );

            return pt * randomGrowth;
        }
    }
}