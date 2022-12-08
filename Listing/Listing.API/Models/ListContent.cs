using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Listing.API.Models
{
    public class ListContent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public int ListId { get; set; }
        [Required]
        public string Content { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public DateTimeOffset RegistrationDate { get; set; }

    }
}
