
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Route = GtfsApi.Models.Route;


namespace GtfsApi.Controllers
{
    [Route("api/septa")]
    [ApiController]
    public class SEPTAController(
        IRouteService routeService,
        IAgencyService agencyService,
        IStopService stopService,
        IFareService fareService,
        IFeedInfoService feedInfoService)
        : BaseAgencyController(agencyService, routeService, stopService)
    {
        protected override string AgencyId => "SEPTA";
        protected override string ParentAgency => "SEPTA";

        [HttpGet("RssFeedInfo")]
        public async Task<ActionResult<List<FeedInfo>>> GetFeedInformation()
        {
            var feedInfo = await feedInfoService.GetFeedInfo(AgencyId);
            
            return Ok(new { FeedInfo = feedInfo });
        }
        
        [HttpGet("regional-rail/Fare")]
        public async Task<ActionResult<Fare>> GetFare(string origin, string destination)
        {
            Fare fare = await fareService.GetFare(origin, destination);

            return Ok(new { Fare = fare });
        }

        [HttpGet("regional-rail/FarePrice")]
        public async Task<ActionResult<float>> GetFarePrice(string origin, string destination)
        {
            Fare fare = await fareService.GetFare(origin, destination);

            float price = await fareService.GetFarePrice(fare);

            return Ok(new { Price = price });
        }
        
        [HttpGet("Routes/rail")]
        public async Task<IActionResult> GetAgencyRoutes()
        {
            List<Route> routes =  await _routeService.GetAgencyRoutesAsync(AgencyId);

            List<Route> railRoutes = routes.Where(rt => rt.Type == 2)
                .Select(rt => rt)
                .ToList();
            
            return Ok(new { Routes = railRoutes });
        }
        
        [HttpGet("Routes/bus")]
        public async Task<IActionResult> GetAgencyRailRoutes()
        {
            List<Route> routes =  await _routeService.GetAgencyRoutesAsync(AgencyId);
            
            return Ok(new { Routes = routes });
        }  
        
    }
}

