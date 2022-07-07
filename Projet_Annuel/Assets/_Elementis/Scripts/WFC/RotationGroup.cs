using System;
using System.Collections;
using System.Collections.Generic;

namespace _Elementis.Scripts.WFC
{
    public class RotationGroup : IEnumerable<Rotation>, IEnumerable
    {
        private readonly int rotationalSymmetry;
        private readonly bool reflectionalSymmetry;
        private readonly int smallestAngle;
        private readonly List<Rotation> rotations;

        public RotationGroup(int rotationalSymmetry, bool reflectionalSymmetry)
        {
            this.rotationalSymmetry = rotationalSymmetry;
            this.reflectionalSymmetry = reflectionalSymmetry;
            this.smallestAngle = 360 / rotationalSymmetry;
            this.rotations = new List<Rotation>();
            for (int index = 0; index < (reflectionalSymmetry ? 2 : 1); ++index)
            {
                for (int rotateCw = 0; rotateCw < 360; rotateCw += this.smallestAngle)
                    this.rotations.Add(new Rotation(rotateCw, index > 0));
            }
        }

        public int RotationalSymmetry => this.rotationalSymmetry;

        public bool ReflectionalSymmetry => this.reflectionalSymmetry;

        public int SmallestAngle => this.smallestAngle;

        public void CheckContains(Rotation rotation)
        {
            if (rotation.RotateCw / this.smallestAngle * this.smallestAngle != rotation.RotateCw)
                throw new Exception(string.Format("Rotation angle {0} not permitted.", (object) rotation.RotateCw));
            if (rotation.ReflectX && !this.reflectionalSymmetry)
                throw new Exception("Reflections are not permitted.");
        }

        public IEnumerator<Rotation> GetEnumerator() => (IEnumerator<Rotation>) this.rotations.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.rotations.GetEnumerator();
    }
}