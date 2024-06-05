using Gtfs.Domain.Models;
using Gtfs.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Route = Gtfs.Domain.Models.Route;

namespace GtfsApi.Controllers
{
    [Route("gtfs/septa")]
    [ApiController]
    public class SEPTAController : ControllerBase
    {

        private readonly RouteService _routeService;
        private readonly AgencyService _agencyService;
        private readonly TripService _tripService;
        private readonly StopService _stopService;
        private readonly StopTimeService _stopTimeService;

        private readonly string _agencyName;
        private readonly string _sourceName;
        private readonly int _agencyId;
        
        public SEPTAController(RouteService routeService, AgencyService agencyService, StopService stopService, TripService tripService, StopTimeService stopTimeService)
        {
            _routeService = routeService;
            _agencyService = agencyService;
            _stopService = stopService;
            _tripService = tripService;
            _stopTimeService = stopTimeService;
            
            _agencyName = "SEPTA";
            _sourceName = "SEPTA";
            
            _agencyId = _agencyService.GetAgencyIdByNameAndSource(_agencyName, _sourceName);
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
            var routes = await _routeService.GetRoutesByAgencyName(_agencyName);

            return Ok(new { Routes = routes });
        }

        [HttpGet("routes/{id}")]
        public async Task<ActionResult<Route>> GetRouteById(int id)
        {
            var route = await _routeService.GetRouteById(id);

            return Ok(new { Route = route });
        }
        
        [HttpGet("routes/regional-rail")]
        public async Task<ActionResult<List<Route>>> GetRegionalRailRoutes()
        {
            var routes = await _routeService.GetRoutesByAgencyAndType(_agencyName, 2);

            return Ok(new { Routes = routes });
        }
        
        [HttpGet("routes/bus")]
        public async Task<ActionResult<List<Route>>> GetBusRoutes()
        {
            var routes = await _routeService.GetRoutesByAgencyAndType(_agencyName, 3);

            return Ok(new { Routes = routes });
        }

        [HttpPost("routes/add")]
        public async void AddRoute(string routeId, string routeShortName, string routeLongName, int type, string color, string textColor, string url)
        {
            var agency =  await _agencyService.GetAgencyById(_agencyId);
            
            _routeService.AddRoute(agency, routeId, routeShortName, routeLongName, type, color, textColor, url);
        }

        [HttpGet("stops/{agencyId}/{routeId}")]
        public async Task<ActionResult<List<Stop>>> GetStopsByRoute(string agencyName, string routeId)
        {
            List<int> tripIds = await _tripService.GetTripIdsByRoute(agencyName, routeId);

            List<Stop> stops = await _stopService.GetStopsByTripIds(tripIds);

            return Ok(new { Stops = stops });
        }

        [HttpGet("stops/{routeType}")]
        public async Task<ActionResult<List<Stop>>> GetStopsByRouteType(int routeType)
        {
            List<Route> routes = await _routeService.GetRoutesByAgencyAndType(_agencyName, routeType);

            List<Trip> trips = await _tripService.GetTripsFromRouteList(routes, _agencyName);

            List<StopTime> stopTimes = await _stopTimeService.GetStopTimesFromTripList(trips);

            List<Stop> stops = await _stopService.GetStopsFromStopTimes(stopTimes);

            return Ok(new { Stops = stops });
        }
        /**
        [HttpGet("stops/all")]
        public async Task<ActionResult<List<Stop>>> GetAllStops()
        {
            List<Stop> stops = await _stopService.GetStopsByDataSource(_sourceName);

            return Ok(new { Stops = stops });
        }
    **/

    }
}
