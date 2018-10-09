using System.Collections.Generic;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MovieFormViewModel
    {
        public Movies Movie { get; set; }
        public IEnumerable<MovieGenre> Genres { get; set; }
    }
}