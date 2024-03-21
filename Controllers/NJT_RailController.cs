using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

public class NJT_RailController : Controller
{
	// GET
	public IActionResult Index()
	{
		return View();
	}
}