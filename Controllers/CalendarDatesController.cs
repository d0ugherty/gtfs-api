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
    public class CalendarDatesController : ControllerBase
    {
        private readonly CalendarDateContext _context;

        public CalendarDatesController(CalendarDateContext context)
        {
            _context = context;
        }

        // GET: api/CalendarDates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarDate>>> GetCalendarDates()
        {
            return await _context.CalendarDates.ToListAsync();
        }

        // GET: api/CalendarDates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalendarDate>> GetCalendarDate(int id)
        {
            var calendarDate = await _context.CalendarDates.FindAsync(id);

            if (calendarDate == null)
            {
                return NotFound();
            }

            return calendarDate;
        }

        // PUT: api/CalendarDates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalendarDate(int id, CalendarDate calendarDate)
        {
            if (id != calendarDate.Id)
            {
                return BadRequest();
            }

            _context.Entry(calendarDate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalendarDateExists(id))
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

        // POST: api/CalendarDates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalendarDate>> PostCalendarDate(CalendarDate calendarDate)
        {
            _context.CalendarDates.Add(calendarDate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalendarDate", new { id = calendarDate.Id }, calendarDate);
        }

        // DELETE: api/CalendarDates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalendarDate(int id)
        {
            var calendarDate = await _context.CalendarDates.FindAsync(id);
            if (calendarDate == null)
            {
                return NotFound();
            }

            _context.CalendarDates.Remove(calendarDate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalendarDateExists(int id)
        {
            return _context.CalendarDates.Any(e => e.Id == id);
        }
    }
}
