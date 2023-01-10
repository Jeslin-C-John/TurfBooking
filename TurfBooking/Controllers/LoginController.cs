using Microsoft.AspNetCore.Mvc;
using System.Text;
using TurfBooking.Data;
using TurfBooking.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace TurfBooking.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Index(UserModel e)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(e.Password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                e.EncryptPass = builder.ToString();
            }

            UserContext Context = new UserContext();
            if (Context.Users.Any(o => o.Name == e.Name) && Context.Users.Any(o => o.EncryptPass == e.EncryptPass))
            {
                
            
            return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }
    }
}
