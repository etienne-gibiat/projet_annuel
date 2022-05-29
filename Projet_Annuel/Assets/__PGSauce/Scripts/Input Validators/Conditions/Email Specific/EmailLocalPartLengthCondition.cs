using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// The Local part length must be between 1 and 64
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditionsEmails + "Local Part Length")]
    public class EmailLocalPartLengthCondition : EmailBaseCondition
    {
        protected override bool CheckLocalPart(string localPart)
        {
            return localPart.Length <= 64 && localPart.Length >= 1;
        }
    }
}