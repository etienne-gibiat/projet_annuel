using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.Strings;
using UnityEngine;
using PGSauce.Core.Publishing;
using PGSauce.Core.Publishing.ContinousIntegration;
using PGSauce.Core.Utilities;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace PGSauce.Core
{
    /// <summary>
    /// The Game settings. Also defines the deploy and build targets.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.Settings + "PGSettings", fileName = "_____________PGSettings")]
    public class PGSettings : SOSingleton<PGSettings>
    {
#if UNITY_EDITOR
        [SerializeField, BoxGroup("Splash Screen")] private Sprite splashscreen;
        [SerializeField, BoxGroup("Splash Screen")] private PlayerSettings.SplashScreen.DrawMode drawMode;
        [SerializeField, BoxGroup("Splash Screen")] private Color splashscreenBackgroundColor = PgColors.Black;
        [SerializeField] private PublishVersion version;
        [SerializeField] private string gameName;
        [SerializeField] private string gameNameInNamespace;
        [SerializeField] private List<PublishSettings> buildTargets;
        [SerializeField] private CiYamlBuilder ciYamlBuilder;

        [ShowInInspector]
        public string CompanyName => "Big Catto";
        
        [ShowInInspector]
        public const string PgSaucePath = "__PGSauce/";
        [ShowInInspector]
        public const string GameSpecificPath = "__PG Game Specific/";
        [ShowInInspector]
        public const string AutomaticallyGeneratedPath = GameSpecificPath + "__Automaticallygenerated";
        
        public string OrganizationId => "3058504";

        public string GameNameInNamespace => gameNameInNamespace.Trim();
        public string GamesNamespace => $"PGSauce.Games.{GameNameInNamespace}";
        public string GameName => gameName;
        public bool HasProLicense => false;
        
        public Sprite Splashscreen => splashscreen;
        public Color SplashscreenBackgroundColor => splashscreenBackgroundColor;
        
        public PlayerSettings.SplashScreen.DrawMode LogoDrawMode => drawMode;

        public List<PublishSettings> BuildTargets => buildTargets;
        [ShowInInspector]
        public List<PublishSettings> UsedTargets => GetUsedTargets();
        public PublishVersion Version => version;
        public string DiawiToken => "m23h6YWiAaoP5QL0Z9eiEUNrPWgSqEO3N9FbdLVX9s";
        public string EmailsDiawi => "nicolas.ruche94@gmail.com";

        [Button(ButtonSizes.Gigantic)]
        public static void UpdateProject()
        {
#if UNITY_EDITOR
            ProjectUpdater.UpdateProject(Instance);
#endif
        }

        public void GenerateCiYaml(List<PublishSettings> usedTargets)
        {
            ciYamlBuilder.BuildYaml(usedTargets);
        }
        
        public List<PublishSettings> GetUsedTargets()
        {
            return BuildTargets.Where(target => target.UseTarget).ToList();
        }
#endif
    }
}
