using System;
using System.Collections.Generic;
using System.Linq;
using PGSauce.Core.Strings;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace PGSauce.UI.InputValidator
{
    /// <summary>
    /// Used to check if the value of an input field is valid against some conditions
    /// </summary>
    [CreateAssetMenu(menuName = MenuPaths.InputValidators + "Validator")]
    public class InputValidator : ScriptableObject
    {
        [SerializeField] private List<InputValidatorCondition> conditions;
        
#if UNITY_EDITOR
        public string testString;
        [ShowInInspector] private bool IsTestStringOk => CheckString(testString).Count == 0;
#endif

        /// <summary>
        /// The conditions of the validator
        /// </summary>
        public IReadOnlyList<InputValidatorCondition> Conditions => conditions;

        /// <summary>
        /// Check the input value and returns the list of conditions it does not validate
        /// </summary>
        /// <param name="inputField">The input field where we are typing</param>
        /// <param name="value">The value we want to put in it</param>
        /// <returns>All the conditions it does not satisfy. If the list is empty, the value is good</returns>
        public List<InputValidatorCondition> Validate(TMP_InputField inputField, string value)
        {
            return CheckString(value);
        }

        private List<InputValidatorCondition> CheckString(string value)
        {
            return Conditions.Where(condition => !condition.ValidateInput(value)).ToList();
        }
    }
}