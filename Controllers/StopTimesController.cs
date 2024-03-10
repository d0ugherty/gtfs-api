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
    public class StopTimesController : ControllerBase
    {
        private readonly StopTimeContext _context;

        public StopTimesController(StopTimeContext context)
        {
            _context = context;
        }

        // GET: api/StopTimes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StopTime>>> GetStopTimes()
        {
            return await _context.StopTimes.ToListAsync();
        }

        // GET: api/StopTimes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StopTime>> GetStopTime(int id)
        {
            var stopTime = await _context.StopTimes.FindAsync(id);

            if (stopTime == null)
            {
                return NotFound();
            }

            return stopTime;
        }

        // PUT: api/StopTimes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStopTime(int id, StopTime stopTime)
        {
            if (id != stopTime.Id)
            {
                return BadRequest();
            }

            _context.Entry(stopTime).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StopTimeExists(id))
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

        // POST: api/StopTimes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StopTime>> PostStopTime(StopTime stopTime)
        {
            _context.StopTimes.Add(stopTime);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStopTime", new { id = stopTime.Id }, stopTime);
        }

        // DELETE: api/StopTimes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStopTime(int id)
        {
            var stopTime = await _context.StopTimes.FindAsync(id);
            if (stopTime == null)
            {
                return NotFound();
            }

            _context.StopTimes.Remove(stopTime);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StopTimeExists(int id)
        {
            return _context.StopTimes.Any(e => e.Id == id);
        }
    }
}
