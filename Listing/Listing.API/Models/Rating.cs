using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listing.API.Models
{
    public class Rating
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public int ListId { get; set; }
        [Required]
        [Range(0.0, 5.0)]
        public float Stars { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public DateTimeOffset RegistrationDate { get; set; }
    }
}
