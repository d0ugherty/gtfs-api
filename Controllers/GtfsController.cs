
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;


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
        
        [HttpGet("Stops/{agencyId}")]
        public async Task<ActionResult<IEnumerable<object>>> GetAgencyStops(string agencyId)
        {
            var routes = await (_context.GtfsRoutes
                    .Where(rt => rt.AgencyName == agencyId.ToUpper()))
                    .ToListAsync();

            var routeIds = routes.Select(route => route.Id).ToList();

            var trips = await (_context.Trips
                    .Where(trip => routeIds.Contains(trip.RouteId))
                    .GroupBy(trip => trip.RouteId)
                    .Select(id => id.FirstOrDefault()))
                    .ToListAsync();

            var tripIds = trips.Select(trip => trip!.Id).ToList();

            var stopTimes = await (_context.StopTimes
                    .Where(stopTime => tripIds.Contains(stopTime.TripId))
                    .GroupBy(stopTime => stopTime.StopId)
                    .Select(stopId => stopId.FirstOrDefault()))
                    .ToListAsync();

            var stopIds = stopTimes.Select(stopTime => stopTime!.StopId).ToList();

            var stops = await (_context.Stops
                .Where(stop => stopIds.Contains(stop.Id))
                .Select(stop => stop)).ToListAsync();
            
            return stops;
        }
    }
}

