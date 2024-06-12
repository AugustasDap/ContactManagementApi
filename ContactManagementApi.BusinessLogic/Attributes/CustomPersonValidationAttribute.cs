using ContactManagementApi.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CustomPersonValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            dynamic person = value;

            // Validate personal code format
            if (person.PersonIdentificationCode == null || person.PersonIdentificationCode.Length != 11 || !Regex.IsMatch(person.PersonIdentificationCode, @"^\d{11}$"))
            {
                return new ValidationResult("Personal code must be exactly 11 symbols long.");
            }

            // Extract parts of the personal code
            int centuryAndGender = int.Parse(person.PersonIdentificationCode.Substring(0, 1));
            int year = int.Parse(person.PersonIdentificationCode.Substring(1, 2));
            int month = int.Parse(person.PersonIdentificationCode.Substring(3, 2));
            int day = int.Parse(person.PersonIdentificationCode.Substring(5, 2));

            // Validate birth date
            try
            {
                int fullYear = GetFullYear(centuryAndGender, year);
                var birthDate = new DateTime(fullYear, month, day);

                if (birthDate != person.Birthday)
                {
                    return new ValidationResult("Birthdate does not correlate with the personal ID code.");
                }
            }
            catch
            {
                return new ValidationResult("Invalid birth date in personal code.");
            }

            // Validate gender
            if (!IsValidGender(centuryAndGender, person.Gender))
            {
                return new ValidationResult("Gender does not correlate with the personal ID code.");
            }

            return ValidationResult.Success;
        }

        private int GetFullYear(int centuryAndGender, int year)
        {
            if (centuryAndGender >= 1 && centuryAndGender <= 2)
            {
                return 1800 + year;
            }
            if (centuryAndGender >= 3 && centuryAndGender <= 4)
            {
                return 1900 + year;
            }
            if (centuryAndGender >= 5 && centuryAndGender <= 6)
            {
                return 2000 + year;
            }
            if (centuryAndGender >= 7 && centuryAndGender <= 8)
            {
                return 2100 + year;
            }
            throw new ArgumentException("Invalid century and gender indicator in personal code.");
        }

        private bool IsValidGender(int centuryAndGender, string gender)
        {
            if ((centuryAndGender % 2 == 1 && gender == "Male") || (centuryAndGender % 2 == 0 && gender == "Female"))
            {
                return true;
            }
            return false;
        }
    }
}
