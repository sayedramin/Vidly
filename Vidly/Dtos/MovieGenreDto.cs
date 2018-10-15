using System.ComponentModel.DataAnnotations;

namespace Vidly.Dtos
{
    public class MovieGenreDto
    {
        [Required]
        public byte Id { get; set; }

        [Required]
        public string Genre { get; set; }
    }
}