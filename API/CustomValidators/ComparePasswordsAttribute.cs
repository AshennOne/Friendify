using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    /// <summary>
    /// This class defines a custom validation attribute for comparing passwords.
    /// </summary>
    public class ComparePasswordsAttribute : ValidationAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly string _otherProperty;
        /// <summary>
        /// Initializes a new instance of the  <see cref="ComparePasswordsAttribute"/> class with the specified other property to compare against.
        /// </summary>
        /// <param name="otherProperty"></param>
        public ComparePasswordsAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }
        /// <summary>
        /// Validates the specified value by comparing it against another property 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="validationContext"></param>
        /// <returns>ValidationResult.Success if the values match. Otherwise, returns a ValidationResult with the error message specified in the ErrorMessage property.</returns>
        /// <exception cref="ArgumentException"></exception>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(_otherProperty);

            if (otherPropertyInfo == null)
            {
                throw new ArgumentException("Property with this name not found");
            }

            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (!object.Equals(value, otherValue))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}