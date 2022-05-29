using PGSauce.Core.Strings;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.PublishSettings + "WEB GL")]
    public class WebGLPublishSettings : PublishSettings
    {
#if UNITY_EDITOR
        public override BuildTargetGroup BuildTarget => BuildTargetGroup.WebGL;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.WebGLPlayer;
#endif
    }
}