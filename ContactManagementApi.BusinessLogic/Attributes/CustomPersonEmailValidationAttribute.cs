using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomPersonEmailValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            if (email != null)
            {
                // Define a regular expression for a standard email format
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(email, emailPattern))
                {
                    return new ValidationResult("Invalid email format.");
                }
            }

            return ValidationResult.Success;
        }
    }

}
