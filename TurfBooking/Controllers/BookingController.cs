using Microsoft.AspNetCore.Mvc;
using TurfBooking.Data;
using TurfBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace TurfBooking.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public IActionResult Index(BookingModel Instance)
        {

            BookingContext Context = new BookingContext();
            ViewBag.BookingDate = TempData["BookingDate"];

            var FillSlot = Context.Bookings
            .FromSql($"SELECT * FROM [Bookings] WHERE BookingDate LIKE {TempData["LongBookingDate"]}")
            .ToList();


            GroundSlotsModel SlotObj = new GroundSlotsModel();

            for (int i=0; i < FillSlot.Count; i++)
            {
                int GroundNo = FillSlot[i].Ground;
                int SlotNo= FillSlot[i].Slot;

                SlotObj.GroundSlotsArr[ GroundNo,SlotNo] = true;
                
            }

            return View();
        }

        //[HttpPost]
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
