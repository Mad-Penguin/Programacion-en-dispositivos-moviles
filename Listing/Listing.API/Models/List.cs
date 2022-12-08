using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listing.API.Models
{
    public class List
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Title must contain at least 4 characters")]
        [MaxLength(50, ErrorMessage = "Title must contain at most 50 characters")]
        public string Title { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public DateTimeOffset RegistrationDate { get; set; }

    }
}
