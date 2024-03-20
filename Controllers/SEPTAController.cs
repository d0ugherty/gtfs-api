
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Route = GtfsApi.Models.Route;


namespace GtfsApi.Controllers
{
    [Route("api/septa")]
    [ApiController]
    public class SEPTAController: ControllerBase
    {
        private readonly IFareService _fareService;
        private readonly IRouteService _routeService;
        private readonly IAgencyService _agencyService;
        private readonly IStopService _stopService;

        public SEPTAController(IFareService fareService, IRouteService routeService, IAgencyService agencyService, IStopService stopService)
        {
            _fareService = fareService;
            _routeService = routeService;
            _agencyService = agencyService;
            _stopService = stopService;
        }

        [HttpGet("Agencies")]
        public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
        {
            List<Agency> agencies = await _agencyService.GetAllAgencies();
            
            return  Ok(new { Agencies = agencies });
        }

        [HttpGet("AgencyInfo")]
        public async Task<ActionResult<Agency>> GetAgency()
        { 
            string agencyId = "SEPTA";
            
            Agency agency = await _agencyService.GetAgencyByGtfsId(agencyId);
            
            return Ok(new { Agency = agency });
        }

        [HttpGet("Routes")]
        public async Task<IActionResult> GetAgencyRoutes()
        {
            string agencyId = "SEPTA";
            
            List<Route> routes =  await _routeService.GetAgencyRoutesAsync(agencyId);
            
            return Ok(new { Routes = routes });
        }

        [HttpGet("Trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAgencyTrips( string gtfsRouteId, int results=10)
        {
            string agencyId = "SEPTA";
            
            Route route = await _routeService.GetRouteAsync(agencyId, gtfsRouteId);

            int routeId = route.Id;

            List<Trip> trips = await _routeService.GetRouteTripsAsync(routeId);

            return Ok(new { Trips = trips });
        }
        
        [HttpGet("Stops")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops()
        {
            string agencyId = "SEPTA";
            
            List<Route> routes = await _routeService.GetAgencyRoutesAsync(agencyId);
            
            List<int> routeIds = routes
                .Select(route => route.Id)
                .ToList();
           
            List<Trip> trips =  await _routeService.GetRouteTripsAsync(routeIds);

            List<int> stopIds = await _routeService.GetRouteStopIds(trips);
            
            List<Stop> stops = await _stopService.GetStopListAsync(stopIds);
            
            return Ok(new { Stops = stops });
        }

        [HttpGet("StopTimes")]
        public async Task<ActionResult<IEnumerable<StopTime>>> GetStopTimes(int id)
        {
            Stop stop = await _stopService.GetStopAsync(id);

            List<StopTime> stopTimes = await _stopService.GetStopTimesAsync(stop);

            return Ok(new { StopTimes = stopTimes });
        }
        
        [HttpGet("Fare")]
        public async Task<ActionResult<Fare>> GetFare(string origin, string destination)
        {
            Fare fare = await _fareService.GetFare(origin, destination);

            return Ok(new { Fare = fare });
        }

        [HttpGet("FarePrice")]
        public async Task<ActionResult<float>> GetFarePrice(string origin, string destination)
        {
            Fare fare = await _fareService.GetFare(origin, destination);

            float price = await _fareService.GetFarePrice(fare);

            return Ok(new { Price = price });
        }
        
    }
}

