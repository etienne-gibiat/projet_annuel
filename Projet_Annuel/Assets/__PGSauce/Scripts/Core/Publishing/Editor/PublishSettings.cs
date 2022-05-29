using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MonKey.Extensions;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using PGSauce.Core.PGDebugging;
using PGSauce.Core.SerializableDictionaries;
using UnityEditor;
#endif

namespace PGSauce.Core.Publishing
{
    [InlineEditor()]
    public abstract class PublishSettings : ScriptableObject
    {
#if UNITY_EDITOR
        [SerializeField] private bool useTarget;
        [SerializeField, Required] private Texture2D icon;
        [SerializeField, BoxGroup("Analytics")] private string gameAnalyticsPublicKey;
        [SerializeField, BoxGroup("Analytics")] private string gameAnalyticsPrivateKey;
        [SerializeField, BoxGroup("CI")] private BuildTarget buildTarget;
        [SerializeField, BoxGroup("CI")] private ConcreteSerializableDictionary<string, string> ciVariables;
        [SerializeField, BoxGroup("CI")] private string ciBuildHeader;
        [SerializeField, BoxGroup("CI")] private string ciImage;
        [SerializeField, BoxGroup("CI")] private List<CustomCiParts> ciParts;
        [SerializeField, BoxGroup("CI")] private List<BuildOptions> ciBuildOptions;

        public Texture2D Icon => icon;
        public abstract BuildTargetGroup BuildTarget { get; }
        public abstract RuntimePlatform RuntimePlatform { get; }

        public PublishVersion Version => PGSettings.Instance.Version;
        
        public bool UseTarget => useTarget;
        public string CiBuildHeader => ciBuildHeader;
        public bool HasCiImage => !ciImage.Trim().IsNullOrEmpty();
        public string CiImage => ciImage.Trim();

        public List<CustomCiParts> CiParts => ciParts;
        


        public string CiBuildOptions()
        {
            if (!HasBuildOptions)
            {
                return "";
            }

            var sb = new StringBuilder();

            for (var i = 0; i < ciBuildOptions.Count; i++)
            {
                sb.Append(ciBuildOptions[i]);
                
                if (i < ciBuildOptions.Count - 1)
                {
                    sb.Append(",");
                }
            }

            var options = sb.ToString().Replace(" ", "").Trim();

            return $"BuildOptions: {options}";
        }

        public bool HasBuildOptions => ciBuildOptions != null && ciBuildOptions.Count > 0;

        public void UpdateTargetProject(PGSettings pgSettings)
        {
            PGDebug.Message($"Update for target {name}").Log();
            SetGameAnalytics(pgSettings);
            SetOnlineGameData(pgSettings);
            UpdateProject(pgSettings);
        }
        
        public List<string> GetVariables()
        {
            var variables = new Dictionary<string, string>(ciVariables) {{"BUILD_TARGET", buildTarget.ToString()}};
            var variablesCustom = GetCustomVariables();
            var list = variables.Select(pair => $"{pair.Key}: {pair.Value}").ToList();
            list.AddRange(variablesCustom.Select(pair => $"{pair.Key}: {pair.Value}").ToList());
            return list;
        }

        protected virtual void SetOnlineGameData(PGSettings pgSettings) {}

        protected virtual void UpdateProject(PGSettings pgSettings) { }

        public virtual string GetCiDeploySteps()
        {
            return "";
        }
        
        protected virtual Dictionary<string, string> GetCustomVariables()
        {
            return new Dictionary<string, string>();
        }
        
        private void SetGameAnalytics(PGSettings pgSettings)
        {
        }
        
        [Serializable]
        public struct CustomCiParts
        {
            public string header;
            public List<string> values;
        }
#endif
    }
}
