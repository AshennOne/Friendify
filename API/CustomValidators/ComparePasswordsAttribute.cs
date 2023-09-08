using System.ComponentModel.DataAnnotations;

namespace API.CustomValidators
{
    public class ComparePasswordsAttribute : ValidationAttribute
    {
        private readonly string _otherProperty;

        public ComparePasswordsAttribute(string otherProperty)
        {
            _otherProperty = otherProperty;
        }

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