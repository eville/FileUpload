using FileUploadService.DataValidation;
using Xunit;

namespace FileUploadService.Tests
{
    public class DataValidatorTests
    {
        [Fact]
        public void Validate_ValidName_ReturnsTrue()
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate("name", "JohnDoe");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_InvalidName_ReturnsFalse()
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate("name", "johnDoe");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_ValidAccountNumber_ReturnsTrue()
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate("number", "3000001");

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void Validate_InvalidAccountNumber_ReturnsFalse()
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate("number", "123456");

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Validate_UnsupportedValidationType_ReturnsFalse()
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate("email", "test@example.com");

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("name")]
        [InlineData("number")]
        public void Validate_NullData_ReturnsFalse(string validationType)
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate(validationType, null);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("name")]
        [InlineData("number")]
        public void Validate_EmptyData_ReturnsFalse(string validationType)
        {
            // Assert
            var validator = new DataValidator();

            // Act
            var result = validator.Validate(validationType, string.Empty);

            // Assert
            Assert.False(result);
        }
    }
}
