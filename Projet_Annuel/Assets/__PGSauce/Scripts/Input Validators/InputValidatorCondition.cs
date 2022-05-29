using System.Collections.Generic;
using PGSauce.Core.Strings;
using PGSauce.Core.Utilities;
using PGSauce.Localization;
using Sirenix.OdinInspector;
using UnityEngine;

namespace PGSauce.UI.InputValidator
{
    /// <summary>
    /// A condition that a string must satisfy. By default this condition is none. We have to override it.
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidatorsConditions + "No condition")]
    public class InputValidatorCondition : ScriptableObject
    {
        [SerializeField, ValueDropdown("GetTerms")]
        private string descriptionTerm;

#if UNITY_EDITOR
        public string testString;
        [ShowInInspector] private bool IsTestStringOk => ValidateInput(testString);
#endif
        [ShowInInspector]
        public string ConditionDescription => PgLocalization.Instance.GetTranslation(descriptionTerm);

        /// <summary>
        /// The method called to check the string. This returns true by default. Override it to define conditions.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <returns>If the string satisfies a certain condition or not</returns>
        public virtual bool ValidateInput(string value)
        {
            return true;
        }
        
        public sealed override string ToString()
        {
            return $"{name} : {ConditionDescription}";
        }
        
        [CalledByOdin]
        private List<string> GetTerms()
        {
            return PgLocalization.Instance.GetTerms("InputValidations");
        }
    }
}