using System.Text;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.Core.Publishing.Deployers
{
    [CreateAssetMenu (menuName = MenuPaths.PublishDeployers + "Android APK")]
    public class AndroidApkDeployer : DeployerBase<AndroidPublishSettings>
    {
        public override string GetCiDeploySteps(AndroidPublishSettings publishSettings)
        {
            var sb = new StringBuilder();
            
            sb.AppendLine($"{Header}:");
            sb.AppendLine($"\tstage: deploy");
            
            sb.AppendLine("\tscript:");
            sb.AppendLine($"\t\t- cd $UNITY_DIR/Builds/Android");
            sb.AppendLine($"\t\t- curl https://upload.diawi.com/ -F token='{PGSettings.Instance.DiawiToken}' -F file=@\"$BUILD_NAME.apk\" -F callback_emails=\"{PGSettings.Instance.EmailsDiawi}\"");
            
            sb.AppendLine($"\tneeds: [\"{publishSettings.CiBuildHeader}\"]");
            
            return sb.ToString();
        }
    }
}