using System;
using System.Collections.Generic;
using PGSauce.Core.Utilities;
using PGSauce.Internal.Attributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// Extensions for int, float etc.
    /// </summary>
    [Version(0,0,1)]
    public static class PgNumberExtensions
    {
        /// <summary>
        /// Returns a random float between range.min and range.max (included)
        /// </summary>
        public static float RandomValue(this MinMax<float> range)
        {
            return Random.Range(range.min, range.max);
        }
        
        /// <summary>
        /// Returns a random float between range.min and range.max (included)
        /// This float is also clamped between 0 and 1
        /// </summary>
        public static float RandomValue(this MinMax<Float01> range)
        {
            return Random.Range(range.min, range.max);
        }
        
        /// <summary>
        /// Returns a random int between range.min and range.max (excluded)
        /// </summary>
        /// <param name="range">The min and max values of the int. The max value is EXCLUDED</param>
        public static int RandomValue(this MinMax<int> range)
        {
            return Random.Range(range.min, range.max);
        }
        
        /// <summary>
        /// Clamps the given value between the given minimum float and maximum float values. Returns the given value if it is within the min and max range
        /// </summary>
        public static float Clamp(this float value, float min, float max)
        {
            return Mathf.Clamp(value, min, max);
        }

        /// <summary>
        /// Linearly interpolates between a and b by t.
        /// </summary>
        public static float Lerp(this float t, float a, float b)
        {
            return Mathf.Lerp(a, b, t);
        }
        
        /// <summary>
        /// Calculates the linear parameter t that produces the interpolant value within the range [a, b].
        /// </summary>
        public static float InverseLerp(this float value, float a, float b)
        {
            return Mathf.InverseLerp(a, b, value);
        }
        
        /// <summary>
        /// Returns true is value is between a and b
        /// </summary>
        ///<param name="strict">should the inequality be strict</param>
        public static bool IsBetween(this float f, float a, float b, bool strict = false)
        {
            if (strict)
            {
                return a < f && f < b;
            }

            return a <= f && f <= b;
        }
        
        /// <summary>
        /// Returns true is value is between a and b
        /// </summary>
        ///<param name="strict">should the inequality be strict</param>
        public static bool IsBetween(this int f, float a, float b, bool strict = false)
        {
            return ((float) f).IsBetween(a, b, strict);
        }
        
        /// <summary>
        /// Remaps a value between [inputA, inputB] into [outputA, outputB]
        /// </summary>
        public static float Remap(this float value, float inputA, float inputB, float outputA, float outputB)
        {
            return (value - inputA) / (inputB - inputA) * (outputB - outputA) + outputA;
        }

        /// <summary>
        /// Remaps a value between [inputA, inputB] into [outputA, outputB]
        /// </summary>
        public static float Remap(this int value, float inputA, float inputB, float outputA, float outputB)
        {
            return Remap((float)value, inputA, inputB, outputA, outputB);
        }
        
        /// <summary>
        /// Increment the int not passed the list count
        /// </summary>
        public static int IncrementClamped<T>(this int index, List<T> list)
        {
            return index.IncrementClamped(list.Count - 1);
        }

        /// <summary>
        /// Increment the int not passed the value
        /// </summary>
        public static int IncrementClamped(this int index, int threshold)
        {
            return Mathf.Clamp(index + 1, 0, threshold);
        }
        
        /// <summary>
        /// Throws a ArgumentOutOfRange exception if the integer is negative.
        /// </summary>
        /// <param name="n">The integer to check.</param>
        /// <param name="name">The name of the variable.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ThrowIfNegative(this int n, string name)
        {
            if(n < 0) throw new ArgumentOutOfRangeException(name, n, "argument cannot be negative");
        }

        /// <summary>
        /// Throws a ArgumentOutOfRange exception if the float is negative.
        /// </summary>
        /// <param name="x">The float to check.</param>
        /// <param name="name">The name of the variable.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static void ThrowIfNegative(float x, string name)
        {
            if (x < 0) throw new ArgumentOutOfRangeException(name, x, "argument cannot be negative");
        }
    }
}