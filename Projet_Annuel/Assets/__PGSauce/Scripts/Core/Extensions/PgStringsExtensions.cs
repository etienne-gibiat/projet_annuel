using System;
using MonKey.Extensions;
using PGSauce.Core.PGDebugging;

namespace PGSauce.Core.Extensions
{
    /// <summary>
    /// Useful strings extensions
    /// </summary>
    public static class PgStringsExtensions
    {
        /// <summary>
        /// Transforms a string like "PGSauce.Core.FSM.Base.State" into "State"
        /// </summary>
        public static string RemoveNamespace(this string typeName)
        {
            // Generic types
            var index = typeName.IndexOf("`", StringComparison.Ordinal);
            if (index >= 0)
            {
                typeName = typeName.Substring(0, index);
            }
            
            index = typeName.LastIndexOf(".", StringComparison.Ordinal);
            if (index >= 0)
            {
                typeName = typeName.Substring(index + 1);
            }
            return typeName;
        }

        /// <summary>
        /// Counts how many times token is inside value
        /// </summary>
        /// <param name="value">The string to check</param>
        /// <param name="token">The token to find</param>
        /// <returns>How many time is token in value</returns>
        public static int CountOccurences(this string value, string token)
        {
            if (value.IsNullOrEmpty())
            {
                return 0;
            }
            return (value.Length - value.Replace(token, "").Length) / token.Length;
        }
    }
}