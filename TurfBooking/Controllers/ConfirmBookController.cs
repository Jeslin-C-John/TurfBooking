using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using TurfBooking.Models;
using Microsoft.EntityFrameworkCore;
using TurfBooking.Data;

namespace TurfBooking.Controllers
{
    public class ConfirmBookController : Controller
    {
        [HttpGet]
        public IActionResult Index(int Time, int Ground, String Date)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");
            BookingModel Instance= new BookingModel();
            Instance.Slot = Time;
            Instance.Ground = Ground;

  

            DateTime.TryParse(Date, out DateTime ParseDateTime);
            DateOnly.TryParse(Date, out DateOnly ParseDateOnly);


            Instance.BookingDate = ParseDateTime;
            Instance.ShortBookingDate = ParseDateOnly;

            Instance.SlotPM = Time + " PM";



            return View(Instance);
        }

        [HttpPost]
        public IActionResult Index(BookingModel e)
        {

            ViewBag.Name = HttpContext.Session.GetString("Name");
            int? UserId = HttpContext.Session.GetInt32("Id");
            BookingContext Context = new BookingContext();
           BookingModel Instance = new BookingModel()
            {
                UserId = UserId,
                BookingDate = e.BookingDate,
                Ground = e.Ground-1,
                Slot = e.Slot-6
            };
            Context.Add(Instance);
            Context.SaveChanges();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
