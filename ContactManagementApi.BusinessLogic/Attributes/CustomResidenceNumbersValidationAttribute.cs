using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomResidenceNumbersValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var number = value as string;
            if (number != null)
            {
                // Define a regular expression pattern for alphanumeric validation
                string alphanumericPattern = @"^[a-zA-Z0-9]+$";

                // Validate number against the pattern
                if (!Regex.IsMatch(number, alphanumericPattern))
                {
                    return new ValidationResult("The field must be alphanumeric and cannot contain special characters or spaces.");
                }
            }
            else
            {
                return new ValidationResult("Invalid input.");
            }

            return ValidationResult.Success;
        }
    }
}
