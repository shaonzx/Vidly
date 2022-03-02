using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        //GET /api/newrentals
        public List<Rental> GetSuccess()
        {
            return _context.Rentals.ToList();
        }

        //POST /api/newrentals
        [HttpPost]
        /*[Authorize(Roles = RoleName.CanManageMovies)] */
        public IHttpActionResult CreateNewRentals(NewRentalDtos newRentalDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = _context.Customers.Single(c => c.Id == newRentalDto.CustomerId);
            var movies = _context.Movies.Where(m => newRentalDto.MovieIds.Contains(m.Id));

            Rental aRental = null;
            foreach (var movie in movies)
            {
                movie.NumberAvailable--;
                aRental = new Rental {Customer = customer, Movie = movie, DateRented = DateTime.Today};
                _context.Rentals.Add(aRental);
            }
            _context.SaveChanges();


            return Ok();
        }
    }
    
}
