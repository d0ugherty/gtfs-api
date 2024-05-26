using Gtfs.DataAccess;
using Gtfs.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SEPTAController : ControllerBase
    {

        private readonly GtfsContext _context;
        private readonly RouteService _routeService;

        private string _agencyName;
        
        public SEPTAController(GtfsContext context, RouteService routeService)
        {
            _context = context;
            _routeService = routeService;
            _agencyName = "SEPTA";
        }

        [HttpGet("All-Routes")]
        public async Task<ActionResult<List<Route>>> GetAllRoutes()
        {
            var routes = await _routeService.GetRoutesByAgency(_agencyName);

            return Ok(new { Routes = routes });
        }
        
        [HttpGet("RR-Routes")]
        public async Task<ActionResult<List<Route>>> GetRegionalRailRoutes()
        {
            var routes = await _routeService.GetRoutesByAgencyAndType(_agencyName, 2);

            return Ok(new { Routes = routes });
        }
    }
}
