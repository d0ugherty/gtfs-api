
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
        : BaseAgencyController(agencyService, routeService, stopService, feedInfoService)
    {
        protected override string AgencyId => "SEPTA";
        protected override string ParentAgency => "SEPTA";
        
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
    
        [HttpGet("routes/light-rail")]
        public async Task<IActionResult> GetAgencyLightRailRoutes()
        {
            List<Route> routes =  await RouteService.GetRoutesByTypeAsync(AgencyId, 0);
            
            return Ok(new { Routes = routes });
        }
    }
}

