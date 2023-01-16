using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System;
using TurfBooking.Models;
using Microsoft.EntityFrameworkCore;
using TurfBooking.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TurfBooking.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Id = HttpContext.Session.GetInt32("Id");

            BookingContext Context = new BookingContext();
            var BookHistory = Context.Bookings
            .FromSql($"SELECT * FROM [Bookings] WHERE UserId = {HttpContext.Session.GetInt32("Id")} ORDER BY Id DESC")
            .ToList();


            
            var BookHistoryList = new List<BookingModel>();
            

            for(int i=0; i < BookHistory.Count; i++)
            {
                BookingModel BookHistoryObj= new BookingModel();
                string? LongDate = BookHistory[i].BookingDate.ToString();
                DateOnly.TryParse(LongDate, out DateOnly ShortDate);
                BookHistoryObj.ShortBookingDate = ShortDate;
                BookHistoryObj.Ground = BookHistory[i].Ground + 1;
                BookHistoryObj.Slot= BookHistory[i].Slot + 1;
                BookHistoryObj.SlotPM = BookHistoryObj.Slot + " PM";
                BookHistoryList.Add(BookHistoryObj);
            }


            return View(BookHistoryList);
        }
        [HttpPost]
        public ActionResult Index(BookingModel Instance)
        {
            TempData["BookingDate"] = Instance.ShortBookingDate.ToString();
            

            string ShortBookingString = Instance.ShortBookingDate.ToString("yyyy-MM-dd");
            string LongBookingString = ShortBookingString + " 00:00:00";

            TempData["LongBookingDate"] = LongBookingString;


            return RedirectToAction("Index", "Booking", new { area = "" });
        }
    }
}
