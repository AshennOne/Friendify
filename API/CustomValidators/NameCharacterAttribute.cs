using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    public class NameCharacterAttribute : ValidationAttribute
{
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