using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    /// <summary>
    /// This class defines a custom validation attribute for validating the characters in a username.
    /// </summary>
    public class UsernameCharacterAttribute : ValidationAttribute
{
        /// <summary>
        /// Validates the specified username to ensure that it contains only alphanumeric characters, underscores, dots, and hyphens.
        /// </summary>
        /// <param name="value"></param>
        /// <returns> True if the username contains only allowed characters. Otherwise, returns false.</returns>
        public override bool IsValid(object value)
    {
        if (value == null || !(value is string username))
        {
            return false;
        }

        return System.Text.RegularExpressions.Regex.IsMatch(username, "^[a-zA-Z0-9_.-]*$");
    }
}
}