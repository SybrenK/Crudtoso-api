using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;

//namespace Crudtoso_api.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    [Produces(MediaTypeNames.Application.Json)]
//    [Consumes(MediaTypeNames.Application.Json)]

//    public class MoviesController : ControllerBase
//    {
//        private readonly MoviesDbContext _context;
//        private readonly IMapper _mapper;

//        public MoviesController(MoviesDbContext context, IMapper mapper)
//        {
//            _context = context;
//            _mapper = mapper;
//        }

//        /// <summary>
//        /// Retrieves a list of movies along with their associated character movies.
//        /// </summary>
//        /// <returns>An ActionResult containing a list of MovieReadDTO objects.</returns>
//        // GET: api/Movies
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<MovieReadDTO>>> GetMovies()
//        {
//            // Check if the Movies collection is null.
//            if (_context.Movies == null)
//            {
//                // Return a NotFound response if the collection is null.
//                return NotFound();
//            }

//            // Query the database to retrieve the list of movies, including their associated character movies.
//            var moviesList = await _context.Movies.Include(c => c.characterMovies).ToListAsync();

//            // Map the moviesList to a list of MovieReadDTO objects using the mapper.
//            var moviesDtoList = _mapper.Map<List<MovieReadDTO>>(moviesList);

//            // Return the list of MovieReadDTO objects.
//            return moviesDtoList;
//        }


//        /// <summary>
//        /// Retrieves a specific movie by its ID.
//        /// </summary>
//        /// <param name="id">The ID of the movie to retrieve.</param>
//        /// <returns>An ActionResult containing the MovieReadDTO object representing the movie.</returns>
//        // GET: api/Movies/5
//        [HttpGet("{id}")]
//        [ProducesResponseType(200)]
//        [ProducesResponseType(404)]
//        public async Task<ActionResult<MovieReadDTO>> GetMovie(int id)
//        {
//            // Query the database to retrieve the movie based on the provided ID.
//            var domainMovie = await _context.Movies.FindAsync(id);

//            // Convert the domainMovie to a MovieReadDTO object using the mapper.
//            var movieDTO = _mapper.Map<MovieReadDTO>(domainMovie);

//            // Check if the movie ID is in the database.
//            if (movieDTO is null)
//            {
//                // Return a NotFound response if the movie is null.
//                return NotFound();
//            }


//            // Return an OK response with the MovieReadDTO object.
//            return Ok(movieDTO);
//        }


