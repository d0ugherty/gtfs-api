
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;


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
            var agencies = await agencyService.GetAllAgencies();
            
            return agencies;
        }

        [HttpGet("{agencyId}")]
        public async Task<ActionResult<Agency>> GetAgencyByGtfsId(string agencyId)
        {
            var agency = await agencyService.GetAgencyByGtfsId(agencyId);
            
            return agency;
        }

        [HttpGet("Routes")]
        public async Task<IActionResult> GetAgencyRoutes(string agencyId)
        {
            var routes =  await routeSerivce.GetAgencyRoutesAsync(agencyId);
            
            return Ok(new {Routes = routes});
        }

        [HttpGet("Trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAgencyTrips(string agencyId, string gtfsRouteId, int results=10)
        {
            var route = await routeSerivce.GetRouteAsync(agencyId, gtfsRouteId);

            var routeId = route.Id;

            var trips = await routeSerivce.GetRouteTripsAsync(routeId);

            return Ok(new { Trips = trips });
        }
        
        [HttpGet("Stops")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops(string agencyId)
        {
            var routes = await routeSerivce.GetAgencyRoutesAsync(agencyId);
            
            var routeIds = routes
                .Select(route => route.Id)
                .ToList();
           
            var trips =  await routeSerivce.GetRouteTripsAsync(routeIds);

            var stopIds = await routeSerivce.GetRouteStopIds(trips);
            
            var stops = await stopService.GetStopsById(stopIds);
            
            return stops;
        }
    }
}

