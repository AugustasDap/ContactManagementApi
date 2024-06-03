using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomPersonGenderValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var gender = value as string;
            if (gender != null)
            {
                if (gender != "Male" && gender != "Female")
                {
                    return new ValidationResult("Gender must be either 'Male' or 'Female'.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
