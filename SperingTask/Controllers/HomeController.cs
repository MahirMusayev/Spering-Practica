using Microsoft.AspNetCore.Mvc;

namespace SperingTask.Controllers
{
	public class HomeController : Controller
	{

		public IActionResult Index()
		{
			return View();
		}
	}
}
