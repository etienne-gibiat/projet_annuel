using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PGSauce.Core.Utilities;

namespace PGSauce.Core.Maths
{
    /// <summary>
    /// Some useful methods for math
    /// </summary>
    public static class PGMaths
    {
        /// <summary>
        /// Compares two numbers
        /// </summary>
        /// <param name="lhs">Number to the left</param>
        /// <param name="rhs">Number to the right</param>
        /// <param name="comparison">What comparator should we use ? (== ? != ? ...)</param>
        /// <returns>True if the lhs matches the rhs with the comparison</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if comparison enum value does not exist</exception>
        public static bool CompareNumbers(int lhs, int rhs, MathComparisonOperator comparison)
        {
            return comparison switch
            {
                MathComparisonOperator.None => true,
                MathComparisonOperator.Different => lhs != rhs,
                MathComparisonOperator.Equals => lhs == rhs,
                MathComparisonOperator.StrictlyLessThan => lhs < rhs,
                MathComparisonOperator.LessThan => lhs <= rhs,
                MathComparisonOperator.StrictlyMoreThan => lhs > rhs,
                MathComparisonOperator.MoreThan => lhs >= rhs,
                _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
            };
        }
        
        /// <summary>
        /// Compares two numbers
        /// </summary>
        /// <param name="lhs">Number to the left</param>
        /// <param name="rhs">Number to the right</param>
        /// <param name="comparison">What comparator should we use ? (== ? != ? ...)</param>
        /// <returns>True if the lhs matches the rhs with the comparison</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if comparison enum value does not exist</exception>
        public static bool CompareNumbers(float lhs, float rhs, MathComparisonOperator comparison)
        {
            return comparison switch
            {
                MathComparisonOperator.None => true,
                MathComparisonOperator.Different => !Mathf.Approximately(lhs, rhs),
                MathComparisonOperator.Equals => Mathf.Approximately(lhs, rhs),
                MathComparisonOperator.StrictlyLessThan => lhs < rhs,
                MathComparisonOperator.LessThan => lhs <= rhs,
                MathComparisonOperator.StrictlyMoreThan => lhs > rhs,
                MathComparisonOperator.MoreThan => lhs >= rhs,
                _ => throw new ArgumentOutOfRangeException(nameof(comparison), comparison, null)
            };
        }

        public static float Mean(float a, float b)
        {
            return (a + b) / 2f;
        }
        
        public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
            var mid = Vector3.Lerp(start, end, t);
            return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
        }
        public static Vector2 Parabola(Vector2 start, Vector2 end, float height, float t)
        {
            Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
            var mid = Vector2.Lerp(start, end, t);
            return new Vector2(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t));
        }

        public static float Sign(this float x, bool allowZero = false)
        {
            if (!allowZero) return Mathf.Sign(x);
            if (x < 0)
            {
                return -1;
            }
            return x > 0 ? 1 : 0;
        }

        public static float Abs(this float x)
        {
            return Mathf.Abs(x);
        }
    }
}
