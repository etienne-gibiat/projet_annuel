using System.IO;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.Strings
{
    [CreateAssetMenu(menuName = MenuPaths.Settings + "Strings Generator")]
    public class StringsGenerator : SOSingleton<StringsGenerator>, IStringsTemplate
    {
        [SerializeField, FolderPath] private string folderWhereToGenerate = "";

        [Button]
        public void GenerateStrings()
        {
            var code = TextTemplateMasterSO.Instance.Templates.strings.ReplaceTemplateWithData(new StringsTemplate(this));
            var assetsDirPath = Application.dataPath;
            var pathToGenerateFolder = Path.Combine(assetsDirPath, folderWhereToGenerate, "PGStrings.cs");
            File.WriteAllText(pathToGenerateFolder, code);
            PGAssets.SaveUnityAssetDatabase();
        }

        public string GAMENAME()
        {
            return PGSettings.Instance.GameName;
        }
    }
}