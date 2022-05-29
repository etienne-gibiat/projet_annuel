using UnityEngine;
using Random = System.Random;

namespace PGSauce.PgRandom
{
    public class ConcretePgRandom : IRandom
    {
        #region Private Fields
        
        private readonly Random _random;

        #endregion

        #region Constructors

        public ConcretePgRandom()
        {
        	_random = new Random();
        }

        public ConcretePgRandom(int seed)
        {
        	_random = new Random(seed);
        }

        #endregion

        #region Public Methods

        public double NextDouble()
        {
        	return _random.NextDouble();
        }

        public int Next()
        {
        	return _random.Next();
        }

        public int Next(int maxValue)
        {
        	return _random.Next(maxValue);
        }

        public int Next(int minValue, int maxValue)
        {
        	return _random.Next(minValue, maxValue);
        }

        public override string ToString()
        {
        	return _random.ToString();
        }

        public void NextBytes(byte[] bytes)
        {
        	_random.NextBytes(bytes);
        }

        /// <summary>
        /// Returns a point randomly selected the on a sphere.
        /// </summary>
        /// <param name="radius">The radius of the sphere.</param>
        /// <returns></returns>
        // http://mathworld.wolfram.com/SpherePointPicking.html
        public Vector3 RandomOnSphere(float radius)
        {
        	var u = (float) _random.NextDouble();
            var v = (float) _random.NextDouble();

            var theta = 2*Mathf.PI*u;
            var phi = Mathf.Acos(2*v - 1);

            var x = radius*Mathf.Cos(theta)*Mathf.Sin(phi);
            var y = radius*Mathf.Sin(theta)*Mathf.Sin(phi);
            var z = radius*Mathf.Cos(phi);

        	return new Vector3(x, y, z);
        }

        #endregion
    }
}