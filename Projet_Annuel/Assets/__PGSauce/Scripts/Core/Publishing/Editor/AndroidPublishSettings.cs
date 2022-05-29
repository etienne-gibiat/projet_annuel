using System.Collections.Generic;
using PGSauce.Core.Strings;

#if UNITY_EDITOR
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Publishing.Data_Base;
using PGSauce.Core.Publishing.Deployers;
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.PublishSettings + "Android")]
    public class AndroidPublishSettings : MobilePublishSettings
    {
#if UNITY_EDITOR
        [SerializeField] private DeployerBase<AndroidPublishSettings> androidDeployer;
        [SerializeField, Range(0, 5)] private int updatePriority = 0;
        public override BuildTargetGroup BuildTarget => BuildTargetGroup.Android;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.Android;
        public string InAppUpdatePriority => updatePriority.ToString();

        protected override void UpdateProject(PGSettings pgSettings)
        {
            base.UpdateProject(pgSettings);
            SetTargetArchitectures();
            RenderSafeArea();
            SetAndroidBundleCode();
        }

        protected override Dictionary<string, string> GetCustomVariables()
        {
            return new Dictionary<string, string>() {{"BUNDLE_VERSION_CODE", PlayerSettings.Android.bundleVersionCode.ToString()}};
        }

        protected override void SetOnlineGameData(PGSettings pgSettings)
        {
            base.SetOnlineGameData(pgSettings);
            DataBaseHandler.SetAndroidGameData(pgSettings, this);
        }


        private void RenderSafeArea()
        {
            PlayerSettings.Android.renderOutsideSafeArea = true;
            PGDebug.Message("ANDROID Render Outside Safe Area").Log();
        }

        private void SetAndroidBundleCode()
        {
            var bundleCode = DataBaseHandler.GetAndroidLastBundleCode(this);
            bundleCode++;
            DataBaseHandler.SetAndroidBundleCode(this, bundleCode);
            PlayerSettings.Android.bundleVersionCode = bundleCode;
            PGDebug.Message("ANDROID Set Bundle Code").Log();
        }

        private void SetTargetArchitectures()
        {
            PlayerSettings.SetScriptingBackend(BuildTarget, ScriptingImplementation.IL2CPP);
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARMv7 | AndroidArchitecture.ARM64;
            PGDebug.Message("ANDROID Set target architecture").Log();
        }

        public override string GetCiDeploySteps()
        {
            return androidDeployer.GetCiDeploySteps(this);
        }
#endif
        
    }
}