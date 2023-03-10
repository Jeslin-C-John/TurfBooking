using Microsoft.AspNetCore.Mvc;
using TurfBooking.Data;
using TurfBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.Configuration;

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


            var MonsoonStart = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SeasonSettings")["MonsoonStart"];
            DateTime.TryParse(MonsoonStart, out DateTime ParseMonsoonStart);
            var MonsoonEnd = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("SeasonSettings")["MonsoonEnd"];
            DateTime.TryParse(MonsoonEnd, out DateTime ParseMonsoonEnd);


            



            GroundSlotsModel SlotObj = new GroundSlotsModel();


            if (ParseMonsoonStart <= ParseDateTime && ParseMonsoonEnd >= ParseDateTime)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 6; j++)
                    {
                        SlotObj.GroundSlotsArr[i,j] = true;
                    }
                }
            }

            if(ParseDateTime==DateTime.Now.Date) {

                int NowHour = DateTime.Now.Hour;

                for(int k=0;k<3;k++)
                    for(int l=0;l<NowHour-5;l++)
                        SlotObj.GroundSlotsArr[k, l] = true;


            }




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

                if(SlotListObj.Time > 12)
                {
                    int IntTime = SlotListObj.Time - 12;
                    SlotListObj.StringTime=IntTime.ToString() + ":00 PM";
                }
                else if (SlotListObj.Time == 12)
                {
                    int IntTime = SlotListObj.Time;
                    SlotListObj.StringTime = SlotListObj.Time.ToString() + ":00 PM";
                }
                else
                {
                    int IntTime = SlotListObj.Time;
                    SlotListObj.StringTime = IntTime.ToString() + ":00 AM";
                }

                if (SlotObj.GroundSlotsArr[0, j] == false)
                    SlotListObj.Ground1 = "Book Now!";
                else SlotListObj.Ground1 = "Slot Unavailable";

                if (SlotObj.GroundSlotsArr[1, j] == false)
                    SlotListObj.Ground2 = "Book Now!";
                else SlotListObj.Ground2 = "Slot Unavailable";

                if (SlotObj.GroundSlotsArr[2, j] == false)
                    SlotListObj.Ground3 = "Book Now!";
                else SlotListObj.Ground3 = "Slot Unavailable";


                


                SlotList.Add(SlotListObj);
                }
            



            return View(SlotList);
        }

     
    }
}
