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
    public class FareAttributesController : ControllerBase
    {
        private readonly FareAttributesContext _context;

        public FareAttributesController(FareAttributesContext context)
        {
            _context = context;
        }

        // GET: api/FareAttributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FareAttributes>>> GetFareAttributesEnumerable()
        {
            return await _context.FareAttributesEnumerable.ToListAsync();
        }

        // GET: api/FareAttributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FareAttributes>> GetFareAttributes(int id)
        {
            var fareAttributes = await _context.FareAttributesEnumerable.FindAsync(id);

            if (fareAttributes == null)
            {
                return NotFound();
            }

            return fareAttributes;
        }

        // PUT: api/FareAttributes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFareAttributes(int id, FareAttributes fareAttributes)
        {
            if (id != fareAttributes.Id)
            {
                return BadRequest();
            }

            _context.Entry(fareAttributes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FareAttributesExists(id))
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

        // POST: api/FareAttributes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FareAttributes>> PostFareAttributes(FareAttributes fareAttributes)
        {
            _context.FareAttributesEnumerable.Add(fareAttributes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFareAttributes", new { id = fareAttributes.Id }, fareAttributes);
        }

        // DELETE: api/FareAttributes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFareAttributes(int id)
        {
            var fareAttributes = await _context.FareAttributesEnumerable.FindAsync(id);
            if (fareAttributes == null)
            {
                return NotFound();
            }

            _context.FareAttributesEnumerable.Remove(fareAttributes);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FareAttributesExists(int id)
        {
            return _context.FareAttributesEnumerable.Any(e => e.Id == id);
        }
    }
}
