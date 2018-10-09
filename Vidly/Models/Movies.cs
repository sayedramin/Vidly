using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movies
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

   
        public MovieGenre Genre { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Date Added")]
        public DateTime AddedDate { get; set; }

        [Required]
        [Range(1,20)]
        [Display(Name = "Number in Stock")]
        public int NumberInStock { get; set; }
        
}
}