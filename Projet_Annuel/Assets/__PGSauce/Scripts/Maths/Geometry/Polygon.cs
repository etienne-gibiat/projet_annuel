using System.Collections.Generic;
using Jobberwocky.GeometryAlgorithms.Source.API;
using Jobberwocky.GeometryAlgorithms.Source.Core;
using Jobberwocky.GeometryAlgorithms.Source.Parameters;
using UnityEngine;

namespace PGSauce.Geometry
{
    public class Polygon
    {
        private List<Vector3> _worldPoints;

        public Polygon(List<Vector3> points)
        {
            _worldPoints = points;
            var parameters = new Triangulation2DParameters
            {
                Boundary = _worldPoints.ToArray(), Side = Side.Front, Delaunay = true
            };

            var triangulationAPI = new TriangulationAPI();
            GeometryData = triangulationAPI.Triangulate2DRaw(parameters);
            Triangles = new List<Triangle>();
            var vertices = GeometryData.Vertices;
            var indices = GeometryData.Indices;
            for (var i = 0; i < indices.Length; i += 3)
            {
                Triangles.Add(new Triangle(vertices[indices[i + 0]], vertices[indices[i + 1]], vertices[indices[i + 2]]));
            }
        }
        
        public List<Triangle> Triangles { get; }
        public Jobberwocky.GeometryAlgorithms.Source.Core.Geometry GeometryData { get; }

        public float Area()
        {
            var area = 0f;
            Triangles.ForEach(t => area += t.Area());
            return area;
        }
    }
}