
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;


namespace GtfsApi.Controllers
{
    [Route("api/septa-rail")]
    [ApiController]
    public class SEPTA_RailController: BaseAgencyController
    {
        private readonly IFareService _fareService;
        private readonly IFeedInfoService _feedInfoService;
        protected override string AgencyId => "SEPTA";

        public SEPTA_RailController(
            IRouteService routeService, 
            IAgencyService agencyService, 
            IStopService stopService, 
            IFareService fareService,
            IFeedInfoService feedInfoService) 
            : base(agencyService, routeService, stopService)
        {
            _fareService = fareService;
            _feedInfoService = feedInfoService;
        }
        
        [HttpGet("Fare")]
        public async Task<ActionResult<Fare>> GetFare(string origin, string destination)
        {
            Fare fare = await _fareService.GetFare(origin, destination);

            return Ok(new { Fare = fare });
        }

        [HttpGet("FarePrice")]
        public async Task<ActionResult<float>> GetFarePrice(string origin, string destination)
        {
            Fare fare = await _fareService.GetFare(origin, destination);

            float price = await _fareService.GetFarePrice(fare);

            return Ok(new { Price = price });
        }

        [HttpGet("RssFeedInfo")]
        public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
        {
            var feedInfo = await _feedInfoService.GetFeedInfo(AgencyId);
            
            return Ok(new { FeedInfo = feedInfo });
        }
        
    }
}

