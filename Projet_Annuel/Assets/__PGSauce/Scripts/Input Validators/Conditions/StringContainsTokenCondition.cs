using MonKey.Extensions;
using PGSauce.Core.Extensions;
using PGSauce.Core.Maths;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// Used to check if the value contains a certain token, and how many times
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditions + "Contains Token")]
    public class StringContainsTokenCondition : InputValidatorCondition
    {
        [SerializeField] private string token;
        [SerializeField] private MathComparisonOperator comparison = MathComparisonOperator.Equals;
        [SerializeField] private int occurences = 1;
        
        public override bool ValidateInput(string value)
        {
            if (token.IsNullOrEmpty())
            {
                return true;
            }
            if (value.IsNullOrEmpty())
            {
                return false;
            }
            var count = value.CountOccurences(token);

            return PGMaths.CompareNumbers(count, occurences, comparison);
        }
    }
}