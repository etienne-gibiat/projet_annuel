using System;
using System.Collections.Generic;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.PublishSettings + "iOS")]
    public class IOSPublishSettings : MobilePublishSettings
    {
#if UNITY_EDITOR
        public override BuildTargetGroup BuildTarget => BuildTargetGroup.iOS;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.IPhonePlayer;
        protected override void SetOnlineGameData(PGSettings pgSettings)
        {
            base.SetOnlineGameData(pgSettings);
            PGDebug.Message("Set online game data ios").LogTodo();
        }

        protected override void UpdateProject(PGSettings pgSettings)
        {
            base.UpdateProject(pgSettings);
            SetBuildNumber();
        }
        
        private void SetBuildNumber()
        {
            var t = DateTime.UtcNow.ToString("yyyyMMddHHmm");
            PlayerSettings.iOS.buildNumber = t;
        }
#endif
    }
}