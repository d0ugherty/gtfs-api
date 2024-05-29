using Gtfs.Domain.Models;
using Gtfs.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Route = Gtfs.Domain.Models.Route;

namespace GtfsApi.Controllers {

    [Route("gtfs/amtrak")]
    [ApiController]
    public class AmtrakController : ControllerBase
    {
        private readonly RouteService _routeService;
        private readonly AgencyService _agencyService;
        private readonly TripService _tripService;
        private readonly StopService _stopService;

        private readonly string _sourceName;

        public AmtrakController(RouteService routeService, AgencyService agencyService, StopService stopService,
            TripService tripService)
        {
            _routeService = routeService;
            _agencyService = agencyService;
            _stopService = stopService;
            _tripService = tripService;
            _sourceName = "Amtrak";
        }

        [HttpGet("agencies/all")]
        public async Task<ActionResult<List<Agency>>> GetAllAgencies()
        {
            var agencies = await _agencyService.GetAgenciesBySource(_sourceName);

            return Ok(new { Agencies = agencies });
        }
        
        [HttpGet("routes/all")]
        public async Task<ActionResult<List<Route>>> GetAllRoutes()
        {
            var routes = await _routeService.GetRoutesBySource(_sourceName);

            return Ok(new { Routes = routes });
        }

        [HttpGet("routes/{id}")]
        public async Task<ActionResult<Route>> GetRouteById(int id)
        {
            var route = await _routeService.GetRouteById(id);

            return Ok(new { Route = route });
        }

        [HttpGet("routes/agency/{agencyName}")]
        public async Task<ActionResult<Route>> GetRoutesByAgency(string agencyName)
        {
            var routes = await _routeService.GetRoutesByAgencyName(agencyName);

            return Ok(new { Routes = routes });
        }
    }
}