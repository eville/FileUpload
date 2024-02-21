using FileUploadService.DataValidation.ConcreteValidators;
using Xunit;

namespace FileUploadService.Tests.ValidatorTests
{
    public class AccountNumberValidatorTests
    {
        [Fact]
        public void IsValid_NullInput_ReturnsFalse()
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid(null);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_EmptyString_ReturnsFalse()
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid(string.Empty);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("3000001")]
        [InlineData("4000001")]
        public void IsValid_ValidAccountNumber_ReturnsTrue(string accountNumber)
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid(accountNumber);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_ValidAccountNumberEndingWithP_ReturnsTrue()
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid("3000001p");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void IsValid_InvalidAccountNumberLessThan6Digits_ReturnsFalse()
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid("300001");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_InvalidAccountNumberWithCharacters_ReturnsFalse()
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid("300000x");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void IsValid_InvalidAccountNumberMoreThan7Digits_ReturnsFalse()
        {
            // Arrange
            var accountNumberValidator = new AccountNumberValidator();

            // Act
            var result = accountNumberValidator.IsValid("3000000000001");

            // Assert
            Assert.False(result);
        }
    }
}
