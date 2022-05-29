using UnityEngine;

namespace Jobberwocky.GeometryAlgorithms.Source.Parameters
{
    public class Voronoi2DParameters : Parameters
    {
        public Voronoi2DParameters()
        {
            Bounded = false;
        }
        /// <summary>
        /// Points used for the generation of a Voronoi diagram
        /// </summary>
        public Vector3[] Points { get; set; }

        /// <summary>
        /// Set to true to get a bounded Voronoi diagram
        /// </summary>
        public bool Bounded { get; set; }
    }
}
