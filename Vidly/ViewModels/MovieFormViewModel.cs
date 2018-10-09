using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        
        public IEnumerable<MovieGenre> Genres { get; set; }

        [Required]
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte? GenreId { get; set; }


        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Display(Name = "Date Added")]
        public DateTime? AddedDate { get; set; }


        [Required]
        [Range(1, 20)]
        [Display(Name = "Number in Stock")]
        public int? NumberInStock { get; set; }

        public string Title => Id != 0 ? "Edit Movie" : "New Movie";

        public MovieFormViewModel()
        {
            Id = 0;
        }

        public MovieFormViewModel(Movies movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            AddedDate = movie.AddedDate;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}