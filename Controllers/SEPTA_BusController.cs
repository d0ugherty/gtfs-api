using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

public class SEPTA_BusController : Controller
{
	// GET
	public IActionResult Index()
	{
		return View();
	}
}