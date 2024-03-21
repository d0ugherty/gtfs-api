
using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;
using GtfsApi.Models;


namespace GtfsApi.Controllers
{
    [Route("api/septa")]
    [ApiController]
    public class SEPTAController: BaseAgencyController
    {
        private readonly IFareService _fareService;
        protected override string AgencyId => "SEPTA";

        public SEPTAController( IRouteService routeService, IAgencyService agencyService, IStopService stopService, IFareService fareService) 
            : base(agencyService, routeService, stopService)
        {
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

