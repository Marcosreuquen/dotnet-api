using Microsoft.AspNetCore.Mvc;
using challenge.Models;
using challenge.Data;


namespace challenge.Controllers
{
  [ApiController]
    [Route("characters")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> characters = new List<Character> 
        {
            new Character {
                Id= 1,
                Name= "Spider Man",
                Age = 30,
                History = "lorem ipsum",
                Img = "http://imagen",
                MoviesId = new int[] {1, 3, 6},
            }, new Character {
                Id= 2,
                Name= "IronMan",
                Age = 50,
                History = "lorem ipsum",
                Img = "http://imagen",
                MoviesId = new int[] {1, 2, 4},
            },
        };
        
        // private readonly ILogger<CharacterController> _logger;
        // private readonly ApplicationDbContext _context;

        // public CharacterController(ILogger<CharacterController> logger, ApplicationDbContext context)
        // {
        //     _logger = logger;
        //     _context = context;
        // }
        // public List<Character> Index(){
        //     var listOfCharacters = _context.DataCharacter.ToList();
        //     return listOfCharacters;
        // }
        //Listado de personajes: (/characters) sólo debe devolver Img y Name
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]string? name=null, [FromQuery]int? age=null, [FromQuery]int? movieId=null)
        {
            // BUSQUEDA: 
            // GET/characters?name=nombre


            if(name != null)
            {
                var characterByName = characters.FindAll(h => h.Name == name);
                if(characterByName == null)
                    return BadRequest("character not found.");
                return Ok(characterByName);
            }

            // GET/characters?age=edad
            else if(age != null)
            {
                var characterByAge = characters.FindAll(h => h.Age == age);
                if(characterByAge == null)
                    return BadRequest("character not found.");
                return Ok(characterByAge);
            }

            // GET/characters?movies=idMovie
            else if(movieId != null)
            {
                var characterByMovies = characters.FindAll(h => Array.Exists(h.MoviesId, el => el == movieId));
                if(characterByMovies == null)
                    return BadRequest("character not found.");
                return Ok(characterByMovies);
            }

            else
            {

                var list = new List<object>();

                foreach (var ch in characters)
                {
                    var newobj = new {
                        Name = ch.Name, 
                        Img = ch.Img
                    };
                    list.Add(newobj);
                };

                return Ok(list);
            }

        }

        //DETAIL: buscar por id, devolver todas las props
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var character = characters.Find(ch => ch.Id == id);

            if(character == null)
                return BadRequest("Character Not Found.");
            return Ok(character);
        }
        
        //CRUD
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]Character value)
        {
            characters.Add(
                new Character {
                    Id = characters.Count + 1,
                    Name = value.Name,
                    Age = value.Age,
                    History = value.History,
                    Img = value.Img,
                    MoviesId = value.MoviesId,
                }
            );

            return Ok(characters);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]Character value)
        {
            var character = characters.Find(ch => ch.Id == value.Id);

            if(character == null)
                return BadRequest("Character Not Found.");

            character.Name = value.Name;
            character.Age = value.Age;
            character.History = value.History;
            character.Img = value.Img;
            character.MoviesId = value.MoviesId;


            return Ok(character);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var character = characters.Find(h => h.Id == id);
            if(character == null)
                return BadRequest("character not found.");
            
            characters.Remove(character);
            return Ok(characters);

        }
    }
}