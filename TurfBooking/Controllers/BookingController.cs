using Microsoft.AspNetCore.Mvc;
using TurfBooking.Data;
using TurfBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TurfBooking.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public IActionResult Index(BookingModel Instance)
        {
            ViewBag.Name = HttpContext.Session.GetString("Name");

            BookingContext Context = new BookingContext();
            ViewBag.BookingDate = HttpContext.Session.GetString("BookingDate");

            DateTime.TryParse(HttpContext.Session.GetString("BookingDate"), out DateTime ParseDateTime);

            //var FillSlot = Context.Bookings
            //.FromSql($"SELECT * FROM [Bookings] WHERE BookingDate LIKE {TempData["LongBookingDate"]}")
            //.ToList();

            var FillSlot = Context.Bookings
            .Where(s => s.BookingDate == ParseDateTime)
            .ToList();


            GroundSlotsModel SlotObj = new GroundSlotsModel();

            for (int i=0; i < FillSlot.Count; i++)
            {
                int GroundNo = FillSlot[i].Ground;
                int SlotNo= FillSlot[i].Slot;

                SlotObj.GroundSlotsArr[ GroundNo,SlotNo] = true;
                
            }

            var SlotList = new List<SlotList>();

            
                for (int j = 0; j < 18; j++)
                {
                    var SlotListObj=new SlotList();

                    SlotListObj.Time = j + 6;

                if (SlotObj.GroundSlotsArr[0, j] == false)
                    SlotListObj.Ground1 = "Book Now!";
                else SlotListObj.Ground1 = "Slot Filled";

                if (SlotObj.GroundSlotsArr[1, j] == false)
                    SlotListObj.Ground2 = "Book Now!";
                else SlotListObj.Ground2 = "Slot Filled";

                if (SlotObj.GroundSlotsArr[2, j] == false)
                    SlotListObj.Ground3 = "Book Now!";
                else SlotListObj.Ground3 = "Slot Filled";

                SlotList.Add(SlotListObj);
                }
            

            return View(SlotList);
        }

     
    }
}
