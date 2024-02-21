using FileUploadService.DataValidation.ConcreteValidators;
using Xunit;

namespace FileUploadService.Tests.ValidatorTests
{
    public class AccountNameValidatorTests
    {

        [Fact]
        public void IsValid_NullInput_ReturnsFalse()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_EmptyString_ReturnsFalse()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid(string.Empty);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_AllLettersFirstNotUppercase_ReturnsFalse()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid("johnDoe");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_AllLettersWithFirstUppercase_ReturnsTrue()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid("JohnDoe");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ContainsNonLetterCharacters_ReturnsFalse()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid("JohnDoe123");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_SingleUppercaseLetter_ReturnsTrue()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid("J");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_SingleLowercaseLetter_ReturnsFalse()
        {
            // Arrange
            var accountNameValidator = new AccountNameValidator();

            // Act
            var result = accountNameValidator.IsValid("j");

            // Assert
            Assert.False(result);
        }
    }
}