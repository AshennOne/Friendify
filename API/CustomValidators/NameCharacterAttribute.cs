using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    /// <summary>
    /// This class defines a custom validation attribute for validating the characters in a name.
    /// </summary>
    public class NameCharacterAttribute : ValidationAttribute
{
        /// <summary>
        /// Validates the specified name to ensure that it contains only alphanumeric characters (letters and numbers).
        /// </summary>
        /// <param name="value"></param>
        /// <returns> true if the name matches the pattern. Otherwise, returns false.</returns>
        public override bool IsValid(object value)
    {
        if (value == null || !(value is string name))
        {
            return false;
        }

        return System.Text.RegularExpressions.Regex.IsMatch(name, "^[a-zA-Z0-9]*$");
    }
}
}