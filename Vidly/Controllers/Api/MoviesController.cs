using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
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

        //Get /api/movies/
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.Include(mm => mm.Genre).ToList();
        }

        //Get /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.ToList().Single(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(Movie aMovie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _context.Movies.Add(aMovie);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + aMovie.Id), aMovie);
        }

        //PUT /api/movies
        [HttpPut]
        public void UpdateMovie(int id, Movie aMovie)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.ToList().Single(x => x.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            Mapper.Map<Movie, Movie>(aMovie, movieInDb);
            _context.SaveChanges();
        }

        //DELETE /api/movies
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);

            var movieInDb = _context.Movies.ToList().Single(x => x.Id == id);
            if (movieInDb == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }
    }
}
