using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;

namespace PGSauce.Core.CSharpWizard
{
    /// <summary>
    /// SO Singleton used to customize C# Wizard
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Settings + "C# Wizard Settings")]
    public class CSharpWizardSettings : SOSingleton<CSharpWizardSettings>
    {
        [SerializeField] private List<string> usings = new List<string>()
            {"UnityEngine", "System.Collections", "System.Collections.Generic"};

        [SerializeField] private bool generateUnityFunctions;

        [BoxGroup("Namespace"), SerializeField] private string defaultNotInGameNameSpace;
        /// <summary>
        /// Returns the Game namespace (usually PGSauce.Games.GAME_NAME)
        /// </summary>
        [BoxGroup("Namespace"), ShowInInspector]
        public string DefaultGameNamespace => PGSettings.Instance.GamesNamespace;
        /// <summary>
        /// Returns the inside PG Sauce namespace (PGSauce)
        /// </summary>
        [BoxGroup("Namespace")]
        public string DefaultNotInGameNameSpace => defaultNotInGameNameSpace;
        /// <summary>
        /// The list of modules we want to be imported when we generate a Runtime script
        /// </summary>
        public ReadOnlyCollection<string> Usings => usings.AsReadOnly();
        /// <summary>
        /// The list of modules we want to be imported when we generate a Editor script
        /// </summary>
        public ReadOnlyCollection<string> UsingsInspector => GetUsingsInspector();
        /// <summary>
        /// Should we generateStart, Awake etc. ?
        /// </summary>
        public bool GenerateUnityFunctions => generateUnityFunctions;


        private ReadOnlyCollection<string> GetUsingsInspector()
        {
            var u = new List<string>(Usings) {"UnityEditor"};
            return u.AsReadOnly();
        }
    }
}
