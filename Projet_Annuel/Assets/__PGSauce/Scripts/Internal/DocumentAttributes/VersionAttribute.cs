using System;

namespace PGSauce.Internal.Attributes
{
    /// <summary>
    /// An attribute to mark API versions.
    /// </summary>
    [Version(0, 0, 1)]
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    public class VersionAttribute : Attribute
    {
        #region  Properties

        /// <summary>
        /// The main version number of this element : totally different API.
        /// </summary>
        public int MainVersion { get; private set; }

        /// <summary>
        /// The sub version version number of this element : code breaks (method signature is changed for example)
        /// </summary>
        public int SubVersion { get; private set; }

        /// <summary>
        /// The sub sub version of this element : minor changes, no code break.
        /// </summary>
        public int SubSubVersion { get; private set; }

        #endregion

        #region Constructors

        public VersionAttribute(int mainVersion, int subVersion  = 0, int subSubVerion = 0)
        {
            MainVersion = mainVersion;
            SubVersion = subVersion;
            SubSubVersion = subSubVerion;
        }

        #endregion
    }
}