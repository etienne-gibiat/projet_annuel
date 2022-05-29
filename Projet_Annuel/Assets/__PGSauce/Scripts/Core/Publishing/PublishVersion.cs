using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    [CreateAssetMenu(menuName = MenuPaths.MenuBase + "Publish/Version")]
    [InlineEditor()]
    public class PublishVersion : ScriptableObject
    {
        [BoxGroup("Version")] public int major;
        [BoxGroup("Version")] public int minor;
        [BoxGroup("Version")] public int hotfix = 1;
        
        [ShowInInspector] public string CurrentVersion => ToString();
        
        public override string ToString()
        {
            return major + "." + minor + "." + hotfix;
        }
    }
}