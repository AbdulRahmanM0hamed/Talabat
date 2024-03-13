using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;

namespace Talabat.Dtos
{
    public class RegisterDto
    {

        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        public string Password { get; set; }

    }
}
