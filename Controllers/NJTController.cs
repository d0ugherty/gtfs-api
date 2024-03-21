using GtfsApi.Interfaces;
using GtfsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Route = GtfsApi.Models.Route;

namespace GtfsApi.Controllers;

	[Route("api/njt")]
	[ApiController]
	public class NJTController: BaseAgencyController
	{
		private readonly IRouteService _routeService;
		private readonly IAgencyService _agencyService;
		private readonly IStopService _stopService;

		protected override string AgencyId => "NJT";

		public NJTController(IRouteService routeService, IAgencyService agencyService, IStopService stopService) 
			: base(agencyService, routeService, stopService)
		{
			_routeService = routeService;
			_agencyService = agencyService;
			_stopService = stopService;
		}
	}