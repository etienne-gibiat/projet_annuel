using System.Collections.Generic;
using System.Collections.ObjectModel;
using Pinwheel.CsharpWizard;

namespace PGSauce.Core.CSharpWizard
{
    /// <summary>
    /// Some utility methods to help generate scripts with C# Wizard
    /// </summary>
    public static class CSharpWizardHelper
    {
        private static string DefaultGameNameSpace => CSharpWizardSettings.Instance.DefaultGameNamespace;
        private static string DefaultNotInGameNameSpace => CSharpWizardSettings.Instance.DefaultNotInGameNameSpace;
        
        /// <summary>
        /// Returns a list of UsingNodes when we want to generate an Runtime Script
        /// </summary>
        public static ClassWizard.UsingNode GetUsingNode()
        {
            var settings = CSharpWizardSettings.Instance;
            var usings = settings.Usings;
            return GetUsingNode(usings);
        }
        
        /// <summary>
        /// Returns a list of UsingNodes when we want to generate an Editor Script
        /// </summary>
        public static ClassWizard.UsingNode GetUsingEditorNode()
        {
            var settings = CSharpWizardSettings.Instance;
            var usings = settings.UsingsInspector;
            return GetUsingNode(usings);
        }
        
        /// <summary>
        /// If the directory is inside PGSauce, returns PGSauce namespace, else returns Game namespace
        /// </summary>
        public static string GetDefaultNamespace(string directory)
        {
            return PGAssets.IsInsidePgSauce(directory) ? DefaultNotInGameNameSpace : DefaultGameNameSpace;
        }
        
        private static ClassWizard.UsingNode GetUsingNode(ReadOnlyCollection<string> usings)
        {
            var un = new ClassWizard.UsingNode();
            foreach (var usingString in usings)
            {
                un.Using(usingString);
            }

            return un;
        }

        /// <summary>
        /// Returns everything we want inside a MonoBehaviour script : Regions, Awake, Start etc.
        /// </summary>
        public static List<string> GetCustomMonoBehaviourCode()
        {
            var code = new List<string>();
            code.Add(GetCompleteRegion("Public And Serialized Fields"));
            code.Add(GetCompleteRegion("Private Fields"));
            code.Add(GetCompleteRegion("Properties"));
            if (CSharpWizardSettings.Instance.GenerateUnityFunctions)
            {
                code.Add(GetRegion("Unity Functions"));
                code.Add(ClassWizard.UnityMessage.AWAKE);
                code.Add(ClassWizard.UnityMessage.START);
                code.Add(ClassWizard.UnityMessage.UPDATE);
                code.Add(ClassWizard.UnityMessage.ON_ENABLE);
                code.Add(ClassWizard.UnityMessage.ON_DISABLE);
                code.Add(ClassWizard.UnityMessage.ON_DESTROY);
                code.Add(EndRegion);
            }
            else
            {
                code.Add(GetCompleteRegion("Unity Functions"));
            }
            
            code.Add(GetCompleteRegion("Public Methods"));
            code.Add(GetCompleteRegion("Private Methods"));

            return code;
        }

        private static string GetCompleteRegion(string regionName)
        {
            return $"{GetRegion(regionName)}{EndRegion}";
        }

        private static string GetRegion(string regionName)
        {
            return $"#region {regionName}\n";
        }
        private static string EndRegion => "#endregion\n\n";
    }
}