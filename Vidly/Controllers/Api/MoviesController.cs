using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Produces("application/json")]
    [System.Web.Http.Route("api/Movies")]
    public class MoviesController : ApiController
    {
            private ApplicationDbContext _context;

        public MoviesController()
        {
            _context= new ApplicationDbContext();
        }

            // GET /api/movies
            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("api/Movies")]
            public IHttpActionResult GetMovies()
            {
                var movieDto = _context.Movies.ToList().Select(Mapper.Map<Movies, MovieDto>);
                return Ok(movieDto);
            }

            // GET /api/movies/1
            [System.Web.Mvc.HttpGet]
            [System.Web.Http.Route("api/Movies/{id}")]
            public IHttpActionResult GetMovie(int id)
            {
                var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movie == null)
                    return NotFound();
                return Ok(Mapper.Map<Movies, MovieDto>(movie));
            }

            // POST /ap/movies
            [System.Web.Http.HttpPost]
            [System.Web.Http.Route("api/Movies")]
            public IHttpActionResult CreateMovie([System.Web.Http.FromBody] MovieDto movieDto)
            {
                if (!ModelState.IsValid)
                    //throw new HttpResponseException(HttpStatusCode.BadRequest);
                    return BadRequest();

                var movie = Mapper.Map<MovieDto, Movies>(movieDto);
                _context.Movies.Add(movie);
                _context.SaveChanges();

                movieDto.Id = movie.Id;
                return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
            }

            // PUT /api/movies/1
            [System.Web.Http.HttpPut]
            [System.Web.Http.Route("api/Movies/{id}")]
            public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var movieInDb = _context.Movies.SingleOrDefault(m => m != null && m.Id == id);

                if (movieInDb == null)
                    return NotFound();

                Mapper.Map(movieDto, movieInDb);

                _context.SaveChanges();

                return Ok();
            }

            // DELETE /api/movies/1
            [System.Web.Http.HttpDelete]
            [System.Web.Http.Route("api/Movies/{id}")]
            public IHttpActionResult DeleteMovie(int id)
            {
                var movieInDb = _context.Movies.SingleOrDefault(m => m != null && m.Id == id);

                if (movieInDb == null)
                    return NotFound();

                _context.Movies.Remove(movieInDb);
                _context.SaveChanges();

                return Ok();
            }
        }
}
