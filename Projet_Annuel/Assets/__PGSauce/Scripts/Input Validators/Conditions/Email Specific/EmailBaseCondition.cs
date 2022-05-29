using MonKey.Extensions;
using UnityEngine;

namespace PGSauce.UI.InputValidator.Conditions
{
    /// <summary>
    /// Splits the email into its local part and after arobase and delegates the check to its inheritors
    /// </summary>
    public abstract class EmailBaseCondition : InputValidatorCondition
    {
        [SerializeField] private StringContainsTokenCondition containsArobase;
        /// <summary>
        /// If there are no arobase or only one, if it is empty returns false. Also returns false if there is nothing after or before @.
        /// Then splits the email around @ and checks each part.
        /// </summary>
        /// <param name="value">The email string</param>
        /// <returns>True if the condition is met.</returns>
        public sealed override bool ValidateInput(string value)
        {
            if (value.IsNullOrEmpty())
            {
                return true;
            }
            
            if (!containsArobase || !containsArobase.ValidateInput(value))
            {
                return true;
            }

            var arobaseIndex = value.IndexOf('@');
            
            var localPart = "";
            var afterArobase = "";

            if (arobaseIndex <= 0)
            {
                localPart = "";
                afterArobase = value.Replace("@", "");
            }
            else if(arobaseIndex >= value.Length - 1)
            {
                localPart = value.Replace("@", "");
                afterArobase = "";
            }
            else
            {
                var split = value.Split('@');
                localPart = split[0];
                afterArobase = split[1];
            }

            if (CheckLocalPart(localPart) && CheckAfterArobase(afterArobase))
            {
                return true;
            }

            return false;
        }

        protected virtual bool CheckAfterArobase(string afterArobase)
        {
            return true;
        }

        protected virtual bool CheckLocalPart(string localPart)
        {
            return true;
        }
    }
}