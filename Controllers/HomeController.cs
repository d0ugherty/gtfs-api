using Microsoft.AspNetCore.Mvc;

namespace GtfsApi.Controllers;

public class HomeController : Controller
{
	// GET
	public IActionResult Index()
	{
		return View();
	}
}