using System.Collections.Generic;
using System.Text;
using PGSauce.Core.PGDebugging;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.Publishing.Deployers
{
    [CreateAssetMenu (menuName = MenuPaths.PublishDeployers + "Android AAB")]
    public class AndroidDeployer : DeployerBase<AndroidPublishSettings>
    {
        private enum TrackType
        {
            @internal = 0,
            alpha = 1,
            beta = 2,
            production = 3
        }
        [SerializeField] private TrackType trackType;
        
        public override string GetCiDeploySteps(AndroidPublishSettings publishSettings)
        {
            var releaseStatus = trackType == TrackType.@internal ? "--release_status draft" : "";
            
            PGDebug.Message($"ANDROID Track is {trackType.ToString()}").Log();
            PGDebug.Message($"ANDROID Bundle id is {publishSettings.BundleId}").Log();
            PGDebug.Message($"ANDROID In App Priority is {publishSettings.InAppUpdatePriority} (5 = highest)").Log();
            
            var sb = new StringBuilder();
            sb.AppendLine($"{Header}:");
            sb.AppendLine($"\tstage: deploy");
            sb.AppendLine("\timage: ruby");
            sb.AppendLine("\tscript:");
            
            sb.AppendLine($"\t\t- cd $UNITY_DIR/Builds/Android");
            sb.AppendLine($"\t\t- echo $GPC_TOKEN > gpc_token.json");
            sb.AppendLine($"\t\t- cp $CI_PROJECT_DIR/Gemfile .");
            sb.AppendLine($"\t\t- gem install bundler");
            sb.AppendLine($"\t\t- bundle install");
            sb.AppendLine($"\t\t- bundle exec fastlane supply --aab $BUILD_NAME.aab --track {trackType.ToString()} --package_name {publishSettings.BundleId} --json_key ./gpc_token.json --in_app_update_priority {publishSettings.InAppUpdatePriority} {releaseStatus}");
            
            sb.AppendLine($"\tneeds: [\"{publishSettings.CiBuildHeader}\"]");
            
            return sb.ToString();
        }
    }
}