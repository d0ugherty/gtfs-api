using System.ComponentModel;
using Gtfs.DataAccess;
using Gtfs.Domain.Models;
using Gtfs.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Route = Microsoft.AspNetCore.Routing.Route;

namespace GtfsApi.Controllers
{
    [Route("gtfs/septa")]
    [ApiController]
    public class SEPTAController : ControllerBase
    {

        private readonly RouteService _routeService;
        private readonly AgencyService _agencyService;

        private readonly string _agencyName;
        private readonly string _sourceName;
        private readonly int _agencyId;
        
        public SEPTAController(RouteService routeService, AgencyService agencyService)
        {
            _routeService = routeService;
            _agencyService = agencyService;
            _agencyName = "SEPTA";
            _sourceName = "SEPTA";
            _agencyId = 3;
        }

        [HttpGet("agencies/all")]
        public async Task<ActionResult<List<Agency>>> GetAllAgencies()
        {
            var agencies = await _agencyService.GetAgenciesBySource(_sourceName);

            return Ok(new { Agencies = agencies });
        }
        
        [HttpGet("routes/all")]
        public async Task<ActionResult<List<Route>>> GetAllRoutes()
        {
            var routes = await _routeService.GetRoutesByAgency(_agencyName);

            return Ok(new { Routes = routes });
        }

        [HttpGet("routes/{id}")]
        public async Task<ActionResult<Route>> GetRouteById(int id)
        {
            var route = await _routeService.GetRouteById(id);

            return Ok(new { Route = route });
        }
        
        [HttpGet("routes/regional-rail")]
        public async Task<ActionResult<List<Route>>> GetRegionalRailRoutes()
        {
            var routes = await _routeService.GetRoutesByAgencyAndType(_agencyName, 2);

            return Ok(new { Routes = routes });
        }
        
        [HttpGet("routes/bus")]
        public async Task<ActionResult<List<Route>>> GetBusRoutes()
        {
            var routes = await _routeService.GetRoutesByAgencyAndType(_agencyName, 3);

            return Ok(new { Routes = routes });
        }

        [HttpPost("routes/add")]
        public async void AddRoute(string routeId, string routeShortName, string routeLongName, int type, string color, string textColor, string url)
        {
            var agency =  await _agencyService.GetAgencyById(_agencyId);
            
            _routeService.AddRoute(agency, routeId, routeShortName, routeLongName, type, color, textColor, url);
        }
    }
}
