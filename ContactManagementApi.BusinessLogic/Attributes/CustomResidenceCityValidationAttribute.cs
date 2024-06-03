using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomResidenceCityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var city = value as string;
            if (city != null)
            {
                // Define a regular expression pattern for valid city names
                string cityPattern = @"^[a-zA-Z\s]+$";

                // Validate city name against the pattern
                if (!Regex.IsMatch(city, cityPattern))
                {
                    return new ValidationResult("City name must only contain letters and spaces, and cannot contain numbers or special characters.");
                }
            }
            else
            {
                return new ValidationResult("Invalid city name.");
            }

            return ValidationResult.Success;
        }
    }
}
