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
    internal class CustomPersonIdCodeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var personIdCode = value as string;
            if (personIdCode != null)
            {
                // Validate length
                if (personIdCode.Length != 11)
                {
                    return new ValidationResult("Personal code must be 11 digits long.");
                }

                // Validate format (only digits)
                if (!Regex.IsMatch(personIdCode, @"^\d{11}$"))
                {
                    return new ValidationResult("Personal code must contain only digits.");
                }

                // Extract parts of the personal code
                int centuryAndGender = int.Parse(personIdCode.Substring(0, 1));
                int year = int.Parse(personIdCode.Substring(1, 2));
                int month = int.Parse(personIdCode.Substring(3, 2));
                int day = int.Parse(personIdCode.Substring(5, 2));
                int serialNumber = int.Parse(personIdCode.Substring(7, 3));
                int checkDigit = int.Parse(personIdCode.Substring(10, 1));

                // Validate birth date
                try
                {
                    int fullYear = GetFullYear(centuryAndGender, year);
                    var birthDate = new DateTime(fullYear, month, day);
                }
                catch
                {
                    return new ValidationResult("Invalid birth date in personal code.");
                }

                // Validate check digit
                if (checkDigit != CalculateCheckDigit(personIdCode))
                {
                    return new ValidationResult("Invalid check digit in personal code.");
                }
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

        private int CalculateCheckDigit(string personIdCode)
        {
            int[] weights1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 1, 2 };
            int[] weights2 = { 3, 4, 5, 6, 7, 8, 9, 1, 2, 3, 4 };

            int sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += (personIdCode[i] - '0') * weights1[i];
            }

            int remainder = sum % 11;
            if (remainder == 10)
            {
                sum = 0;
                for (int i = 0; i < 10; i++)
                {
                    sum += (personIdCode[i] - '0') * weights2[i];
                }
                remainder = sum % 11;
                if (remainder == 10)
                {
                    remainder = 0;
                }
            }

            return remainder;
        }
    }
}
