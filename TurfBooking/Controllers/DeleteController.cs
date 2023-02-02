using Microsoft.AspNetCore.Mvc;
using System.Linq;
using TurfBooking.Data;
using TurfBooking.Models;

namespace TurfBooking.Controllers
{
    public class DeleteController : Controller
    {
        public IActionResult Index(int TaskId)
        {

            using (var Context = new BookingContext())
            {
                var Delete = Context.Bookings.Where(d => d.Id == TaskId).First();

                Context.Bookings.Remove(Delete);

                Context.SaveChanges();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
