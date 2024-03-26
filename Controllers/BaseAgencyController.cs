using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Controllers;

public abstract class BaseAgencyController : ControllerBase
{

	protected readonly IAgencyService AgencyService;
	protected readonly IRouteService RouteService;
	protected readonly IStopService StopService;
	protected readonly IFeedInfoService FeedInfoService;

	protected abstract string AgencyId { get; }
	protected abstract string ParentAgency { get; }

	protected BaseAgencyController(IAgencyService agencyService, 
		IRouteService routeService, 
		IStopService stopService,
		IFeedInfoService feedInfoService)
	{
		AgencyService = agencyService;
		RouteService = routeService;
		StopService = stopService;
		FeedInfoService = feedInfoService;
	}
	
	[HttpGet("RssFeedInfo")]
	public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
	{
		var feedInfo = await FeedInfoService.GetFeedInfo(AgencyId);
            
		return Ok(new { FeedInfo = feedInfo });
	}
	
	[HttpGet("Agencies")]
	public async Task<ActionResult<IEnumerable<Agency>>> GetAgencies()
	{
		List<Agency> agencies = await AgencyService.GetAllAgencies(ParentAgency);
            
		return  Ok(new { Agencies = agencies });
	}
	
	[HttpGet("Trips")]
	public async Task<ActionResult<IEnumerable<Trip>>> GetRouteTrips(string gtfsRouteId, int results=10)
	{
		Route route = await RouteService.GetRouteAsync(AgencyId, gtfsRouteId);

		int routeId = route.Id;

		List<Trip> trips = await RouteService.GetRouteTripsAsync(routeId);

		return Ok(new { Trips = trips });
	}
	
	[HttpGet("Stops")]
	public async Task<ActionResult<IEnumerable<Stop>>> GetAgencyStops()
	{
            
		List<Route> routes = await RouteService.GetAgencyRoutesAsync(AgencyId);
            
		List<int> routeIds = routes
			.Select(route => route.Id)
			.ToList();
           
		List<Trip> trips =  await RouteService.GetRouteTripsAsync(routeIds);

		List<int> stopIds = await RouteService.GetRouteStopIds(trips);
            
		List<Stop> stops = await StopService.GetStopListAsync(stopIds);
            
		return Ok(new { Stops = stops });
	}
	
	[HttpGet("StopTimes")]
	public async Task<ActionResult<IEnumerable<StopTime>>> GetStopTimes(int stopId)
	{
		Stop stop = await StopService.GetStopAsync(stopId);

		List<StopTime> stopTimes = await StopService.GetStopTimesAsync(stop);

		return Ok(new { StopTimes = stopTimes });
	}

	[HttpGet("routes")]
	public async Task<ActionResult<IEnumerable<Route>>> GetAllRoutes()
	{
		List<Route> routes = await RouteService.GetAgencyRoutesAsync(AgencyId);
		
		return Ok(new { Routes = routes });
	}
	
	[HttpGet("route-stops")]
	public async Task<ActionResult<IEnumerable<Stop>>> GetRouteStops(string routeId)
	{
		List<Stop> stops = await RouteService.GetRouteStops(AgencyId, routeId);
		
		return Ok(new { Stops = stops });
	}

	[HttpGet("route-shapes")]
	public async Task<ActionResult<IEnumerable<Shape>>> GetShapes(string routeId, int pageNumber=1)
	{
		List<Shape> shapeData = await RouteService.GetRouteShapesAsync(AgencyId, routeId, pageNumber);
		
		return Ok(new { Shapes = shapeData });
	}
 	
	
}
