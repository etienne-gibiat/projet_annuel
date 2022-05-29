using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Object = UnityEngine.Object;

namespace PGSauce.Core.PGEditor
{
    public abstract class CodeGeneratingOdinWindowBase : OdinEditorWindow
    {
        private string _localSelectedPath = "";
        protected TextTemplateMasterSO.TemplatesData Templates { get; private set; }

        [ShowInInspector, FoldoutGroup("Read Only Data"), PropertyOrder(-1000)]
        protected string AssetsDirPath => PGAssets.AssetsDirPath;
        [ShowInInspector, FoldoutGroup("Read Only Data"), PropertyOrder(-1000)]
        protected string ProjectDirPath => PGAssets.ProjectDirPath;
        [ShowInInspector, FoldoutGroup("Read Only Data"), PropertyOrder(-1000)]
        protected string LocalSelectedPath => _localSelectedPath;
        [ShowInInspector, FoldoutGroup("Read Only Data"), PropertyOrder(-1000)]
        protected bool IsInsidePGSauce => _localSelectedPath.Contains("__PGSauce");
        [ShowInInspector, FoldoutGroup("Read Only Data"), PropertyOrder(-1000)]
        protected string NameSpace => $"PGSauce.{GetSubNamespace()}";

        [ShowInInspector, FoldoutGroup("Inside Pg Sauce", expanded: false), PropertyOrder(-1000)]
        protected virtual string InsidePgSauceSubNameSpace { get; }

        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            UpdateSelectedPath();
            Templates = TextTemplateMasterSO.Instance.Templates;
            OnInspectorGUIBehaviour();
        }

        protected virtual void OnInspectorGUIBehaviour()
        {
            
        }

        protected static void ShowWindow<T>(string title) where T : EditorWindow
        {
            GetWindow<T>(title).Show();
        }
        
        public bool ConfirmCodeGeneration()
        {
            var sure = true;
            if (IsInsidePGSauce)
            {
                sure = EditorUtility.DisplayDialog("Inside PGSauce",
                    "Are you sure you want to generate the script inside PG Sauce", "Yes", "No");
            }

            return sure && CustomConfirmCodeGeneration();
        }

        protected virtual bool CustomConfirmCodeGeneration()
        {
            return true;
        }
        
        protected string GetSubNamespace()
        {
            return IsInsidePGSauce ? (string.IsNullOrEmpty(InsidePgSauceSubNameSpace) ? "Core" : $"Core.{InsidePgSauceSubNameSpace}") : $"Games.{PGSettings.Instance.GameNameInNamespace}";
        }

        private void UpdateSelectedPath()
        {
            var folderPath = PGAssets.GetPathUnderMouse();
            _localSelectedPath = folderPath;
        }
        
        protected static string FormatType(string variableType)
        {
            if (variableType.Length <= 0)
            {
                return "";
            }
            
            var result = variableType.Trim().Replace("<", "")
                .Replace(">", "");


            return char.ToUpper(result[0]) + result.Substring(1);
        }

        protected static void AbstractGenerateOneFile(string result, string intoPath, string fileName)
        {
            PGAssets.AbstractGenerateOneFile(result, intoPath, fileName);
        }
        
        protected static string AbstractFormatting(IReadOnlyList<string> values, PGAssets.Format formatter)
        {
            return PGAssets.AbstractFormatting(values, formatter);
        }

        protected string CreateMenuPrefixPath => IsInsidePGSauce ? "MenuPaths.MenuBase + " : "MenuPaths.GamePath + ";

    }
}
