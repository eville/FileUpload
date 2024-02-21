namespace FileUploadService.DataValidation.ConcreteValidators
{
    public class AccountNameValidator : IAccountDetailsValidator
    {
        public bool IsValid(string data)
        {
            if (data != null && data.Length >= 1)
            {
                return data.All(Char.IsLetter) && char.IsUpper(data[0]);
            }

            return false;        
        }
    }
}
