using System.Collections.Generic;
using UnityEngine;

namespace Jobberwocky.GeometryAlgorithms.Examples.Data
{
    public class Square : Shape
    {
        protected const float Scale = 1;

        public Square()
        {
            Points = CreateSquare(Scale);

            CameraPoint = new Vector3(0, 0, -10);
            CameraRotation = new Quaternion(0, 0, 0, 1);
        }

        protected Vector3[] CreateSquare(float scale)
        {
            Vector3[] points = new Vector3[4] {
                new Vector3(0.5f * scale, 0.5f * scale),
                new Vector3(0.5f * scale, -0.5f * scale),
                new Vector3(-0.5f * scale, -0.5f * scale),
                new Vector3(-0.5f * scale, 0.5f * scale),
            };

            return points;
        }
    }
}