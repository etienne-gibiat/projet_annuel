using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PGSauce.Geometry
{
    public struct Line
    {
        public Line(Vector3 a, Vector3 b)
        {
            this.A = a;
            this.B = b;
        }

        public Vector3 A { get; private set; }

        public Vector3 B { get; private set; }
    }
}
