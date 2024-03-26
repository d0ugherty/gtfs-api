using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Controllers;

[Route("api/nj-transit")]
[ApiController]
public class NJTransitController(
    IRouteService routeService,
    IAgencyService agencyService,
    IStopService stopService,
    IFeedInfoService feedInfoService)
    : BaseAgencyController(agencyService, routeService, stopService, feedInfoService)
{
    protected override string AgencyId => "NJT";
    protected override string ParentAgency => "NJ Transit";
    
    [HttpGet("routes/rail")]
    public async Task<IActionResult> GetAgencyRoutes()
    {
        List<Route> routes =  await RouteService.GetRoutesByTypeAsync(AgencyId, 2);
            
        return Ok(new { Routes = routes });
    }
        
    [HttpGet("routes/bus")]
    public async Task<IActionResult> GetAgencyRailRoutes()
    {
        List<Route> routes =  await RouteService.GetRoutesByTypeAsync(AgencyId, 3);
            
        return Ok(new { Routes = routes });
    }
    
    [HttpGet("stops/river-line")]
    public async Task<IActionResult> GetRiverLineStops()
    {
        List<Stop> stops = await RouteService.GetRouteStops(AgencyId, "16");
		
        return Ok(new { Stops = stops });
    }
    
    [HttpGet("stops/newark-light-rail")]
    public async Task<IActionResult> GetNewarkLightRailStops()
    {
        List<Stop> stops = await RouteService.GetRouteStops(AgencyId, "16");
		
        return Ok(new { Stops = stops });
    }

}