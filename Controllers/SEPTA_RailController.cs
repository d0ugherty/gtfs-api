
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;


namespace GtfsApi.Controllers
{
    [Route("api/septa-rail")]
    [ApiController]
    public class SEPTA_RailController(
        IRouteService routeService,
        IAgencyService agencyService,
        IStopService stopService,
        IFareService fareService,
        IFeedInfoService feedInfoService)
        : BaseAgencyController(agencyService, routeService, stopService)
    {
        protected override string AgencyId => "SEPTA";

        [HttpGet("Fare")]
        public async Task<ActionResult<Fare>> GetFare(string origin, string destination)
        {
            Fare fare = await fareService.GetFare(origin, destination);

            return Ok(new { Fare = fare });
        }

        [HttpGet("FarePrice")]
        public async Task<ActionResult<float>> GetFarePrice(string origin, string destination)
        {
            Fare fare = await fareService.GetFare(origin, destination);

            float price = await fareService.GetFarePrice(fare);

            return Ok(new { Price = price });
        }

        [HttpGet("RssFeedInfo")]
        public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
        {
            var feedInfo = await feedInfoService.GetFeedInfo(AgencyId);
            
            return Ok(new { FeedInfo = feedInfo });
        }
        
    }
}

