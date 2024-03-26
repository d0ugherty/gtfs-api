
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Route = GtfsApi.Models.Route;


namespace GtfsApi.Controllers
{
    [Route("api/septa")]
    [ApiController]
    public class SEPTAController : BaseAgencyController
    {
        private readonly IFareService _fareService;

        public SEPTAController(IRouteService routeService, 
            IAgencyService agencyService, 
            IStopService stopService, 
            IFareService fareService, 
            IFeedInfoService feedInfoService) 
            : base(
                agencyService,
                routeService, 
                stopService, 
                feedInfoService)
        {
            _fareService = fareService;
        }

        protected override string AgencyId => "SEPTA";
        protected override string ParentAgency => "SEPTA";
        
        [HttpGet("FareInfo/regional-rail")]
        public async Task<ActionResult<Fare>> GetFare(string origin, string destination)
        {
            Fare fare = await _fareService.GetFare(origin, destination);

            return Ok(new { Fare = fare });
        }

        [HttpGet("FarePrice/regional-rail")]
        public async Task<ActionResult<float>> GetFarePrice(string origin, string destination)
        {
            Fare fare = await _fareService.GetFare(origin, destination);

            float price = await _fareService.GetFarePrice(fare);

            return Ok(new { Price = price });
        }

        [HttpGet("routes/regional-rail")]
        public async Task<ActionResult<IEnumerable<Route>>> GetRailRoutes()
        {
            List<Route> routes = await RouteService.GetRoutesByTypeAsync(AgencyId, 2);

            return Ok(new { Routes = routes });
        }
        
        [HttpGet("routes/bus")]
        public async Task<ActionResult<IEnumerable<Route>>> GetBusRoutes()
        {
            List<Route> routes = await RouteService.GetRoutesByTypeAsync(AgencyId, 3);

            return Ok(new { Routes = routes });
        }  
        
    }
}

