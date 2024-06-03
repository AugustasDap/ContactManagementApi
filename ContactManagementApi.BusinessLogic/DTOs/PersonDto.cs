﻿using ContactManagementApi.BusinessLogic.Attributes;
using ContactManagementApi.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.DTOs
{
    public class PersonDto
    {
        public Guid Id { get; set; }
        
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
        public string FilePath { get; set; }
        public PlaceOfResidenceDto PlaceOfResidence { get; set; }
    }
}