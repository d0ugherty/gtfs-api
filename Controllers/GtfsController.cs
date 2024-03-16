
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtfsController : ControllerBase 
    {
        private readonly GtfsContext  _context;

        public GtfsController(GtfsContext context)
        {
            _context = context;
        }
       
        [HttpGet("Agencies")]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
        {
            var agencies = await _context.Agencies.ToListAsync();
            return agencies;
        }
        
        
        [HttpGet("Routes/{agencyId}")]
        public async Task<ActionResult<IEnumerable<GtfsRoute>>> GetAgencyRoutes(string agencyId)
        {
            var routes = await _context.GtfsRoutes
                .Where(rt => rt.AgencyName == agencyId.ToUpper())
                .ToListAsync();
            
            return routes;
        }
        
        // Finish this
        [HttpGet("Stops/{agencyId}")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops(string agencyId)
        {
            var stops = await _context.Stops.ToListAsync(); 
            return stops;
        }
       
    }
}
