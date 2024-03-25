using GtfsApi.Interfaces;
using GtfsApi.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

[Route("api/amtrak")]
[ApiController]
public class AmtrakController(
    IRouteService routeService,
    IAgencyService agencyService,
    IStopService stopService,
    IFeedInfoService feedInfoService) : BaseAgencyController(agencyService, routeService, stopService)
{
    protected override string AgencyId => "Amtrak";
    protected override string ParentAgency => "Amtrak";

    
    [HttpGet("RssFeedInfo")]
    public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
    {
        var feedInfo = await feedInfoService.GetFeedInfo(AgencyId);
            
        return Ok(new { FeedInfo = feedInfo });
    }
}