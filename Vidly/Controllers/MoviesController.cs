using System;
using System.Collections.Generic;
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

        // GET: Movies/Random
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shrek!"};

            //var customers = new List<Customer>();
            var customers = new List<Customer>
            {
                new Customer{Name = "Customer 1"},
                new Customer{Name = "Customer 2"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
            //return Content("Hell yeah!");
            //return HttpNotFound();
            //return new EmptyResult();
            //return RedirectToAction("Index", "Home", new  {page=1, sortBy="Name"});

            //ViewData["Movie"] = movie;
            //ViewBag.RandomMovie = movie;

            //return View();
        }


        //Movies........ return list of movies
        public ActionResult Index()
        {
            //var movies = GetMovies();
            var movies = _context.Movies.Include(g => g.Genre).ToList();
            //var movies = _context.Movies.ToList();

            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List", movies);
            }
            else
            {
                return View("ReadOnlyList", movies);
            }

            //return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(g => g.Genre).FirstOrDefault(x => x.Id == id);

            return View(movie);
        }

        private IEnumerable<Movie> GetMovies()
        {
            var movies = new List<Movie>
            {
                new Movie {Id = 1, Name = "Shrek"},
                new Movie {Id = 2, Name = "Wall-E"}
            };

            return movies;
        }

        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult NewMovie()
        {
            var genres = _context.Genres.ToList();
            var movieFormViewModel = new MovieFromViewModel
            {
                Movie = new Movie(),
                Genres = genres
            };

            return View("MovieForm", movieFormViewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.ToList().Single(m => m.Id == id);
            var genres = _context.Genres.ToList();
            var movieFormViewModel = new MovieFromViewModel
            {
                Movie = movie,
                Genres = genres
            };

            return View("MovieForm", movieFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(MovieFromViewModel vm)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (!ModelState.IsValid)
            {
                MovieFromViewModel aNewVM = new MovieFromViewModel
                {
                    Movie = vm.Movie,
                    Genres = _context.Genres.ToList()
                };
                return View("MovieForm", aNewVM);
            }

            if (vm.Movie.Id == 0)
            {
                if (vm.Movie.DateAdded == DateTime.MinValue)
                {
                    vm.Movie.DateAdded = DateTime.Now;
                }
                _context.Movies.Add(vm.Movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == vm.Movie.Id);
                movieInDb.Name = vm.Movie.Name;
                movieInDb.ReleaseDate = vm.Movie.ReleaseDate;
                movieInDb.GenreId = vm.Movie.GenreId;
                movieInDb.NumberInStock = vm.Movie.NumberInStock;
                //TryUpdateModel(movieInDb);
            }
            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }
    }
}