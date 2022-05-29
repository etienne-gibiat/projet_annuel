using System;

namespace PGSauce.Core.Utilities
{
    /// <summary>
    /// Called By Odin
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field)]
    [JetBrains.Annotations.MeansImplicitUse]
    public class CalledByOdinAttribute : Attribute
    {
        
    }
}