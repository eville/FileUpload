namespace FileUploadService.Helpers
{
    public class ValidationMessageHelper : IValidationMessageHelper
    {
        public string ProccessMesage(bool isAccountNameValid, bool isAccountNumberValid, int lineNumber, string line)
        {

            if (!isAccountNameValid && !isAccountNumberValid)
            {
                return $"Account name, account number - not valid for {lineNumber} line '{line}'";
            }
            else if (!isAccountNameValid)
            {
                return $"Account name - not valid for {lineNumber} line '{line}'";
            }
            else if (!isAccountNumberValid)
            {
                return $"Account number - not valid for {lineNumber} line '{line}'";
            }

            return string.Empty;
        }
    }
}
