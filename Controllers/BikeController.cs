using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Crudtoso_api.Data;
using Crudtoso_api.Data.DTOs.BikeDTOs;
using Crudtoso_api.Model;

namespace Crudtoso_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]

    public class BikeController : ControllerBase
    {
        private readonly BikesDbContext _context;
        private readonly IMapper _mapper;

        public BikeController(BikesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of movies along with their associated character movies.
        /// </summary>
        /// <returns>An ActionResult containing a list of BikeReadDTO objects.</returns>
        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeReadDTO>>> GetBikes()
        {
            // Check if the Movies collection is null.
            if (_context.BikeDbs == null)
            {
                // Return a NotFound response if the collection is null.
                return NotFound();
            }

            // Query the database to retrieve the list of movies, including their associated character movies.
            var moviesList = await _context.BikeDbs.ToListAsync();

            // Map the moviesList to a list of BikeReadDTO objects using the mapper.
            var moviesDtoList = _mapper.Map<List<BikeReadDTO>>(moviesList);

            // Return the list of BikeReadDTO objects.
            return moviesDtoList;
        }


        /// <summary>
        /// Retrieves a specific movie by its ID.
        /// </summary>
        /// <param name="id">The ID of the movie to retrieve.</param>
        /// <returns>An ActionResult containing the BikeReadDTO object representing the movie.</returns>
        // GET: api/Movies/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BikeReadDTO>> GetMovie(int id)
        {
            // Query the database to retrieve the movie based on the provided ID.
            var domainMovie = await _context.BikeDbs.FindAsync(id);

            // Convert the domainMovie to a BikeReadDTO object using the mapper.
            var movieDTO = _mapper.Map<BikeReadDTO>(domainMovie);

            // Check if the movie ID is in the database.
            if (movieDTO is null)
            {
                // Return a NotFound response if the movie is null.
                return NotFound();
            }


            // Return an OK response with the BikeReadDTO object.
            return Ok(movieDTO);
        }


        /// <summary>
        /// Updates a specific movie with the provided changes.
        /// </summary>
        /// <param name="id">The ID of the movie to update.</param>
        /// <param name="bikeChangeDTO">The BikeUpdateDTO object containing the changes.</param>
        /// <returns>An IActionResult representing the result of the update operation.</returns>
        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutMovie(int id, BikeUpdateDTO bikeChangeDTO)
        {
            // Check if the provided ID matches the ID in the bikeChangeDTO.
            if (id != bikeChangeDTO.ProductId)
            {
                // Return a BadRequest response if the IDs don't match.
                return BadRequest();
            }

            // Map the BikeUpdateDTO to a Movie domain object.
            var domainMovie = _mapper.Map<BikeDb>(bikeChangeDTO);

            // Set the state of the domainMovie object to Modified to indicate that it has been updated.
            _context.Entry(domainMovie).State = EntityState.Modified;

            try
            {
                // Save the changes to the database.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the movie with the provided ID exists in the database.
                if (!MovieExists(id))
                {
                    // Return a NotFound response if the movie doesn't exist.
                    return NotFound();
                }
                else
                {
                    // Rethrow the exception if it is a concurrency exception.
                    throw;
                }
            }

            // Return a NoContent response to indicate a successful update.
            return NoContent();
        }


        /// <summary>
        /// Creates a new movie with the provided data.
        /// </summary>
        /// <param name="newMovieDTO">The BikeCreateDTO object containing the data for the new movie.</param>
        /// <returns>An ActionResult containing the created BikeReadDTO object.</returns>
        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(203)]
        public async Task<ActionResult<BikeDb>> PostMovie(BikeCreateDTO newMovieDTO)
        {
            // Check if the Movies collection is null.
            if (_context.BikeDbs == null)
            {
                // Return a Problem response with a specific error message if the collection is null.
                return Problem("Entity set 'BikesDbContext.Movies' is null.");
            }

            // Map the BikeCreateDTO to a Movie domain object.
            var domainMovie = _mapper.Map<BikeDb>(newMovieDTO);

            // Add the domainMovie to the Movies collection.
            _context.BikeDbs.Add(domainMovie);

            // Save the changes to the database.
            await _context.SaveChangesAsync();

            // Map the created domainMovie to a BikeReadDTO object.
            var bikeReadDTO = _mapper.Map<BikeReadDTO>(domainMovie);

            // Return a CreatedAtAction response with the created BikeReadDTO object.
            return CreatedAtAction("GetMovie", new { id = bikeReadDTO.ProductName }, bikeReadDTO);
        }


        // DELETE: api/Movies/5
        /// <summary>
        /// Deletes a specific movie based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the movie to delete.</param>
        /// <returns>An IActionResult representing the result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            if (_context.BikeDbs == null)
            {
                return NotFound();
            }
            var movie = await _context.BikeDbs.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.BikeDbs.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return (_context.BikeDbs?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        //CUSTOM ENDPOINTS


    }
}