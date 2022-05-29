using UnityEngine;

namespace PGSauce.Core.Publishing.Deployers
{
    public abstract class DeployerBase<TPlatform> : DeployerSo where TPlatform : PublishSettings
    {
        [SerializeField] private string header;

        public string Header => header;

        public abstract string GetCiDeploySteps(TPlatform publishSettings);

    }
}