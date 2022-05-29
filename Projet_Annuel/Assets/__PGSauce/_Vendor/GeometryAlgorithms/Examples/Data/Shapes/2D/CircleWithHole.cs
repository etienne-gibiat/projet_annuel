using UnityEngine;

namespace Jobberwocky.GeometryAlgorithms.Examples.Data
{
    public class CircleWithHole : Circle
    {
        public CircleWithHole()
        {
            Holes = new Vector3[1][];
            Holes[0] = CreateCircle(Scale * 0.5f, 72);
        }
    }
}