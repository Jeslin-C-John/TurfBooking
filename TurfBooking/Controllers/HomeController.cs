using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using TurfBooking.Models;

namespace TurfBooking.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Id = HttpContext.Session.GetInt32("Id");

            return View();
        }
        [HttpPost]
        public ActionResult Index(BookingModel Instance)
        {
            TempData["BookingDate"] = Instance.ShortBookingDate.ToString();
            

            String ShortBookingString = Instance.ShortBookingDate.ToString("yyyy-MM-dd");
            String LongBookingString = ShortBookingString + " 00:00:00";

            TempData["LongBookingDate"] = LongBookingString;


            return RedirectToAction("Index", "Booking", new { area = "" });
        }
    }
}
