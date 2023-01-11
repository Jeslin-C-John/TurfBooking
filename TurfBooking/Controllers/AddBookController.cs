using Microsoft.AspNetCore.Mvc;
using TurfBooking.Models;

namespace TurfBooking.Controllers
{
    public class AddBookController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BookingModel e)
        {
            return View();
        }
    }
}
