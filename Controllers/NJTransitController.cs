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
    IStopService stopService)
    : BaseAgencyController(agencyService, routeService, stopService)
{
    protected override string AgencyId => "NJT";
    protected override string ParentAgency => "NJ Transit";
    
    [HttpGet("Routes/rail")]
    public async Task<IActionResult> GetAgencyRoutes()
    {
        List<Route> routes =  await _routeService.GetAgencyRoutesAsync(AgencyId, 2);
            
        return Ok(new { Routes = routes });
    }
        
    [HttpGet("Routes/bus")]
    public async Task<IActionResult> GetAgencyRailRoutes()
    {
        List<Route> routes =  await _routeService.GetAgencyRoutesAsync(AgencyId, 3);
            
        return Ok(new { Routes = routes });
    }
    
    [HttpGet("Routes/light-rail")]
    public async Task<IActionResult> GetAgencyLightRailRoutes()
    {
        List<Route> routes =  await _routeService.GetAgencyRoutesAsync(AgencyId, 0);
            
        return Ok(new { Routes = routes });
    }

}