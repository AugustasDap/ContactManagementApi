using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomPersonBirthdayValidationAttribute : ValidationAttribute
    {
        private const int MinimumAge = 0;  
        private const int MaximumAge = 120; 

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime birthday)
            {
                var age = DateTime.Today.Year - birthday.Year;

                
                if (birthday > DateTime.Today.AddYears(-age)) // Adjust age if birthday hasn't occurred yet this year
                {
                    age--;
                }

                if (age < MinimumAge || age > MaximumAge)
                {
                    return new ValidationResult($"Age must be between {MinimumAge} and {MaximumAge} years.");
                }

                if (birthday > DateTime.Today)
                {
                    return new ValidationResult("Birthday must be a date in the past.");
                }
            }
            else
            {
                return new ValidationResult("Invalid date format.");
            }

            return ValidationResult.Success;
        }
    }
}
