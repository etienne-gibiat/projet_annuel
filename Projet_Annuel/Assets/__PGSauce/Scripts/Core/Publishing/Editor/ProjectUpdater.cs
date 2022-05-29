
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
using UnityEditor;
using UnityEngine;

#endif

namespace PGSauce.Core.Publishing
{
    public static class ProjectUpdater
    {
#if UNITY_EDITOR
        public static void UpdateProject(PGSettings pgSettings)
        {
            PGDebug.Message($"UNITY ADS").LogTodo();
            PGDebug.Message($"Reset Saves, data etc.").LogTodo();
            ResetAbTests();
            TryDisableUnityLogo(pgSettings);
            HandleSplashScreen(pgSettings);
            SetCompanyName(pgSettings);
            SetGameName(pgSettings);
            SetGameVersion(pgSettings);

            var usedTargets = pgSettings.GetUsedTargets();

            foreach (var target in usedTargets)
            {
                SetIcons(target);
                target.UpdateTargetProject(pgSettings);
            }

            GenerateCiYaml(pgSettings, usedTargets);

            StringsGenerator.Instance.GenerateStrings();
        }

        private static void ResetAbTests()
        {
            PGDebug.Message($"Set AB TEST Handler Debug to false").Log();
        }


        private static void GenerateCiYaml(PGSettings pgSettings, List<PublishSettings> usedTargets)
        {
            pgSettings.GenerateCiYaml(usedTargets);
        }

        private static void SetGameVersion(PGSettings pgSettings)
        {
            PlayerSettings.bundleVersion = pgSettings.Version.CurrentVersion;
            PGDebug.Message($"Set up Game Version to {PlayerSettings.bundleVersion}").Log();
        }

        private static void SetGameName(PGSettings pgSettings)
        {
            PlayerSettings.productName = pgSettings.GameName.Trim();
            PGDebug.Message($"Set up Game Name {PlayerSettings.productName}").Log();
        }

        private static void SetIcons(PublishSettings target)
        {
            if (!target.Icon)
            {
                throw new UnityException($"Icon is not set for target {target.name}!");
            }
            
            var icons = new[] {target.Icon};
            PlayerSettings.SetIconsForTargetGroup(target.BuildTarget, icons);
            PlayerSettings.SetIconsForTargetGroup(BuildTargetGroup.Unknown, icons);
            PGDebug.Message($"Set icons for {target.BuildTarget}").Log();
        }

        private static void SetCompanyName(PGSettings pgSettings)
        {
            PlayerSettings.companyName = pgSettings.CompanyName.Trim();
            PGDebug.Message($"Set up Company Name {PlayerSettings.companyName}").Log();
        }

        private static void HandleSplashScreen(PGSettings pgSettings)
        {
            PlayerSettings.SplashScreen.drawMode = pgSettings.LogoDrawMode;

            var splashScreen = pgSettings.Splashscreen;
            var logos = new PlayerSettings.SplashScreenLogo[1];
            logos[0] = PlayerSettings.SplashScreenLogo.Create(2f, splashScreen);
            PlayerSettings.SplashScreen.logos = logos;
            PlayerSettings.SplashScreen.backgroundColor = pgSettings.SplashscreenBackgroundColor;
            
            PGDebug.Message("Set up Splash Screen").Log();
        }

        private static void TryDisableUnityLogo(PGSettings pgSettings)
        {
            if (pgSettings.HasProLicense)
            {
                PlayerSettings.SplashScreen.showUnityLogo = false;
            }

            PGDebug.Message($"Show unity logo {PlayerSettings.SplashScreen.showUnityLogo}").Log();
        }
#endif
    }
}