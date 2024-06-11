using ContactManagementApi.BusinessLogic.Attributes;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.DTOs
{
    [CustomPersonValidation]
    public class PersonUpdateDto
    {
        [Required]
        [CustomPersonNameValidation]
        public string Name { get; set; }

        [Required]
        [CustomPersonNameValidation]
        public string LastName { get; set; }

        [Required]
        [CustomPersonGenderValidation]
        public string Gender { get; set; }

        [Required]
        [CustomPersonBirthdayValidation]
        public DateTime Birthday { get; set; }

        [Required]
        [CustomPersonIdCodeValidation]
        public string PersonIdentificationCode { get; set; }

        [Required]
        [CustomPersonPhoneValidation]
        public string PhoneNumber { get; set; }

        [Required]
        [CustomPersonEmailValidation]
        public string Email { get; set; }

        [CustomImageFileValidation]
        public IFormFile File { get; set; }
        public PlaceOfResidenceDto PlaceOfResidence { get; set; }
    }
}
