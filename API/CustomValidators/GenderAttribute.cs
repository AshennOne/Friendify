using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CustomValidators
{
    using System;
using System.ComponentModel.DataAnnotations;

public class GenderAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null || !(value is string gender))
        {
            return ValidationResult.Success; // Return success if the value is null or not a string.
        }

        string normalizedGender = gender.ToLower(); // Convert to lowercase for case-insensitive comparison.

        if (normalizedGender == "female" || normalizedGender == "male")
        {
            return ValidationResult.Success; // Valid gender value.
        }

        return new ValidationResult(ErrorMessage ?? "Invalid gender value. Please use 'female' or 'male'.");
    }
}

}