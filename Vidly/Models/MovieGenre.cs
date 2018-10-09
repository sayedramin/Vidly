using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class MovieGenre
    {
        [Required]
        public byte Id { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}