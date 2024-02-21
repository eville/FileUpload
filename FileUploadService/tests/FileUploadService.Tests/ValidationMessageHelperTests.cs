using FileUploadService.Helpers;
using Xunit;

namespace FileUploadService.Tests
{
    public class ValidationMessageHelperTests
    {
        [Fact]
        public void ProcessMessage_BothInvalid_ReturnsCombinedErrorMessage()
        {
            // Arrange
            bool isAccountNameValid = false;
            bool isAccountNumberValid = false;
            int lineNumber = 1;
            string line = "SampleAccount1";
            var helper = new ValidationMessageHelper();

            // Act
            var result = helper.ProccessMesage(isAccountNameValid, isAccountNumberValid, lineNumber, line);

            // Assert
            Assert.Equal($"Account name, account number - not valid for 1 line 'SampleAccount1'", result);
        }

        [Fact]
        public void ProcessMessage_OnlyNameInvalid_ReturnsNameErrorMessage()
        {
            // Arrange
            bool isAccountNameValid = false;
            bool isAccountNumberValid = true;
            int lineNumber = 2;
            string line = "SampleAccount2";
            var helper = new ValidationMessageHelper();


            // Act
            var result = helper.ProccessMesage(isAccountNameValid, isAccountNumberValid, lineNumber, line);

            // Assert
            Assert.Equal($"Account name - not valid for 2 line 'SampleAccount2'", result);
        }

        [Fact]
        public void ProcessMessage_OnlyNumberInvalid_ReturnsNumberErrorMessage()
        {
            // Arrange
            bool isAccountNameValid = true;
            bool isAccountNumberValid = false;
            int lineNumber = 3;
            string line = "SampleAccount3";
            var helper = new ValidationMessageHelper();

            // Act
            var result = helper.ProccessMesage(isAccountNameValid, isAccountNumberValid, lineNumber, line);

            // Assert
            Assert.Equal($"Account number - not valid for 3 line 'SampleAccount3'", result);
        }

        [Fact]
        public void ProcessMessage_BothValid_ReturnsEmptyString()
        {
            // Arrange
            bool isAccountNameValid = true;
            bool isAccountNumberValid = true;
            int lineNumber = 4;
            string line = "ValidAccount";
            var helper = new ValidationMessageHelper();

            // Act
            var result = helper.ProccessMesage(isAccountNameValid, isAccountNumberValid, lineNumber, line);

            // Assert
            Assert.Equal(string.Empty, result);
        }
    }
}
