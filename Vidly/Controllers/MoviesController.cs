using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var moives = _context.Movies.Include(m=>m.Genre).ToList();

            return View("Index",moives);
        }

        // GET: Movies/Save/5
        public ActionResult Edit(int id)
        {
            var movieViwModel = new MovieFormViewModel
            {
                Movie = _context.Movies.Include(m=>m.Genre).SingleOrDefault(m => m != null && m.Id == id),
                Genres = _context.MovieGenres.ToList()
            };

            if (movieViwModel.Movie == null)
                return HttpNotFound();

            return View("MovieForm", movieViwModel);

        }


        public ActionResult New()
        {
            var movieFormViewModel = new MovieFormViewModel
            {
                Movie = new Movies(),
                Genres = _context.MovieGenres.ToList()
            };
            return View("MovieForm", movieFormViewModel);
        }


        [HttpPost]
        public ActionResult Save(Movies movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel
                {
                    Movie = movie,
                    Genres = _context.MovieGenres.ToList()
                };
                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m != null && m.Id == movie.Id);

                movieInDb.Id = movie.Id;
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;

                movieInDb.AddedDate = movie.AddedDate;

                _context.Entry(movieInDb).State = EntityState.Modified;
            }

            _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }
    }
}