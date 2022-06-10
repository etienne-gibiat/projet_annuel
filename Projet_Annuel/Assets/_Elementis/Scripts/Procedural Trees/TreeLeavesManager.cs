using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees
{
    public class TreeLeavesManager : MonoBehaviour
    {
        [Range(0, 20)]
        public int radialSubdivisions = 10;
        [SerializeField] private MeshFilter filter;
        [SerializeField, AssetsOnly] private List<SpriteRenderer> leavesPrefabs;

        public TreeMesh treeMesh;

        public IReadOnlyList<SpriteRenderer> LeavesPrefabs => leavesPrefabs;

        public void UpdateLeaves(TreeGenerator tree)
        {
            var branches = tree.Branches.Where(b => b.HasGerm).ToList();
            UpdateMesh(tree, branches);
        }

        private void UpdateMesh(TreeGenerator tree, List<Branch> branches)
        {
            var mesh = new Mesh();
            var size = GermBranchRadius;
            var vertices = new List<Vector3>();
            var tris = new List<int>();

            foreach (var b in branches)
            {
                var germVertices = new List<Vector3>();
                var germ = b.Germ;

                var rot = Quaternion.FromToRotation(Vector3.up, germ.GermDirection);

                for (int j = 0; j < radialSubdivisions; j++)
                {
                    var alpha = ((float) j / radialSubdivisions) * Mathf.PI * 2f;
                    var position = new Vector3(Mathf.Cos(alpha) * size, 0, Mathf.Sin(alpha) * size);
                    position = rot * position;

                    var posStart = position + germ.Start;
                    var posEnd = position;

                    if (!germ.BranchIsGrown)
                    {
                        posEnd += germ.Start + (germ.End - germ.Start) * tree.IterationTimeRatio;
                    }
                    else
                    {
                        posEnd += germ.End;
                    }

                    var treePos = tree.transform.position;
                    germVertices.Add(posStart + treePos);
                    germVertices.Add(posEnd + treePos);
                }

                var germTris = new List<int>();
                var max = radialSubdivisions * 2;

                for (int j = 0; j <= (radialSubdivisions - 1) * 2; j += 2)
                {
                    germTris.Add(j % max);
                    germTris.Add((j + 3) % max);
                    germTris.Add((j + 2) % max);

                    germTris.Add(j % max);
                    germTris.Add((j + 1) % max);
                    germTris.Add((j + 3) % max);
                }

                tris.AddRange(germTris.Select(tri => tri + vertices.Count));

                vertices.AddRange(germVertices);
            }

            mesh.vertices = vertices.ToArray();
            mesh.triangles = tris.ToArray();
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
            filter.mesh = mesh;

            var grown = branches.Where(b => b.Germ.BranchIsGrown).ToList();
            foreach (var branch in grown)
            {
                if (!branch.Germ.LeavesAreGrown)
                {
                    branch.Germ.GrowLeaves(tree.IterationTimeRatio);
                }
            }
        }

        public float GermBranchRadius => treeMesh.extremitiesSize / 2f;
    }
}