using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace PGSauce.Core.Publishing
{
    public abstract class MobilePublishSettings : PublishSettings
    {
#if UNITY_EDITOR
        [SerializeField, BoxGroup("Orientation")] private bool allowedAutorotateToPortrait;
        [SerializeField, BoxGroup("Orientation")] private bool allowedAutorotateToPortraitUpsideDown;
        [SerializeField, BoxGroup("Orientation")] private bool allowedAutorotateToLandscapeLeft;
        [SerializeField, BoxGroup("Orientation")] private bool allowedAutorotateToLandscapeRight;
        [SerializeField] private string bundleIdSuffix = "newgame";
        [ShowInInspector] public string BundleId => "com.bigcatto." + bundleIdSuffix.Trim();

        protected override void UpdateProject(PGSettings pgSettings)
        {
            base.UpdateProject(pgSettings);
            HandleOrientation();
            SetBundleId();
        }
        
        private void SetBundleId()
        {
            PlayerSettings.SetApplicationIdentifier(BuildTarget, BundleId);
        }
        
        private void HandleOrientation()
        {
            PlayerSettings.defaultInterfaceOrientation = UIOrientation.AutoRotation;
            PlayerSettings.allowedAutorotateToPortrait = allowedAutorotateToPortrait;
            PlayerSettings.allowedAutorotateToLandscapeLeft = allowedAutorotateToLandscapeLeft;
            PlayerSettings.allowedAutorotateToLandscapeRight = allowedAutorotateToLandscapeRight;
            PlayerSettings.allowedAutorotateToPortraitUpsideDown = allowedAutorotateToPortraitUpsideDown;
        }
#endif
    }
}