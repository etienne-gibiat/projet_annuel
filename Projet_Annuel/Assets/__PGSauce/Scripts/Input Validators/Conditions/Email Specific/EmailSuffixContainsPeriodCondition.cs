using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// There must be period after @
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditionsEmails + "Period after @")]
    public class EmailSuffixContainsPeriodCondition : EmailBaseCondition
    {
        [SerializeField] private StringContainsTokenCondition containsAtLeastOnePeriod;

        protected override bool CheckAfterArobase(string afterArobase)
        {
            return containsAtLeastOnePeriod.ValidateInput(afterArobase);
        }
    }
}