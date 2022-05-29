
using PGSauce.Core.Strings;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.PublishSettings + "Linux 64")]
    public class Linux64PublishSettings : PublishSettings
    {
#if UNITY_EDITOR
        public override BuildTargetGroup BuildTarget => BuildTargetGroup.Standalone;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.LinuxPlayer;
#endif
    }
}