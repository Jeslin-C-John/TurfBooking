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

            //var BookHistory = Context.Bookings
            //.FromSql($"SELECT * FROM [Bookings] WHERE UserId = {HttpContext.Session.GetInt32("Id")} ORDER BY Id DESC")
            //.ToList();

            var BookHistory = Context.Bookings
            .Where(s => s.UserId == HttpContext.Session.GetInt32("Id"))
            .OrderByDescending(s => s.BookingDate)
            .ToList();



            var BookHistoryList = new List<BookingModel>();
            

            for(int i=0; i < BookHistory.Count; i++)
            {
                BookingModel BookHistoryObj= new BookingModel();
                string? LongDate = BookHistory[i].BookingDate.ToString();
                
                string? ShortDate=LongDate.Substring(0, 10);

                DateOnly DateOnlyShort;

                DateOnly.TryParse(ShortDate, out DateOnlyShort);

                BookHistoryObj.ShortBookingDate = DateOnlyShort;

                BookHistoryObj.Id = BookHistory[i].Id;

                BookHistoryObj.BookingDate = BookHistory[i].BookingDate;
                BookHistoryObj.Ground = BookHistory[i].Ground + 1;
                BookHistoryObj.Slot= BookHistory[i].Slot + 6;
                if (BookHistoryObj.Slot > 12)
                {
                    var AMPMSlot = BookHistoryObj.Slot - 12;
                    BookHistoryObj.SlotPM = AMPMSlot + ":00 PM";
                }
                else if (BookHistoryObj.Slot == 12)
                {
                    var AMPMSlot = BookHistoryObj.Slot;
                    BookHistoryObj.SlotPM = AMPMSlot + ":00 PM";
                }
                else
                {
                    var AMPMSlot = BookHistoryObj.Slot;
                    BookHistoryObj.SlotPM = AMPMSlot + ":00 AM";
                }
                if(BookHistoryObj.BookingDate == DateTime.Now.Date && BookHistoryObj.Slot > DateTime.Now.Hour)
                    BookHistoryList.Add(BookHistoryObj);
                else if (BookHistoryObj.BookingDate > DateTime.Now.Date)
                    BookHistoryList.Add(BookHistoryObj);
            }


            return View(BookHistoryList);
        }


        [HttpGet]
        public IActionResult DateForm()
        {

            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Id = HttpContext.Session.GetInt32("Id");

            


            return View();
        }


        [HttpPost]
        public ActionResult DateForm(BookingModel Instance)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");

            TempData["BookingDate"] = Instance.ShortBookingDate.ToString();
            HttpContext.Session.SetString("BookingDate", Instance.ShortBookingDate.ToString());


            string ShortBookingString = Instance.ShortBookingDate.ToString("yyyy-MM-dd");
            string LongBookingString = ShortBookingString + " 00:00:00";

            TempData["LongBookingDate"] = LongBookingString;


            return RedirectToAction("Index", "Booking", new { area = "" });
        }
    }
}
