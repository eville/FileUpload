namespace FileUploadService.Helpers
{
    public interface IValidationMessageHelper
    {
        string ProccessMesage(bool isAccountNameValid, bool isAccountNumberValid, int lineNumber, string line);
    }
}