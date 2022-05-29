using PGSauce.Core.Strings;

#if UNITY_EDITOR
using PGSauce.Core.PGDebugging;
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.PublishSettings + "Windows 64")]
    public class WindowsPublishSettings : PublishSettings
    {
#if UNITY_EDITOR
        public override BuildTargetGroup BuildTarget => BuildTargetGroup.Standalone;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.WindowsPlayer;

        protected override void UpdateProject(PGSettings pgSettings)
        {
            base.UpdateProject(pgSettings);
            PGDebug.Message("Update Projects Windows").LogTodo();
        }

        protected override void SetOnlineGameData(PGSettings pgSettings)
        {
            base.SetOnlineGameData(pgSettings);
            PGDebug.Message("Set online game data windows").LogTodo();
        }
#endif
        
    }
}