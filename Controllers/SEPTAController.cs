
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;
using Microsoft.EntityFrameworkCore;
using Route = GtfsApi.Models.Route;


namespace GtfsApi.Controllers
{
    [Route("api/septa")]
    [ApiController]
    public class SEPTAController: BaseAgencyController
    {
        private readonly IFareService _fareService;
        private readonly IRouteService _routeService;
        private readonly IAgencyService _agencyService;
        private readonly IStopService _stopService;
        
        protected override string AgencyId => "SEPTA";

        public SEPTAController( IRouteService routeService, IAgencyService agencyService, IStopService stopService, IFareService fareService) 
            : base(agencyService, routeService, stopService)
        {
            _routeService = routeService;
            _agencyService = agencyService;
            _stopService = stopService;
            _fareService = fareService;
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
        
    }
}

