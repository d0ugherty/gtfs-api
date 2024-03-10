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
    public class ShapesController : ControllerBase
    {
        private readonly ShapeContext _context;

        public ShapesController(ShapeContext context)
        {
            _context = context;
        }

        // GET: api/Shapes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shape>>> GetShapes()
        {
            return await _context.Shapes.ToListAsync();
        }

        // GET: api/Shapes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Shape>> GetShape(int id)
        {
            var shape = await _context.Shapes.FindAsync(id);

            if (shape == null)
            {
                return NotFound();
            }

            return shape;
        }

        // PUT: api/Shapes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShape(int id, Shape shape)
        {
            if (id != shape.Id)
            {
                return BadRequest();
            }

            _context.Entry(shape).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShapeExists(id))
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

        // POST: api/Shapes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shape>> PostShape(Shape shape)
        {
            _context.Shapes.Add(shape);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetShape", new { id = shape.Id }, shape);
        }

        // DELETE: api/Shapes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShape(int id)
        {
            var shape = await _context.Shapes.FindAsync(id);
            if (shape == null)
            {
                return NotFound();
            }

            _context.Shapes.Remove(shape);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShapeExists(int id)
        {
            return _context.Shapes.Any(e => e.Id == id);
        }
    }
}
