using Jobberwocky.GeometryAlgorithms.Source.Core;
using UnityEngine;

namespace PGSauce.Geometry
{
    public struct Triangle
    {
        public Vertex VertexA;
        public Vertex VertexB;
        public Vertex VertexC;

        public Triangle(Vertex vertexA, Vertex vertexB, Vertex vertexC)
        {
            this.VertexA = vertexA;
            this.VertexB = vertexB;
            this.VertexC = vertexC;
        }

        public float Area()
        {
                var v = Vector3.Cross(VertexA.Position - VertexB.Position, VertexA.Position-VertexC.Position);
                return v.magnitude * 0.5f;
        }

        public Vector3 RandomPoint()
        {
            var a = Random.Range(0f, 1f);
            var b = Random.Range(0f, 1f);

            var sqrtA = Mathf.Sqrt(a);
            return (1 - sqrtA) * VertexA.Position + (sqrtA * (1 - b)) * VertexB.Position + b * sqrtA * VertexC.Position;
        }
    }
}