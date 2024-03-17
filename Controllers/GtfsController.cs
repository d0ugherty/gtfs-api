
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Route = GtfsApi.Models.Route;


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
        public async Task<ActionResult<IEnumerable<Route>>> GetAgencyRoutes(string agencyId)
        {
            var routes = await _context.GtfsRoutes
                .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper()))
                .ToListAsync();
            
            return routes;
        }
        
        [HttpGet("Stops/{agencyId}")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops(string agencyId)
        {
            var routes = await (_context.GtfsRoutes
                    .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper())))
                    .ToListAsync();

            foreach (var route in routes)
            {
                Console.WriteLine(route.ShortName);
            }
            
            var routeIds = routes.Select(route => route.Id).ToList();
            
            Console.WriteLine($"routeIds empty: {routeIds.IsNullOrEmpty()}");
                
            foreach (var id in routeIds)
            {
                
                Console.WriteLine(id);
                
            }
            
            var trips = await (_context.Trips
                    .Where(trip => routeIds.Contains(trip.FkRouteId))
                    .GroupBy(trip => trip.FkRouteId)
                    .Select(id => id.FirstOrDefault()))
                    .ToListAsync();
            
            Console.WriteLine($"trips empty: {trips.IsNullOrEmpty()}");
            
            foreach (var trip in trips)
            {
                Console.WriteLine(trip.TripId);
            }
            
            var tripIds = trips.Select(trip => trip!.Id).ToList();

            var stopTimes = await (_context.StopTimes
                    .Where(stopTime => tripIds.Contains(stopTime.FkTripId))
                    .GroupBy(stopTime => stopTime.FkStopId)
                    .Select(stopId => stopId.FirstOrDefault()))
                    .ToListAsync();
            Console.WriteLine($"stopTimes Empty : {stopTimes.IsNullOrEmpty()}");
            
            
            var stopIds = stopTimes.Select(stopTime => stopTime!.FkStopId).ToList();

            var stops = await (_context.Stops
                .Where(stop => stopIds.Contains(stop.Id))
                .Select(stop => stop)).ToListAsync();
            
            return stops;
        }
    }
}

