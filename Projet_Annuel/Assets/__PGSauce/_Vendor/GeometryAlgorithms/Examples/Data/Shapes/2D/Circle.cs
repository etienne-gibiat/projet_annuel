using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Jobberwocky.GeometryAlgorithms.Examples.Data
{
    public class Circle : Shape
    {
        protected const float Scale = 1;

        public Circle()
        {
            Boundary = CreateCircle(Scale, 72);
            Points = new Vector3[1] {new Vector3(0.0f, 0.0f, 0.0f)};

            CameraPoint = new Vector3(0, 0, -10);
            CameraRotation = new Quaternion(0, 0, 0, 1);
        }

        // http://answers.unity3d.com/questions/944228/creating-a-smooth-round-flat-circle.html
        /// <summary>
        /// Generate points in a circular shape
        /// </summary>
        /// <returns></returns>
        protected Vector3[] CreateCircle(float scale, int nPoints)
        {
            Vector3[] points = new Vector3[nPoints];

            float angleStep = 360.0f / nPoints;
            Quaternion quaternion = Quaternion.Euler(0.0f, 0.0f, angleStep);

            points[0] = scale * new Vector3(0.0f, 0.5f, 0.0f); // 1. First vertex on circle outline (radius = 0.5f)
            points[1] = quaternion * points[0];

            quaternion.eulerAngles *= 2;
            for (int i = 1; i < nPoints - 1; i++) {
                points[i + 1] = quaternion * points[i - 1];
            }

            return points;
        }
    }
}