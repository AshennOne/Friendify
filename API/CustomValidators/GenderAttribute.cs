using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CustomValidators
{
    using System;
using System.ComponentModel.DataAnnotations;
    /// <summary>
    /// This class defines a custom validation attribute for validating gender values.
    /// </summary>
    public class GenderAttribute : ValidationAttribute
{
        /// <summary>
        /// Validates the specified gender value.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult.Success if the gender value is valid (either "female" or "male"). Otherwise, returns a ValidationResult with the specified error message or a default message.</returns>
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