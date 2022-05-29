using UnityEngine;

namespace Jobberwocky.GeometryAlgorithms.Examples.Data
{
    public class SquareWithHole : Square
    {
        public SquareWithHole()
        {
            Holes = new Vector3[1][];
            Holes[0] = CreateSquare(Scale * 0.5f);
        }
    }
}