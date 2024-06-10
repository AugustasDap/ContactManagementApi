using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    internal class CustomImageFileValidationAttribute : ValidationAttribute
    {
        private readonly string[] _permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;

            if (file == null)
            {
                return ValidationResult.Success;
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!_permittedExtensions.Contains(extension))
            {
                return new ValidationResult("Invalid file type. Only image files .jpg, .jpeg, .png, .gif are allowed.");
            }

            return ValidationResult.Success;
        }

    }
}
