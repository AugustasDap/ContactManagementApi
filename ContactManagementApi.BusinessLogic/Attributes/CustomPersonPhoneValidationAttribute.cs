using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomPersonPhoneValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var phoneNumber = value as string;
            if (phoneNumber != null)
            {
                // Define a regular expression for E.164 phone number format
                string e164Pattern = @"^\+?[1-9]\d{1,14}$";

                if (!Regex.IsMatch(phoneNumber, e164Pattern))
                {
                    return new ValidationResult("Invalid phone number format. The phone number must be in E.164 (+370xxxxxxxxx) format.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
