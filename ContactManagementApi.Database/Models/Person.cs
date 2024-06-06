using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagementApi.Database.Models
{
    public class Person : CommonProperties
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birthday { get; set; }
        public string PersonIdentificationCode { get; set; } //string because no calculations done
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string FilePath { get; set; }
        
        public PlaceOfResidence PlaceOfResidence { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
    }
}
