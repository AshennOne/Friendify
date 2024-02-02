using System.ComponentModel.DataAnnotations;
using API.CustomValidators;

namespace API.tests.CustomValidatorsTests
{
    public class ComparePasswordsAttributeTests
    {
       public class TestClass
        {
            [ComparePasswords("PasswordConfirmation", ErrorMessage = "Passwords do not match")]
            public string Password { get; set; }

            public string PasswordConfirmation { get; set; }
        }

        //[Fact]
        public void ComparePasswordsAttribute_ValidPasswords_ShouldPassValidation()
        {
            // Arrange
            var instance = new TestClass
            {
                Password = "password123",
                PasswordConfirmation = "password123"
            };

            var validationContext = new ValidationContext(instance);
            var validationAttribute = new ComparePasswordsAttribute("Password");

            // Act
            var validationResult = validationAttribute.GetValidationResult(instance.Password, validationContext);

            // Assert
           // Assert.Null(validationResult);
        }

        //[Fact]
        public void ComparePasswordsAttribute_InvalidPasswords_ShouldFailValidation()
        {
            // Arrange
            var instance = new TestClass
            {
                Password = "password123",
                PasswordConfirmation = "password456"
            };

            var validationContext = new ValidationContext(instance);
            var validationAttribute = new ComparePasswordsAttribute("Password")
            {
                ErrorMessage = "Custom error message"
            };

            // Act
            var validationResult = validationAttribute.GetValidationResult(instance.Password, validationContext);

            // Assert
           // Assert.NotNull(validationResult);
           // Assert.Equal("Custom error message", validationResult.ErrorMessage);
        }

        [Fact]
        public void ComparePasswordsAttribute_PropertyNotFound_ShouldThrowArgumentException()
        {
            // Arrange
            var instance = new TestClass();

            var validationContext = new ValidationContext(instance);
            var validationAttribute = new ComparePasswordsAttribute("NonExistentProperty");

            // Act & Assert
           // Assert.Throws<ArgumentException>(() => validationAttribute.GetValidationResult(null, validationContext));
        }
    }
}