//        /// <summary>
//        /// Updates a specific movie with the provided changes.
//        /// </summary>
//        /// <param name="id">The ID of the movie to update.</param>
//        /// <param name="movieChangeDTO">The MovieUpdateDTO object containing the changes.</param>
//        /// <returns>An IActionResult representing the result of the update operation.</returns>
//        // PUT: api/Movies/5
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPut("{id}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(204)]
//        public async Task<IActionResult> PutMovie(int id, MovieUpdateDTO movieChangeDTO)
//        {
//            // Check if the provided ID matches the ID in the movieChangeDTO.
//            if (id != movieChangeDTO.Id)
//            {
//                // Return a BadRequest response if the IDs don't match.
//                return BadRequest();
//            }

//            // Map the MovieUpdateDTO to a Movie domain object.
//            var domainMovie = _mapper.Map<Movie>(movieChangeDTO);

//            // Set the state of the domainMovie object to Modified to indicate that it has been updated.
//            _context.Entry(domainMovie).State = EntityState.Modified;

//            try
//            {
//                // Save the changes to the database.
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                // Check if the movie with the provided ID exists in the database.
//                if (!MovieExists(id))
//                {
//                    // Return a NotFound response if the movie doesn't exist.
//                    return NotFound();
//                }
//                else
//                {
//                    // Rethrow the exception if it is a concurrency exception.
//                    throw;
//                }
//            }

//            // Return a NoContent response to indicate a successful update.
//            return NoContent();
//        }


//        /// <summary>
//        /// Creates a new movie with the provided data.
//        /// </summary>
//        /// <param name="newMovieDTO">The MovieCreateDTO object containing the data for the new movie.</param>
//        /// <returns>An ActionResult containing the created MovieReadDTO object.</returns>
//        // POST: api/Movies
//        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
//        [HttpPost]
//        [ProducesResponseType(203)]
//        public async Task<ActionResult<Movie>> PostMovie(MovieCreateDTO newMovieDTO)
//        {
//            // Check if the Movies collection is null.
//            if (_context.Movies == null)
//            {
//                // Return a Problem response with a specific error message if the collection is null.
//                return Problem("Entity set 'MoviesDbContext.Movies' is null.");
//            }

//            // Map the MovieCreateDTO to a Movie domain object.
//            var domainMovie = _mapper.Map<Movie>(newMovieDTO);

//            // Add the domainMovie to the Movies collection.
//            _context.Movies.Add(domainMovie);

//            // Save the changes to the database.
//            await _context.SaveChangesAsync();

//            // Map the created domainMovie to a MovieReadDTO object.
//            var movieReadDTO = _mapper.Map<MovieReadDTO>(domainMovie);

//            // Return a CreatedAtAction response with the created MovieReadDTO object.
//            return CreatedAtAction("GetMovie", new { id = movieReadDTO.MovieTitle }, movieReadDTO);
//        }


//        // DELETE: api/Movies/5
//        /// <summary>
//        /// Deletes a specific movie based on the provided ID.
//        /// </summary>
//        /// <param name="id">The ID of the movie to delete.</param>
//        /// <returns>An IActionResult representing the result of the deletion operation.</returns>
//        [HttpDelete("{id}")]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(204)]
//        public async Task<IActionResult> DeleteMovie(int id)
//        {
//            if (_context.Movies == null)
//            {
//                return NotFound();
//            }
//            var movie = await _context.Movies.FindAsync(id);
//            if (movie == null)
//            {
//                return NotFound();
//            }

//            _context.Movies.Remove(movie);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

//        private bool MovieExists(int id)
//        {
//            return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
//        }

//        //CUSTOM ENDPOINTS

//        /// <summary>
//        /// Adds characters to a movie based on the movie ID and an array of character IDs.
//        /// </summary>
//        /// <param name="id">The ID of the movie.</param>
//        /// <param name="characterIds">An array of character IDs to add to the movie.</param>
//        /// <returns>An ActionResult representing the result of the operation.</returns>
//        [HttpPut("addToMovie/{id}")]
//        [ProducesResponseType(400)]
//        [ProducesResponseType(204)]
//        public async Task<ActionResult> AddCharacterToMovie(int id, [FromBody] int[] characterIds)
//        {
//            // Find the movie in the database based on the provided ID.
//            var domainMovie = await _context.Movies.FindAsync(id);

//            foreach (int characterId in characterIds)
//            {
//                // Find the character in the database based on the character ID.
//                var domainCharacter = await _context.Characters.FindAsync(characterId);

//                if (domainCharacter == null)
//                {
//                    // Return a BadRequest response if the character doesn't exist.
//                    return BadRequest($"No character to assign to Movie with ID {characterId}");
//                }

//                List<int> characterMovieIds = new List<int>();
//                if (domainCharacter.characterMovies != null)
//                {
//                    // Get a list of movie IDs associated with the character.
//                    foreach (CharacterMovie cm in domainCharacter.characterMovies)
//                    {
//                        characterMovieIds.Add(cm.MovieId);
//                    }
//                }

//                if (!characterMovieIds.Contains(id))
//                {
//                    // Create a new CharacterMovie object and add it to the context if the character is not already associated with the movie.
//                    CharacterMovie characterMovieToAdd = new CharacterMovie()
//                    {
//                        Character = domainCharacter,
//                        CharacterId = characterId,
//                        Movie = domainMovie,
//                        MovieId = id
//                    };

//                    _context.CharacterMovies.Add(characterMovieToAdd);
//                }
//            }

//            // Save the changes to the database.
//            await _context.SaveChangesAsync();

//            // Return a NoContent response to indicate a successful operation.
//            return NoContent();
//        }




//        /// <summary>
//        /// Retrieves a list of characters in a movie based on the movie ID.
//        /// </summary>
//        /// <param name="id">The ID of the movie.</param>
//        /// <returns>An ActionResult containing a list of CharacterReadDTO objects representing the characters in the movie.</returns>
//        [HttpGet("{id}/Characters")]
//        [ProducesResponseType(404)]
//        [ProducesResponseType(200)]
//        public async Task<ActionResult<IEnumerable<CharacterReadDTO>>> GetCharactersInMovie(int id)
//        {
//            // Query the movie with its characters. Use .Include to load the related entities from the database.
//            var movie = await _context.Movies
//                .Include(m => m.characterMovies) // Load the CharacterMovie relations
//                .ThenInclude(cm => cm.Character) // For each CharacterMovie, load the Character entity
//                .FirstOrDefaultAsync(m => m.Id == id); // Get the movie that matches the provided id

//            // Check if the movie exists. If not, return a 404 Not Found status.
//            if (movie == null)
//            {
//                return NotFound();
//            }

//            // Map the Character entities to CharacterReadDTOs, using AutoMapper.
//            var characterDtos = _mapper.Map<List<CharacterReadDTO>>(movie.characterMovies.Select(cm => cm.Character));

//            // Return the list of CharacterReadDTOs with a 200 OK status.
//            return Ok(characterDtos);
//        }



//    }
//}