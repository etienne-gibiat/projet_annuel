using System;
using System.Collections.Generic;
using PGSauce.Core.Strings;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.PublishSettings + "Mac OS")]
    public class MacOsPublishSettings : PublishSettings
    {
#if UNITY_EDITOR
        public override BuildTargetGroup BuildTarget => BuildTargetGroup.Standalone;
        public override RuntimePlatform RuntimePlatform => RuntimePlatform.OSXPlayer;
#endif
    }
}