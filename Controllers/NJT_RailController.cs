using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

[Route("api/nj-transit-rail")]
[ApiController]
public class NJT_RailController: BaseAgencyController
{
    protected override string AgencyId => "NJT";

    public NJT_RailController(
        IRouteService routeService, 
        IAgencyService agencyService, 
        IStopService stopService) 
        : base(agencyService, routeService, stopService)
    {
			
    }
}