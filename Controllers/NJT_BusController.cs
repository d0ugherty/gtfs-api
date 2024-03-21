using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

	[Route("api/njt")]
	[ApiController]
	public class NJT_BusController: BaseAgencyController
	{
		protected override string AgencyId => "NJT";

		public NJT_BusController(IRouteService routeService, IAgencyService agencyService, IStopService stopService) 
			: base(agencyService, routeService, stopService)
		{
			
		}
	}