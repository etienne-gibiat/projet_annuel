

using PGSauce.Internal.Attributes;
using UnityEngine;

namespace PGSauce.PgRandom
{
    /// <summary>
    /// Represents a random generator.
    /// </summary>
    [Version(0,0,1)]
    public interface IRandom
    {
        #region Public Methods		
        /// <summary>
        /// Gets the next the random double value.
        /// </summary>
        double NextDouble();

        /// <summary>
        /// Gets the next the random integer value.
        /// </summary>
        int Next();

        /// <summary>
        /// Gets the next the random integer value below the given maximum.
        /// </summary>
        int Next(int maxValue);

        /// <summary>
        /// Gets the next the random integer value greater than or equal to the minimum 
        /// and below the given maximum.
        /// </summary>
        int Next(int minValue, int maxValue);

        /// <summary>
        /// Fills the given array with random bytes.
        /// </summary>
        void NextBytes(byte[] bytes);


        /// <summary>
        /// Returns a random value on the surface of a sphere with a given radius.
        /// </summary>
        /// <param name="radius">The radius.</param>
        /// <returns>Vector3.</returns>
        Vector3 RandomOnSphere(float radius);

        #endregion
    }
}