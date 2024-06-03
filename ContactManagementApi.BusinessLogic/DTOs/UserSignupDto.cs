using ContactManagementApi.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace ContactManagementApi.BusinessLogic.DTOs
{
    public class UserSignupDto
    {
        [Required]
        [CustomUsernameValidation]
        public string UserName { get; set; }

        [Required]
        [CustomPasswordValidation]
        public string Password { get; set; }
    }
}
