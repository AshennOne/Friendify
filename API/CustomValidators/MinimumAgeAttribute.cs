using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    /// <summary>
    /// This class defines a custom validation attribute for ensuring that a person's age meets a minimum threshold.
    /// </summary>
    public class MinimumAgeAttribute : ValidationAttribute
    {
        /// <summary>
        /// This property specifies a minimum age that is allowed to have an account
        /// </summary>
        private readonly int _minimumAge;
        /// <summary>
        ///  Initializes a new instance of the <see cref="MinimumAgeAttribute"/> class with the specified minimum age requirement.
        /// </summary>
        /// <param name="minimumAge"></param>
        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }
        /// <summary>
        /// Validates the specified birth date to ensure that the person's age meets the minimum requirement.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult.Success if the person's age meets the minimum requirement. Otherwise, returns a ValidationResult with an error message indicating the minimum age requirement.</returns>
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