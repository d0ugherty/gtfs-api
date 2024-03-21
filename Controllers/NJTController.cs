using GtfsApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

	[Route("api/njt")]
	[ApiController]
	public class NJTController: BaseAgencyController
	{
		protected override string AgencyId => "NJT";

		public NJTController(IRouteService routeService, IAgencyService agencyService, IStopService stopService) 
			: base(agencyService, routeService, stopService)
		{
			
		}
	}