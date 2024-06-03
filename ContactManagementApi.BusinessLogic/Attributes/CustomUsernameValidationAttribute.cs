
using System.ComponentModel.DataAnnotations;


namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomUsernameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var username = value as string;
            if (username != null)
            {
                if (username.Length < 8 || username.Length > 20)
                {
                    return new ValidationResult("Username must be between 8 and 20 characters.");
                }

            }
            return ValidationResult.Success;
        }
    }
}
