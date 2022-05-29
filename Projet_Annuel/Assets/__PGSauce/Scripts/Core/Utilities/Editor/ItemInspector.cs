
using GameTroopers.UI;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;

#if UNITY_EDITOR
using PGSauce.Core.CSharpWizard;
using UnityEditor;
using Sirenix.OdinInspector.Editor;
#endif

namespace PGSauce.Core
{
    /// <summary>
    /// Used to locate some important objects. Big Catto/Select/{Object}
    /// </summary>
    public class ItemInspector
    {
#if UNITY_EDITOR
        [MenuItem(MenuPaths.SelectObject + "Select PG Settings")]
        public static void SelectPgSettings()
        {
            OdinEditorWindow.InspectObject(PGSettings.Instance).Show();
        }

        [MenuItem(MenuPaths.SelectObject + "Select PG Debug Settings")]
        public static void SelectPgDebugSettings()
        {
            OdinEditorWindow.InspectObject(PGDebugSettings.Instance).Show();
        }
        
        [MenuItem(MenuPaths.SelectObject + "Select Strings Generator")]
        public static void SelectStringsGenerator()
        {
            OdinEditorWindow.InspectObject(StringsGenerator.Instance).Show();
        }
        
        [MenuItem(MenuPaths.SelectObject + "Select Text Template Master")]
        public static void SelectTextTemplateMaster()
        {
            OdinEditorWindow.InspectObject(TextTemplateMasterSO.Instance).Show();
        }
        
        [MenuItem(MenuPaths.SelectObject + "Select C# Wizard Settings")]
        public static void SelectCSharpWizardSettings()
        {
            OdinEditorWindow.InspectObject(CSharpWizardSettings.Instance).Show();
        }
        
        [MenuItem(MenuPaths.SelectObject + "UI Settings")]
        public static void SelectUISettings()
        {
            OdinEditorWindow.InspectObject(UISettings.Instance).Show();
        }
#endif
    }
}