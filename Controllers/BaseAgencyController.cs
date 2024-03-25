using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Controllers;

public abstract class BaseAgencyController : ControllerBase
{

	protected readonly IAgencyService _agencyService;
	protected readonly IRouteService _routeService;
	protected readonly IStopService _stopService;

	protected abstract string AgencyId { get; }
	protected abstract string ParentAgency { get; }

	protected BaseAgencyController(IAgencyService agencyService, IRouteService routeService, IStopService stopService)
	{
		_agencyService = agencyService;
		_routeService = routeService;
		_stopService = stopService;
	}

	[HttpGet("Agencies")]
	public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
	{
		List<Agency> agencies = await _agencyService.GetAllAgencies(ParentAgency);
            
		return  Ok(new { Agencies = agencies });
	}
	
	[HttpGet("AgencyInfo")]
	public async Task<ActionResult<Agency>> GetAgency()
	{ 
		Agency agency = await _agencyService.GetAgencyByGtfsId(AgencyId);
            
		return Ok(new { Agency = agency });
	}
	
	
	[HttpGet("Trips")]
	public async Task<ActionResult<IEnumerable<Trip>>> GetRouteTrips(string gtfsRouteId, int results=10)
	{
		Route route = await _routeService.GetRouteAsync(AgencyId, gtfsRouteId);

		int routeId = route.Id;

		List<Trip> trips = await _routeService.GetRouteTripsAsync(routeId);

		return Ok(new { Trips = trips });
	}
	
	[HttpGet("Stops")]
	public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops()
	{
            
		List<Route> routes = await _routeService.GetAgencyRoutesAsync(AgencyId);
            
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

	[HttpGet("routes")]
	public async Task<ActionResult<IEnumerable<Route>>> GetAllRoutes()
	{
		List<Route> routes = await _routeService.GetAgencyRoutesAsync(AgencyId);
		
		return Ok(new { Routes = routes });
	}

	[HttpGet("stops/{routeId}")]
	public async Task<ActionResult<IEnumerable<Stop>>> GetRouteStops(string routeId)
	{
		List<Stop> stops = await _routeService.GetRouteStops(AgencyId, routeId);
		
		
		return Ok(new { Stops = stops });
	}
	
	
}
