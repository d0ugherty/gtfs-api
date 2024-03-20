using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Controllers;

	[Route("api/njt")]
	[ApiController]
	public class NJTController: ControllerBase
	{
		private readonly IRouteService _routeService;
		private readonly IAgencyService _agencyService;
		private readonly IStopService _stopService;

		public NJTController(IRouteService routeService, IAgencyService agencyService, IStopService stopService)
		{
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
	        string agencyId = "NJT";
	        
            Agency agency = await _agencyService.GetAgencyByGtfsId(agencyId);
            
            return Ok(new { Agency = agency });
        }

        [HttpGet("Routes")]
        public async Task<IActionResult> GetAgencyRoutes()
        {
	        string agencyId = "NJT";

            List<Route> routes =  await _routeService.GetAgencyRoutesAsync(agencyId);
            
            return Ok(new { Routes = routes });
        }

        [HttpGet("Trips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAgencyTrips(string gtfsRouteId, int results=10)
        {
	        string agencyId = "NJT";

            Route route = await _routeService.GetRouteAsync(agencyId, gtfsRouteId);

            int routeId = route.Id;

            List<Trip> trips = await _routeService.GetRouteTripsAsync(routeId);

            return Ok(new { Trips = trips });
        }
        
        [HttpGet("Stops")]
        public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops()
        {
	        string agencyId = "NJT";

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
        
	}