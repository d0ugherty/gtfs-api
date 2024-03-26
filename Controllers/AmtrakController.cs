using System.Diagnostics;
using GtfsApi.Interfaces;
using GtfsApi.Migrations;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Controllers;

[Route("api/amtrak")]
[ApiController]
public class AmtrakController(
    IRouteService routeService,
    IAgencyService agencyService,
    IStopService stopService,
    IFeedInfoService feedInfoService) : BaseAgencyController(agencyService, routeService, stopService, feedInfoService)
{
    protected override string AgencyId => "Amtrak";
    protected override string ParentAgency => "Amtrak";
    
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

    [HttpGet("routes/amtrak-thruway-service")]
    public async Task<ActionResult<IEnumerable<Route>>> GetThruwayBus()
    {
        List<Route> routes = await RouteService.GetRoutesByTypeAsync(AgencyId, 3);

        List<Route> thruServiceRoutes = routes
            .Where(route =>
            {
                Debug.Assert(route.LongName != null, "route.LongName != null");
                return route.LongName.Equals("Amtrak Thruway Connecting Service");
            })
            .ToList();

        return Ok(new { Routes = thruServiceRoutes });
    }
}