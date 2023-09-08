using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    public class MinimumAgeAttribute : ValidationAttribute
{
    private readonly int _minimumAge;

    public MinimumAgeAttribute(int minimumAge)
    {
        _minimumAge = minimumAge;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;

           
            if (birthDate.Date > today.AddYears(-age)) age--;

            if (age < _minimumAge)
            {
                return new ValidationResult($"You must be at least {_minimumAge} years old.");
            }
        }

        return ValidationResult.Success;
    }
}
}