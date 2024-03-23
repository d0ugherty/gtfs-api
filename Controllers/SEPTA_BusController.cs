using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;


namespace GtfsApi.Controllers
{
    [Route("api/septa-bus")]
    [ApiController]
    public class SEPTA_BusController(
        IRouteService routeService,
        IAgencyService agencyService,
        IStopService stopService,
        IFeedInfoService feedInfoService)
        : BaseAgencyController(agencyService, routeService, stopService)
    {
        protected override string AgencyId => "1";

        [HttpGet("RssFeedInfo")]
        public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
        {
            var feedInfo = await feedInfoService.GetFeedInfo(AgencyId);
            
            return Ok(new { FeedInfo = feedInfo });
        }

    }
}