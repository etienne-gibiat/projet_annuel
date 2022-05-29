using System;

namespace PGSauce.Core.Utilities
{
    /// <summary>
    /// To Be used with PGMaths.CompareNumbers(...)"/>
    /// </summary>
    public enum MathComparisonOperator
    {
        None = 0,
        Different = 1,
        Equals = 2,
        StrictlyLessThan = 3,
        LessThan = 4,
        StrictlyMoreThan = 5,
        MoreThan = 6
    }
}