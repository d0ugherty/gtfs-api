
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
            var routes = await _context.Routes
                .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper()))
                .ToListAsync();
            
            return routes;
        }

        [HttpGet("Trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAgencyTrips(string agencyId, string routeId, int results=10)
        {
            var routes = await (_context.Routes
                    .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper()) 
                                 && rt.RouteId.Equals(routeId.ToUpper())))
                    .ToListAsync();
            
            var routeIds = routes
                .Select(route => route.Id)
                .ToList();
           
            var trips = await (_context.Trips
                    .Where(trip => routeIds.Contains(trip.FkRouteId)))
                    .Take(results)
                    .ToListAsync();
           
            return trips;
        }
        
        [HttpGet("Stops/{agencyId}")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops(string agencyId)
        {
            var routes = await (_context.Routes
                    .Where(rt => rt.GtfsAgencyId.Equals(agencyId.ToUpper())))
                    .ToListAsync();
            
            var routeIds = routes.Select(route => route.Id).ToList();
            
            var trips = await (_context.Trips
                    .Where(trip => routeIds.Contains(trip.FkRouteId))
                    .GroupBy(trip => trip.FkRouteId)
                    .Select(id => id.FirstOrDefault()))
                    .ToListAsync();
            
            var tripIds = trips.Select(trip => trip!.Id).ToList();

            var stopTimes = await (_context.StopTimes
                    .Where(stopTime => tripIds.Contains(stopTime.FkTripId))
                    .GroupBy(stopTime => stopTime.FkStopId)
                    .Select(stopId => stopId.FirstOrDefault()))
                    .ToListAsync();

            var stopIds = stopTimes.Select(stopTime => stopTime!.FkStopId).ToList();

            var stops = await (_context.Stops
                .Where(stop => stopIds.Contains(stop.Id))
                .Select(stop => stop)).ToListAsync();
            
            return stops;
        }
    }
}

