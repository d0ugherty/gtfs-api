using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;


namespace GtfsApi.Controllers
{
    [Route("api/septa-bus")]
    [ApiController]
    public class SEPTA_BusController: BaseAgencyController
    {
        private readonly IFeedInfoService _feedInfoService;
        protected override string AgencyId => "1";

        public SEPTA_BusController( IRouteService routeService, IAgencyService agencyService, IStopService stopService, IFeedInfoService feedInfoService) 
            : base(agencyService, routeService, stopService)
        {
            _feedInfoService = feedInfoService;
        }
        
        [HttpGet("RssFeedInfo")]
        public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
        {
            var feedInfo = await _feedInfoService.GetFeedInfo(AgencyId);
            
            return Ok(new { FeedInfo = feedInfo });
        }

    }
}