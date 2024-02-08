using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{/// <summary>
/// This class defines a custom validation attribute for validating whether a password contains at least one uppercase letter and at least one digit.
/// </summary>
    public class PasswordContainsUppercaseAndDigitAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified password to ensure that it contains at least one uppercase letter and at least one digit.
        /// </summary>
        /// <param name="value"></param>
        /// <returns>True if the password contains at least one uppercase letter and at least one digit. Otherwise, returns false.</returns>
        public override bool IsValid(object value)
        {
            if (value == null || !(value is string password))
            {
                return false;
            }

            return password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }
    }
}