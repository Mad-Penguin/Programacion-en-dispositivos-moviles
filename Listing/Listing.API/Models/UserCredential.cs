using System.ComponentModel.DataAnnotations;

namespace Listing.API.Models
{
    public class UserCredential
    {
        [Required]
        [MinLength(4, ErrorMessage = "Username must contain at least 4 characters")]
        [MaxLength(50, ErrorMessage = "Username must contain at most 50 characters")]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
