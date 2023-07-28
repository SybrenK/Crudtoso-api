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

    public class BikesController : ControllerBase
    {
        private readonly BikesDbContext _context;
        private readonly IMapper _mapper;

        public BikesController(BikesDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves a list of bikes.
        /// </summary>
        /// <returns>An ActionResult containing a list of BikeReadDTO objects.</returns>
        // GET: api/Bikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BikeReadDTO>>> GetBikes()
        {
            // Check if the Bikes collection is null.
            if (_context.BikeDbs == null)
            {
                // Return a NotFound response if the collection is null.
                return NotFound();
            }

            // Query the database to retrieve the list of bikes, including their associated character bikes.
            var bikesList = await _context.BikeDbs.ToListAsync();

            // Map the bikesList to a list of BikeReadDTO objects using the mapper.
            var bikesDtoList = _mapper.Map<List<BikeReadDTO>>(bikesList);

            // Return the list of BikeReadDTO objects.
            return bikesDtoList;
        }


        /// <summary>
        /// Retrieves a specific bike by its ID.
        /// </summary>
        /// <param name="id">The ID of the bike to retrieve.</param>
        /// <returns>An ActionResult containing the BikeReadDTO object representing the bike.</returns>
        // GET: api/Bikes/5
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<BikeReadDTO>> GetBike(int id)
        {
            // Query the database to retrieve the bike based on the provided ID.
            var domainBike = await _context.BikeDbs.FindAsync(id);

            // Convert the domainBike to a BikeReadDTO object using the mapper.
            var bikeDTO = _mapper.Map<BikeReadDTO>(domainBike);

            // Check if the bike ID is in the database.
            if (bikeDTO is null)
            {
                // Return a NotFound response if the bike is null.
                return NotFound();
            }


            // Return an OK response with the BikeReadDTO object.
            return Ok(bikeDTO);
        }


        /// <summary>
        /// Updates a specific bike with the provided changes.
        /// </summary>
        /// <param name="id">The ID of the bike to update.</param>
        /// <param name="bikeChangeDTO">The BikeUpdateDTO object containing the changes.</param>
        /// <returns>An IActionResult representing the result of the update operation.</returns>
        // PUT: api/Bikes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> PutBike(int id, BikeUpdateDTO bikeChangeDTO)
        {
            // Check if the provided ID matches the ID in the bikeChangeDTO.
            if (id != bikeChangeDTO.ProductId)
            {
                // Return a BadRequest response if the IDs don't match.
                return BadRequest("Queried ID & ID in updated json don't match");
            }

            // Map the BikeUpdateDTO to a Bike domain object.
            var domainBike = _mapper.Map<BikeDb>(bikeChangeDTO);

            // Set the state of the domainBike object to Modified to indicate that it has been updated.
            _context.Entry(domainBike).State = EntityState.Modified;

            try
            {
                // Save the changes to the database.
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Check if the bike with the provided ID exists in the database.
                if (!BikeExists(id))
                {
                    // Return a NotFound response if the bike doesn't exist.
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
        /// Creates a new bike with the provided data.
        /// </summary>
        /// <param name="newBikeDTO">The BikeCreateDTO object containing the data for the new bike.</param>
        /// <returns>An ActionResult containing the created BikeReadDTO object.</returns>
        // POST: api/Bikes
        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<ActionResult<BikeDb>> PostBike(BikeCreateDTO newBikeDTO)
        {
            // Check if the Bikes collection is null.
            if (_context.BikeDbs == null)
            {
                // Return a Problem response with a specific error message if the collection is null.
                return Problem("Entity set 'BikesDbContext.Bikes' is null.");
            }

            // Map the BikeCreateDTO to a Bike domain object.
            var domainBike = _mapper.Map<BikeDb>(newBikeDTO);

            // Add the domainBike to the Bikes collection.
            _context.BikeDbs.Add(domainBike);

            // Save the changes to the database.
            await _context.SaveChangesAsync();

            // Map the created domainBike to a BikeReadDTO object.
            var bikeReadDTO = _mapper.Map<BikeReadDTO>(domainBike);

            // Return a CreatedAtAction response with the created BikeReadDTO object.
            return CreatedAtAction("GetBike", new { id = bikeReadDTO.ProductName }, bikeReadDTO);
        }


        // DELETE: api/Bikes/5
        /// <summary>
        /// Deletes a specific bike based on the provided ID.
        /// </summary>
        /// <param name="id">The ID of the bike to delete.</param>
        /// <returns>An IActionResult representing the result of the deletion operation.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteBike(int id)
        {
            if (_context.BikeDbs == null)
            {
                return NotFound();
            }
            var bike = await _context.BikeDbs.FindAsync(id);
            if (bike == null)
            {
                return NotFound();
            }

            _context.BikeDbs.Remove(bike);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BikeExists(int id)
        {
            return (_context.BikeDbs?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }

        //CUSTOM ENDPOINTS IF WE WANT ANY 


    }
}