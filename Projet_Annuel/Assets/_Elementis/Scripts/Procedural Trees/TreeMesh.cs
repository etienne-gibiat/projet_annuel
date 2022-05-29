using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace _Elementis.Scripts.Procedural_Trees
{
    public class TreeMesh : MonoBehaviour
    {
        [SerializeField] private float invertGrowth = 2.5f;
        [Range(0, 20)]
        public int radialSubdivisions = 10;
        [SerializeField] private MeshFilter filter;
        [Range(0f, 1f), Tooltip("The size at the extremity of the branches")]
        public float extremitiesSize = 0.03f;

        public void ToMesh(TreeGenerator tree)
        {
            var treeMesh = new Mesh();
            var branches = tree.Branches;
            UpdateBranchSizes(tree, branches);

            var vertices = new Vector3[(branches.Count + 1) * radialSubdivisions];
            for (int i = 0; i < branches.Count; i++)
            {
                var branch = branches[i];
                var vertexId = radialSubdivisions * i;
                branch.VertexId = vertexId;

                var rot = Quaternion.FromToRotation(Vector3.up, branch.Direction);

                for (int j = 0; j < radialSubdivisions; j++)
                {
                    var alpha = ((float) j / radialSubdivisions) * Mathf.PI * 2f;
                    var position = new Vector3(Mathf.Cos(alpha) * branch.Size, 0, Mathf.Sin(alpha) * branch.Size);
                    position = rot * position;

                    if (branch.IsLeaf && !branch.Grown)
                    {
                        position += branch.Start + (branch.End - branch.Start) * tree.IterationTimeRatio;
                    }
                    else
                    {
                        position += branch.End;
                    }

                    vertices[vertexId + j] = position - tree.transform.position;

                    if (branch.IsRoot)
                    {
                        vertices[branches.Count * radialSubdivisions + j] = branch.Start +
                                                                            new Vector3(Mathf.Cos(alpha) * branch.Size,
                                                                                0, Mathf.Sin(alpha) * branch.Size) -
                                                                            tree.transform.position;
                    }
                }
            }
            
            var triangles = new int [branches.Count * radialSubdivisions * 6];
            for (int i = 0; i < branches.Count; i++)
            {
                var branch = branches[i];
                var fIndex = i * radialSubdivisions * 6;
                var bottomIndex = branch.IsRoot ? branches.Count * radialSubdivisions : branch.Parent.VertexId;
                var topIndex = branch.VertexId;

                for (int j = 0; j < radialSubdivisions; j++)
                {
                    triangles[fIndex + j * 6] = bottomIndex + j;
                    triangles[fIndex + j * 6 + 1] = topIndex + j;
                    if (j == radialSubdivisions - 1)
                    {
                        triangles[fIndex + j * 6 + 2] = topIndex;
                        
                        triangles[fIndex + j * 6 + 3] = bottomIndex + j;
                        triangles[fIndex + j * 6 + 4] = topIndex;
                        triangles[fIndex + j * 6 + 5] = bottomIndex;
                    }
                    else
                    {
                        triangles[fIndex + j * 6 + 2] = topIndex + j + 1;
                        
                        triangles[fIndex + j * 6 + 3] = bottomIndex + j;
                        triangles[fIndex + j * 6 + 4] = topIndex + j + 1;
                        triangles[fIndex + j * 6 + 5] = bottomIndex + j + 1;
                    }
                }

                treeMesh.vertices = vertices;
                treeMesh.triangles = triangles;
                treeMesh.RecalculateBounds();
                treeMesh.RecalculateNormals();
                filter.mesh = treeMesh;
            }
        }

        private void UpdateBranchSizes(TreeGenerator tree, IReadOnlyList<Branch> branches)
        {
            for (int i = branches.Count - 1; i >= 0; i--)
            {
                var size = 0f;
                var branch = branches[i];
                if (branch.IsLeaf)
                {
                    size = extremitiesSize;
                }
                else
                {
                    size += branch.Children.Sum(b => Mathf.Pow(b.Size, invertGrowth));
                    size = Mathf.Pow(size, 1 / invertGrowth);
                }

                branch.Size = size;
            }
        }
    }
}