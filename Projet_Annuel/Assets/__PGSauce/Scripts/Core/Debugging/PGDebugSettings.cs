using PGSauce.Core.Strings;
using UnityEngine;
using PGSauce.Core.Utilities;

namespace PGSauce.Core.PGDebugging
{
    /// <summary>
    /// The log settings
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Settings + "Debug/Settings")]
    public class PGDebugSettings : SOSingleton<PGDebugSettings>
    {
        public enum PrefixType { ClassName, MethodName, NoPrefix }
        
        /// <summary>
        /// The default color of the message when logging to info channel
        /// </summary>
        public Color defaultLogColor = PgColors.Purple;
        /// <summary>
        /// The default color of the message when logging to error channel
        /// </summary>
        public Color defaultLogErrorColor = PgColors.Redish;
        /// <summary>
        /// The default color of the message when logging to warning channel
        /// </summary>
        public Color defaultLogWarningColor = PgColors.Yellowish;
        /// <summary>
        /// What prefix should we display ? The class of the caller ? The method calling it or nothing ?
        /// </summary>
        public PrefixType defaultPrefixType = PrefixType.ClassName;
        [Min(1)] public int textSize = 14;
    }
}
