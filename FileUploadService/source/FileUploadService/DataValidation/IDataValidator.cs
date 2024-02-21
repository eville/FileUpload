namespace FileUploadService.DataValidation
{
    public interface IDataValidator
    {
        bool Validate(string validation, string data);
    }
}