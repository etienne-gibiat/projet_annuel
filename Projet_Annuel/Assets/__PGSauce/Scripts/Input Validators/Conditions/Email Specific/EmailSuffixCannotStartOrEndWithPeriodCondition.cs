using System.Linq;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// The part after @ cannot start or end with a period
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditionsEmails + "No periods at start or end of suffix")]
    public class EmailSuffixCannotStartOrEndWithPeriodCondition : EmailBaseCondition
    {
        protected override bool CheckAfterArobase(string afterArobase)
        {
            if (afterArobase.Length == 0)
            {
                return true;
            }
            return !afterArobase[0].Equals('.') && !afterArobase.Last().Equals('.');
        }
    }
}