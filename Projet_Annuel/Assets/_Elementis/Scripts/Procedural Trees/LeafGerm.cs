using System.Collections.Generic;
using PGSauce.Core.Extensions;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees
{
    public class LeafGerm
    {
        private List<SpriteRenderer> _leaves;
        private readonly List<Vector3> _leavesScales;

        public float GermBranchSize { get; set; }

        public Vector3 GermDirection { get; set; }
        public bool BranchIsGrown { get; set; }
        
        public Vector3 End { get; private set; }

        public Vector3 Start { get; private set; }

        public IReadOnlyList<SpriteRenderer> Leaves => _leaves;
        public bool LeavesAreGrown { get; set; }
        public bool FullyGrown => BranchIsGrown && LeavesAreGrown;

        public LeafGerm(TreeGenerator tree, Branch branch)
        {
            var center = tree.WorldCenter;
            var growDir = (branch.End - branch.Start).normalized;
            var gDir1 = Vector3.Cross(growDir, Vector3.up);
            var gDir2 = -gDir1;

            var dirFromCenter = (branch.End - center).normalized;
            var dot1 = Vector3.Dot(dirFromCenter, gDir1);
            var dot2 = Vector3.Dot(dirFromCenter, gDir2);

            var noLuck = Random.Range(0f, 1f) <= 0.1f;

            if (dot1 > dot2)
            {
                GermDirection = noLuck ? gDir2 : gDir1;
            }
            else
            {
                GermDirection = noLuck ? gDir1 : gDir2;
            }
            
            GermDirection.Normalize();

            GermBranchSize = tree.GermBranchSizeRange.RandomValue() * tree.Scale;

            Start = branch.End;
            End = Start + GermDirection * GermBranchSize;
            var leafCount = tree.LeavesPerGermBranch.RandomValue();
            _leaves = new List<SpriteRenderer>();
            _leavesScales = new List<Vector3>();

            for (int i = 0; i < leafCount; i++)
            {
                var leaf = tree.GetNewLeaf();
                _leaves.Add(leaf);
                _leavesScales.Add(leaf.transform.localScale * tree.Scale);
                leaf.gameObject.SetActive(false);
                leaf.transform.localScale = Vector3.zero;
                leaf.transform.position = GetRandomLeafSpawnPoint(tree, branch);
                leaf.transform.localRotation = Quaternion.Euler(Random.Range(0f, 360f),Random.Range(0f, 360f),Random.Range(0f, 360f));
            }
        }

        private Vector3 GetRandomLeafSpawnPoint(TreeGenerator tree, Branch b)
        {
            return b.End;
            /*
            var position = new Vector3(Mathf.Cos(Random.Range(0f, 360f) * Mathf.Deg2Rad) * tree.GermBranchRadius, 0,
                Mathf.Sin(Random.Range(0f, 360f) * Mathf.Deg2Rad) * tree.GermBranchRadius);
            var posT = Random.Range(0.1f, 1f);
            var rot = Quaternion.FromToRotation(Vector3.up, GermDirection);
            position = rot * position;
            position += Start + (End - Start) * posT;
            return position + tree.transform.position;*/
        }


        public void GrowLeaves(float treeIterationTimeRatio)
        {
            for (int i = 0; i < _leaves.Count; i++)
            {
                _leaves[i].transform.localScale = Vector3.Lerp(Vector3.zero, _leavesScales[i], treeIterationTimeRatio);
            }
        }

        public void BeginGrowLeaves()
        {
            BranchIsGrown = true;
            for (int i = 0; i < _leaves.Count; i++)
            {
                _leaves[i].gameObject.SetActive(true);
            }
        }
    }
}