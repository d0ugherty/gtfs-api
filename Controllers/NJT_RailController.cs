using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

[Route("api/nj-transit-rail")]
[ApiController]
public class NJT_RailController(
    IRouteService routeService,
    IAgencyService agencyService,
    IStopService stopService)
    : BaseAgencyController(agencyService, routeService, stopService)
{
    protected override string AgencyId => "NJT";
}