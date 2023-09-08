using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    public class PasswordContainsUppercaseAndDigitAttribute : ValidationAttribute
{
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