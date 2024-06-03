using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomPersonNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var personName = value as string;
            if (personName != null)
            {
                // Check length between 2 and 50 characters
                if (personName.Length < 2 || personName.Length > 50)
                {
                    return new ValidationResult("Name must be between 2 and 50 characters.");
                }

                // Check for special characters or numbers
                if (!Regex.IsMatch(personName, @"^[a-zA-Z\s]+$"))
                {
                    return new ValidationResult("Name can only contain letters and spaces.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
