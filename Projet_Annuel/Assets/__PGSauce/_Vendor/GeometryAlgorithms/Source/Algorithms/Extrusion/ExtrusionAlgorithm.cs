using System;
using System.Collections.Generic;
using System.Linq;
using Jobberwocky.GeometryAlgorithms.GeometryAlgorithms.Source.Algorithms;
using Jobberwocky.GeometryAlgorithms.Source.Core;
using UnityEngine;

namespace Jobberwocky.GeometryAlgorithms.Extrusion
{
    public class ExtrusionAlgorithm : Algorithm<ExtrusionAlgorithm>
    {
        private ExtrusionAlgorithm() { }

        public Mesh Extrude(Mesh mesh, float height)
        {
            var extrudedVertices = new Vector3[mesh.vertices.Length];
            for (var i = 0; i < mesh.vertices.Length; i++) {
                var vertex = mesh.vertices[i];

                extrudedVertices[i] = new Vector3(vertex.x, vertex.y, vertex.z + height);
            }

            var indices = mesh.GetIndices(0);
            if (height > 0) {
                Array.Reverse(indices);
            }

            var boundaryEdges = new Dictionary<string, EdgeInt>();
            var extrudedIndices = new int[indices.Length];
            for (int i = 0; i < indices.Length; i += 3) {
                extrudedIndices[i] = mesh.vertices.Length + indices[i + 2];
                extrudedIndices[i + 1] = mesh.vertices.Length + indices[i + 1];
                extrudedIndices[i + 2] = mesh.vertices.Length + indices[i + 0];

                for (int j = 0; j < 3; j++) {
                    var edge = new EdgeInt(indices[i + j], indices[i + ((j + 1) % 3)]);
                    var edgeKey = edge.GetKey();
                    if (boundaryEdges.ContainsKey(edgeKey)) {
                        boundaryEdges.Remove(edgeKey);
                    } else {
                        boundaryEdges.Add(edgeKey, edge);
                    }
                }
            }

            var sideIndices = new int[6 * boundaryEdges.Values.Count];
            var edges = boundaryEdges.Values.ToArray();
            for (int i = 0; i < edges.Length; i++) {
                var edge = edges[i];

                sideIndices[i * 6] = edge.X + mesh.vertices.Length;
                sideIndices[i * 6 + 1] = edge.Y;
                sideIndices[i * 6 + 2] = edge.X;
                sideIndices[i * 6 + 3] = edge.Y;
                sideIndices[i * 6 + 4] = edge.X + mesh.vertices.Length;
                sideIndices[i * 6 + 5] = edge.Y + mesh.vertices.Length;
            }
            
            var mergedVertices = new List<Vector3>(mesh.vertices.Length + extrudedVertices.Length);
            mergedVertices.AddRange(mesh.vertices);
            mergedVertices.AddRange(extrudedVertices);

            var mergedIndices = new List<int>(indices.Length + extrudedIndices.Length + sideIndices.Length);
            mergedIndices.AddRange(indices);
            mergedIndices.AddRange(extrudedIndices);
            mergedIndices.AddRange(sideIndices);
            
            var extrudedMesh = new Mesh();
            extrudedMesh.SetVertices(mergedVertices);
            extrudedMesh.SetIndices(mergedIndices.ToArray(), MeshTopology.Triangles, 0);
            extrudedMesh.RecalculateBounds();
            extrudedMesh.RecalculateNormals();

            return extrudedMesh;
        }
    }
}