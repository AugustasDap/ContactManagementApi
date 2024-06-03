using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomResidenceStreetValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var street = value as string;
            if (street != null)
            {
                // Define a regular expression pattern for valid street names
                // - Allows letters, numbers, spaces, hyphens, and dots
                // - Ensures the dot can only be at the end
                string streetPattern = @"^[a-zA-Z0-9\s\-]+\.?$";

                // Check if the street name contains at least one space
                if (!street.Contains(" "))
                {
                    return new ValidationResult("Street name must contain at least one space.");
                }

                // Validate street name against the pattern
                if (!Regex.IsMatch(street, streetPattern))
                {
                    return new ValidationResult("Street name must only contain letters, numbers, spaces, hyphens, and can end with a dot. It cannot contain other special characters.");
                }
            }
            else
            {
                return new ValidationResult("Invalid street name.");
            }

            return ValidationResult.Success;
        }
    }
}
