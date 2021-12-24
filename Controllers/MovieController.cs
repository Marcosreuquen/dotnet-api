using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using challenge.Models;
using challenge.Data;



namespace challenge.Controllers
{
    [ApiController]
    [Route("movies")]
    public class MovieController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie> 
        {
            new Movie {
                Id= 1,
                Title = "el maravilloso hombre araña",
                Img = "http://imagen",
                Calification = 2,
                DateOfCreation = DateTime.Now,
                CharactersId = new int[] {1, 3, 6},
                GenresId = new int[] {1, 7},
            }, new Movie {
                Id= 2,
                Title = "iron man 2",
                Img = "http://imagen",
                Calification = 4,
                DateOfCreation = DateTime.Now,
                CharactersId = new int[] {2, 4, 6},
                GenresId = new int[] {1, 4},
            }, 
        };

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string? name=null, [FromQuery]int? genre=null, [FromQuery]string? movieId=null)
        {
            //LISTADO: deberá mostrar solo Img, Title y DateOfCreation (GET/movies)
            //BÚSQUEDA:
            if(name != null)
            {
                //GET/movies?name=nombre
                var MoviesByName = movies.FindAll(m => m.Title == name);
                if(MoviesByName == null)
                    return BadRequest("Movie not found.");
                return Ok(MoviesByName);
            }

            else if(genre != null)
            {
                //GET/movies?genre=idGenre
                var MoviesByGenre = movies.FindAll(m => Array.Exists(m.GenresId, el => el == genre));
                if(MoviesByGenre == null)
                    return BadRequest("Genre not found.");
                return Ok(MoviesByGenre);
            }

            else if(movieId != null)
            {
                //GET/movies?order=ASC|DESC
                // movies.OrderByDescending();
                // var characterByMovies = movies.FindAll(h => Array.Exists(h.MoviesId, el => el == movieId));
                // if(characterByMovies == null)
                    // return BadRequest("character not found.");
                return Ok(movies);
            }

            else
            {

                var list = new List<object>();

                foreach (var m in movies)
                {
                    var newobj = new {
                        Title = m.Title, 
                        Img = m.Img,
                        DateOfCreation = m.DateOfCreation,
                    };
                    list.Add(newobj);
                };

                return Ok(list);
            };

        }

        //DETALLE: búsqueda por id y muestra todas las props
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var movie = movies.Find(ch => ch.Id == id);

            if(movie == null)
                return BadRequest("Movie Not Found.");
            return Ok(movie);
        }
        
        //CRUD
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Movie value)
        {

            movies.Add(
                new Movie {
                    Id = movies.Count + 1,
                    Title = value.Title,
                    Img = value.Img,
                    Calification = value.Calification,
                    CharactersId = value.CharactersId,
                    DateOfCreation = DateTime.Now,
                    GenresId = value.GenresId,

                }
            );

            return Ok(movies);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Movie value) 
        {
            var movie = movies.Find(ch => ch.Id == value.Id);

            if(movie == null)
                return BadRequest("Movie Not Found.");

            
            movie.Title = value.Title;
            movie.Img = value.Img;
            movie.Calification = value.Calification;
            movie.CharactersId = value.CharactersId;
            movie.GenresId = value.GenresId;

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movie = movies.Find(m => m.Id == id);
            if(movie == null)
                return BadRequest("Movie not found.");
            
            movies.Remove(movie);
            return Ok(movies);

        }
    }

}