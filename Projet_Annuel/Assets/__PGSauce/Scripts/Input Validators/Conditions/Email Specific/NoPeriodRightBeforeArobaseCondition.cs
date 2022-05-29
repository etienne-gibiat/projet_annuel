using System.Linq;
using MonKey.Extensions;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// No . before @
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditionsEmails + "No . before @")]
    public class NoPeriodRightBeforeArobaseCondition : EmailBaseCondition
    {
        protected override bool CheckLocalPart(string localPart)
        {
            if (localPart.Length == 0)
            {
                return true;
            }
            return !localPart.Last().Equals('.');
        }
    }
}