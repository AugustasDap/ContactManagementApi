using ContactManagementApi.BusinessLogic.Attributes;
using ContactManagementApi.Database.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.BusinessLogic.DTOs
{
    public class PlaceOfResidenceDto
    {
        [Required]
        [CustomResidenceCityValidation]
        public string City { get; set; }

        [Required]
        [CustomResidenceStreetValidation]
        public string Street { get; set; }

        [Required]
        [CustomResidenceNumbersValidation]
        public string HouseNumber { get; set; }

        [CustomResidenceNumbersValidation]
        public string ApartmentNumber { get; set; }
        

       
    }
}
