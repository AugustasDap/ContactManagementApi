
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;


namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomPasswordValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = value as string;
            if (password != null)
            {
                if (password.Length < 12)
                {
                    return new ValidationResult("Password must be at least 12 characters long.");
                }
                if (Regex.Matches(password, "[A-Z]").Count < 2)
                {
                    return new ValidationResult("Password must contain at least 2 uppercase letters.");
                }
                if (Regex.Matches(password, "[a-z]").Count < 2)
                {
                    return new ValidationResult("Password must contain at least 2 lowercase letters.");
                }
                if (Regex.Matches(password, "[0-9]").Count < 2)
                {
                    return new ValidationResult("Password must contain at least 2 digits.");
                }
                if (Regex.Matches(password, "[^a-zA-Z0-9]").Count < 2)
                {
                    return new ValidationResult("Password must contain at least 2 special characters.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
