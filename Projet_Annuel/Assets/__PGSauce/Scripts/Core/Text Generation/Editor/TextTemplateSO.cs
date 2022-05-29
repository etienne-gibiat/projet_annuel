using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEditor;

namespace PGSauce.Core
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Text Template/Text Template SO")]
    public class TextTemplateSO : ScriptableObject
    {
        [SerializeField] private TextAsset template;
        [SerializeField] private bool isMasterTemplate;
        [SerializeField, FolderPath, HideIf("@isMasterTemplate")] private string folderWhereToGenerate = "";
        [SerializeField, ShowIf("@IsInsidePGSauce && !isMasterTemplate")] private string insidePGSauceSubNameSpace;
        [SerializeField, ReadOnly, HideIf("@isMasterTemplate")] private List<string> tags;

        [ShowInInspector, ReadOnly]
        private string _pathToGenerateFolder = "";
        
        private List<string> _sanitizedTags;
        private List<string> SanitizedTags => _sanitizedTags;

        private void OnValidate()
        {
            if (string.IsNullOrEmpty(folderWhereToGenerate) && template)
            {
                folderWhereToGenerate = PGAssets.GetAssetPath(template);
            }

            if (string.IsNullOrEmpty(insidePGSauceSubNameSpace))
            {
                insidePGSauceSubNameSpace = TextTemplateMasterSO.Instance.LastInsidePgSauceSubNameSpace;
            }
        }


        [Button, HideIf("@isMasterTemplate")]
        public void GenerateScripts()
        {
            UpdateLastSubNameSpace();
            GenerateTags();
            
            var assetsDirPath = Application.dataPath;
            _pathToGenerateFolder = Path.Combine(assetsDirPath, folderWhereToGenerate);

            if (!Directory.Exists(_pathToGenerateFolder))
            {
                throw new UnityException($"{_pathToGenerateFolder} does not exist");
            }

            var templateName = template.name.Replace(".txt", "");
            
            GenerateInterfaceCode(templateName);
            GenerateTemplateGeneratorCode(templateName);

            PGAssets.SaveUnityAssetDatabase();
        }

        private void UpdateLastSubNameSpace()
        {
#if UNITY_EDITOR
            TextTemplateMasterSO.Instance.LastInsidePgSauceSubNameSpace = insidePGSauceSubNameSpace;
            EditorUtility.SetDirty(TextTemplateMasterSO.Instance);
#endif
        }

        private void GenerateTemplateGeneratorCode(string templateName)
        {
            var textTemplateTemplateGenerator =
                new TextTemplateTemplateGenerator(GetSubNamespace(), templateName, GetTagGeneratorInit());
            var textTemplateGenerator = new TextTemplateGeneratorTemplateGenerator(textTemplateTemplateGenerator);

            var textGeneratorTemplate =
                TextTemplateMasterSO.Instance.TextGeneratorTemplate.ReplaceTemplateWithData(textTemplateGenerator);
            var intoPath = _pathToGenerateFolder;
            var filename = $"{templateName}.cs";
            PGAssets.AbstractGenerateOneFile(textGeneratorTemplate, intoPath, filename);
        }

        private string GetTagGeneratorInit()
        {
            return PGAssets.AbstractFormatting(SanitizedTags,
                ((_, index) => $"\t\t\tTagGenerators.Add(\"{tags[index]}\", templateInterface.{SanitizedTags[index]});\n"));
        }

        private void GenerateInterfaceCode(string templateName)
        {
            var textTemplateInterface = new TextTemplateInterface(GetSubNamespace(), templateName, GetMethods());
            var textTemplateGenerator = new TextTemplateGenerator(textTemplateInterface);

            var interfaceTemplate =
                TextTemplateMasterSO.Instance.InterfaceTemplate.ReplaceTemplateWithData(textTemplateGenerator);
            var intoPath = _pathToGenerateFolder;
            var filename = $"I{templateName}.cs";
            PGAssets.AbstractGenerateOneFile(interfaceTemplate, intoPath, filename);
        }

        private string GetMethods()
        {
            return PGAssets.AbstractFormatting(SanitizedTags,
                ((tag, index) => $"\t\tstring {tag}();\n"));
        }

        private bool IsInsidePGSauce => folderWhereToGenerate.Contains("__PGSauce");
        
        private string GetSubNamespace()
        {
            return IsInsidePGSauce ? (string.IsNullOrEmpty(insidePGSauceSubNameSpace) ? "Core" : $"Core.{insidePGSauceSubNameSpace}") : $"Games.{PGSettings.Instance.GameNameInNamespace}";
        }

        public string ReplaceTemplateWithData(TextTemplateGeneratorBase templateGeneratorBase)
        {
            return templateGeneratorBase.ReplaceTemplateWithData(template.text);
        }

        private void GenerateTags()
        {
            var hashTags = new HashSet<string>();
            var regex = new Regex(@"#([A-Z])\w+#");
            var text = template.text;

            var matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                var groups = match.Groups;
                var tag = groups[0].Value;
                hashTags.Add(tag);
            }

            tags = hashTags.ToList();

            _sanitizedTags = tags.Select(tag => tag.Replace("#", "")).ToList();
        }
    }
}
