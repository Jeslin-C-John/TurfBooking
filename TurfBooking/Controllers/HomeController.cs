using Microsoft.AspNetCore.Mvc;

namespace TurfBooking.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = TempData["Name"];
            ViewBag.Id = TempData["Id"];
            return View();
        }
    }
}
