
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using GtfsApi.Migrations;


namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtfsController : ControllerBase
    {
        private readonly GtfsContext _context;

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
        // Need to finish this. I'll need to get more creative since this is timing out
        // I think on the trips and stop times.
        [HttpGet("Stops/{agencyId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAgencyStops(string agencyId)
        {
            
            var stops = await (_context.Stops
                .Join(_context.StopTimes,
                stop => stop.Id,
                stopTime => stopTime.StopId,
                (stop, stopTime) => new { stop, stopTime })
                .Join(_context.Trips,
                    joined => joined.stopTime.TripId,
                    trip => trip.Id,
                    (joined, trip) => new {joined.stop, joined.stopTime, trip})
                .Join(_context.GtfsRoutes,
                    joined => joined.trip.RouteId,
                    route => route.Id,
                    (joined, route) => new {joined.trip, route})
                .Join(_context.Agencies,
                    joined => joined.route.AgencyId,
                    agency => agency.Id,
                    (joined, agency) => new {joined.route, agency})
                .Where(joined => joined.agency.AgencyId == agencyId))
                .ToListAsync();
            return stops;
        }
    }
    }

