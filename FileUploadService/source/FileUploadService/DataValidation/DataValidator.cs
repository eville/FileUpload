using FileUploadService.DataValidation.ConcreteValidators;

namespace FileUploadService.DataValidation
{
    public class DataValidator : IDataValidator
    {
        public bool Validate(string validation, string data)
        {
            var validators = new Dictionary<string, IAccountDetailsValidator>()
            {
               { "name", new AccountNameValidator() },
               { "number", new AccountNumberValidator() }
            };


            if (validators.TryGetValue(validation, out var validator))
            {
                return validator.IsValid(data);
            }

            return false;
        }



    }
}
