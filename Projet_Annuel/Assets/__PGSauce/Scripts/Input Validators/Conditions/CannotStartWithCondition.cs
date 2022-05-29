using MonKey.Extensions;
using PGSauce.Core.Strings;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// The string cannot start with this token
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditions + "Cannot Start With")]
    public class CannotStartWithCondition : InputValidatorCondition
    {
        [SerializeField] private string token;
        public override bool ValidateInput(string value)
        {
            if (token.IsNullOrEmpty() || value.IsNullOrEmpty())
            {
                return true;
            }

            if (value.Length < token.Length)
            {
                return true;
            }

            if (value.Length == token.Length)
            {
                return !value.Equals(token);
            }

            return !value.Replace(value.Substring(token.Length), "").Equals(token);
        }
    }
}