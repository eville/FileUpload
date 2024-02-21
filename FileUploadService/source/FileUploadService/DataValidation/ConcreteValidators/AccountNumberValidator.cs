using System.Text.RegularExpressions;

namespace FileUploadService.DataValidation.ConcreteValidators
{
    public class AccountNumberValidator : IAccountDetailsValidator
    {
        public bool IsValid(string data)
        {
            if(data != null)
            {
                var pattern = @"^(3|4)[0-9]{6}p?$";
                var regex = new Regex(pattern);

                return regex.IsMatch(data);
            }

            return false;
        }
    }
}
