
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Route = GtfsApi.Models.Route;


namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GtfsController(GtfsContext context, 
        IRouteService routeSerivce, 
        IAgencyService agencyService,
        IStopService stopService)
        : ControllerBase
    
    {
        [HttpGet("Agencies")]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
        {
            List<Agency> agencies = await agencyService.GetAllAgencies();
            
            return  Ok(new { Agencies = agencies });
        }

        [HttpGet("{agencyId}")]
        public async Task<ActionResult<Agency>> GetAgencyByGtfsId(string agencyId)
        {
            Agency agency = await agencyService.GetAgencyByGtfsId(agencyId);
            
            return Ok(new { Agency = agency });
        }

        [HttpGet("Routes")]
        public async Task<IActionResult> GetAgencyRoutes(string agencyId)
        {
            List<Route> routes =  await routeSerivce.GetAgencyRoutesAsync(agencyId);
            
            return Ok(new { Routes = routes });
        }

        [HttpGet("Trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAgencyTrips(string agencyId, string gtfsRouteId, int results=10)
        {
            Route route = await routeSerivce.GetRouteAsync(agencyId, gtfsRouteId);

            int routeId = route.Id;

            List<Trip> trips = await routeSerivce.GetRouteTripsAsync(routeId);

            return Ok(new { Trips = trips });
        }
        
        [HttpGet("Stops")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops(string agencyId)
        {
            List<Route> routes = await routeSerivce.GetAgencyRoutesAsync(agencyId);
            
            List<int> routeIds = routes
                .Select(route => route.Id)
                .ToList();
           
            List<Trip> trips =  await routeSerivce.GetRouteTripsAsync(routeIds);

            List<int> stopIds = await routeSerivce.GetRouteStopIds(trips);
            
            List<Stop> stops = await stopService.GetStopListAsync(stopIds);
            
            return Ok(new { Stops = stops });
        }

        [HttpGet("StopTimes/{id}")]
        public async Task<ActionResult<IEnumerable<StopTime>>> GetStopTimes(int id)
        {
            Stop stop = await stopService.GetStopAsync(id);

            List<StopTime> stopTimes = await stopService.GetStopTimesAsync(stop);

            return Ok(new { StopTimes = stopTimes });
        }
    }
}

