using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GtfsApi.Models;

namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaresController : ControllerBase
    {
        private readonly FareContext _context;

        public FaresController(FareContext context)
        {
            _context = context;
        }

        // GET: api/Fares
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fare>>> GetFares()
        {
            return await _context.Fares.ToListAsync();
        }

        // GET: api/Fares/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Fare>> GetFare(int id)
        {
            var fare = await _context.Fares.FindAsync(id);

            if (fare == null)
            {
                return NotFound();
            }

            return fare;
        }

        // PUT: api/Fares/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFare(int id, Fare fare)
        {
            if (id != fare.Id)
            {
                return BadRequest();
            }

            _context.Entry(fare).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FareExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fares
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fare>> PostFare(Fare fare)
        {
            _context.Fares.Add(fare);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFare", new { id = fare.Id }, fare);
        }

        // DELETE: api/Fares/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFare(int id)
        {
            var fare = await _context.Fares.FindAsync(id);
            if (fare == null)
            {
                return NotFound();
            }

            _context.Fares.Remove(fare);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FareExists(int id)
        {
            return _context.Fares.Any(e => e.Id == id);
        }
    }
}
