
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static GtfsApi.Services.RouteService;
using Route = GtfsApi.Models.Route;


namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtfsController : ControllerBase
    {
        private readonly GtfsContext _context;
        private readonly IRouteService _routeService;

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
        public async Task<IActionResult> GetAgencyRoutes(string agencyId)
        {
            var routes = _routeService.GetAllRoutesAsync(agencyId);
            
            return Ok(new {Routes = routes});
        }

        [HttpGet("Trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAgencyTrips(string agencyId, string gtfsRouteId, int results=10)
        {
            var route = _routeService.GetRouteAsync(agencyId, gtfsRouteId);

            var routeId = route.Id;

            var trips = _routeService.GetRouteTripsAsync(routeId, results);

            return Ok(new { Trips = trips });
        }
        
        [HttpGet("Stops/{agencyId}")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops(string agencyId)
        {
            var routes = _routeService.GetAllRoutesAsync(agencyId);
            
            var routeIds = routes
                .Where()
                .Select(route => route.Id)
                .ToList();
           
            var trips = await (_context.Trips
                    .Where(trip => routeIds.Contains(trip.FkRouteId)))
                    .ToListAsync();
            
            var tripIds = trips.Select(trip => trip!.Id).ToList();

            var stopTimes = await (_context.StopTimes
                    .Where(stopTime => tripIds.Contains(stopTime.FkTripId))
                    .GroupBy(stopTime => stopTime.FkStopId)
                    .Select(stopId => stopId.FirstOrDefault()))
                    .ToListAsync();
            foreach (var stopTime in stopTimes)
            {
                Console.WriteLine(stopTime.Stop);
            }
            var stopIds = stopTimes.Select(stopTime => stopTime!.FkStopId).ToList();

            var stops = await (_context.Stops
                .Where(stop => stopIds.Contains(stop.Id))
                .Select(stop => stop)).ToListAsync();
            
            return stops;
        }
    }
}

