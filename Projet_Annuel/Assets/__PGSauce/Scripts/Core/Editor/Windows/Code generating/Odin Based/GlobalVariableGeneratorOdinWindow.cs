
using System.IO;
using PGSauce.Core.GlobalVariables;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEditor;

namespace PGSauce.Core.PGEditor
{
    public class GlobalVariableGeneratorOdinWindow : CodeGeneratingOdinWindowBase, IGlobalVariableTemplate
    {
        [MenuItem(MenuPaths.MenuBase + "Code Generation/Global Variable")]
        private static void OpenWindow()
        {
            ShowWindow<GlobalVariableGeneratorOdinWindow>("Global Variable");
        }

        [ShowInInspector, FoldoutGroup("Inside Pg Sauce", expanded: false), PropertyOrder(-1000)]
        private const string LocalPathGv = "__PGSauce/Global Variables";

        [ShowInInspector]
        [ValidateInput("IsNotNullOrEmpty", "Can't be empty !")]
        private string _variableType = "";
        
        [ShowInInspector, FoldoutGroup("Results")]
        private string FormattedType => FormatType(_variableType);
        [ShowInInspector, FoldoutGroup("Results")]
        private string FullPath => Path.Combine(AssetsDirPath, LocalSelectedPath, $"Global{FormattedType}.cs");

        private GlobalVariableGenerationAction _globalVariableGenerationAction;

        protected override string InsidePgSauceSubNameSpace => "GlobalVariables";

        [Button("Generate Global Variable")]
        private void GenerateGV()
        {
            _globalVariableGenerationAction.Generate();
        }

        protected override void OnInspectorGUIBehaviour()
        {
            base.OnInspectorGUIBehaviour();
            _globalVariableGenerationAction = new GlobalVariableGenerationAction(this);
        }

        public void GenerateGVCode()
        {
            var result = Templates.globalVariable.ReplaceTemplateWithData(new GlobalVariableTemplate(this));

            var intoPath = FullPath;
            
            File.WriteAllText(intoPath, result);
            
            if (IsInsidePGSauce)
            {
                var gvPath = Path.Combine(AssetsDirPath, LocalPathGv, FormattedType);

                if (!Directory.Exists(gvPath))
                {
                    Directory.CreateDirectory(gvPath);
                }
            }
        }
        
        public (bool isOK, string errorMessage) VerifyCriticalData()
        {
            if (! IsNotNullOrEmpty(_variableType))
            {
                return (false, "Type must be not empty");
            }

            return CodeGenerationAction.OkData;
        }

        private bool IsNotNullOrEmpty(string val)
        {
            return !string.IsNullOrEmpty(val);
        }

        public string MENUNAME()
        {
            return $"{CreateMenuPrefixPath}\"Global Variables/Global {FORMATTEDTYPE()}\"";
        }
        
        public string SUBNAMESPACE()
        {
            return GetSubNamespace();
        }

        public string FORMATTEDTYPE()
        {
            return FormattedType;
        }

        public string TYPE()
        {
            return _variableType;
        }
    }
}
