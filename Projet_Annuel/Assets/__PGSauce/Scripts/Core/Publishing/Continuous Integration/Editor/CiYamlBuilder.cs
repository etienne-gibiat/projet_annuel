using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using PGSauce.Core.FSM.WithSo;
using PGSauce.Core.PGDebugging;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.Publishing.ContinousIntegration
{
    [Serializable]
    public struct CiYamlBuilder : ICiYamlTemplate
    {
        [SerializeField] private TextTemplateSO ciYamlTextTemplate;
        [SerializeField, ValidateInput("ForwardSlashes", "Slashes must be / not \\ ")] private string subFolder;
        private string _buildTargets;
        private string _deploys;
        private const string YamlSpace = "    ";

        [Button]
        public void BuildYaml(List<PublishSettings> targets)
        {
            PGDebug.Message($"Generate CI YAML").Log();
            var path = GetPath();
            _buildTargets = GenerateBuildTargets(targets);
            _deploys = GenerateDeploys(targets);
            PGDebug.Message($"Path to generate yaml is {path}").Log();
            var result = ciYamlTextTemplate.ReplaceTemplateWithData( new CiYamlTemplate(this));
            PGAssets.AbstractGenerateOneFile(result, path, ".gitlab-ci.yml");
        }

        private string GetPath()
        {
            var subfolder = SUBFOLDER();
            var projectPath = PGAssets.ProjectDirPath;
            if (projectPath.Contains("\\"))
            {
                projectPath = projectPath.Replace("\\", "/");
            }
            projectPath = projectPath.Replace(subfolder, "");
            return projectPath;
        }

        public string BUILDNAME()
        {
            return PGSettings.Instance.GameNameInNamespace;
        }

        public string UNITYVERSION()
        {
            return Application.unityVersion;
        }

        public string SUBFOLDER_FORMATTED_BASH()
        {
            var subfolder = SUBFOLDER();
            if (subfolder[0].Equals('/'))
            {
                subfolder = subfolder.Substring(1);
            }

            if (!subfolder[subfolder.Length - 1].Equals('/'))
            {
                subfolder += '/';
            }

            return subfolder;
        }

        public string SUBFOLDER()
        {
            return subFolder;
        }

        public string OTHERVARIABLES()
        {
            return "";
        }

        public string BUILDTARGETS()
        {
            return _buildTargets;
        }

        public string DEPLOYS()
        {
            return _deploys;
        }
        
        private string GenerateDeploys(List<PublishSettings> targets)
        {
            var sb = new StringBuilder();

            foreach (var target in targets)
            {
                sb.Append(target.GetCiDeploySteps());
                sb.AppendLine();
                sb.AppendLine();
            }
            
            var res = sb.ToString();
            res = res.Replace("\t", YamlSpace);
            PGDebug.Message($"Build Targets are {res}").Log();
            return res;
        }

        private string GenerateBuildTargets(List<PublishSettings> targets)
        {
            var sb = new StringBuilder();

            foreach (var target in targets)
            {
                AppendHeader(sb, target);

                AppendVariables(target, sb);

                AppendCustomParts(target, sb);

                sb.AppendLine("");
                sb.AppendLine("");
            }

            var res = sb.ToString();
            res = res.Replace("\t", YamlSpace);
            PGDebug.Message($"Build Targets are {res}").Log();
            return res;
        }

        private static void AppendCustomParts(PublishSettings target, StringBuilder sb)
        {
            var customParts = target.CiParts;

            foreach (var part in customParts)
            {
                sb.AppendLine($"\t{part.header}:");
                foreach (var value in part.values)
                {
                    sb.AppendLine($"\t\t- {value}");
                }
            }
        }

        private static void AppendHeader(StringBuilder sb, PublishSettings target)
        {
            sb.AppendLine($"{target.CiBuildHeader}:");
            sb.AppendLine("\t<<: *build");
            if (target.HasCiImage)
            {
                sb.AppendLine($"\timage: $IMAGE:$UNITY_VERSION-{target.CiImage}-$IMAGE_VERSION");
            }
        }

        private static void AppendVariables(PublishSettings target, StringBuilder sb)
        {
            var variables = target.GetVariables();
            sb.AppendLine("\tvariables:");
            foreach (var variable in variables)
            {
                sb.AppendLine($"\t\t{variable}");
            }

            if (target.HasBuildOptions)
            {
                sb.AppendLine($"\t\t{target.CiBuildOptions()}");
            }
        }
        
        [UsedImplicitly]
        private bool ForwardSlashes(string folder)
        {
            return !folder.Contains("\\");
        }
    }
}