using MonKey.Extensions;
using PGSauce.Core.Maths;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// Used to check the length of the string
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditions + "String Length")]
    public class StringLengthCondition : InputValidatorCondition
    {
        [SerializeField, Min(0)] private int length;
        [SerializeField] private MathComparisonOperator comparison;

        public override bool ValidateInput(string value)
        {
            if (value.IsNullOrEmpty())
            {
                value = "";
            }
            return PGMaths.CompareNumbers(value.Length, length, comparison);
        }
    }
}