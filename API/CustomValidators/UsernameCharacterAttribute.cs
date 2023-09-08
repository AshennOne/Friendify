using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    public class UsernameCharacterAttribute : ValidationAttribute
{
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