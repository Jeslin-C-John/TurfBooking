using Microsoft.AspNetCore.Mvc;
using System.Text;
using TurfBooking.Data;
using TurfBooking.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

            

            var User = Context.Users
            .FromSql($"SELECT * FROM [Users] WHERE Email = {e.Email} AND EncryptPass = {e.EncryptPass}")
            .ToList();

            

            if (User.Count == 1)
            {
                TempData["Id"] = User[0].Id;
                TempData["Name"] = User[0].Name;
                return RedirectToAction("Index", "Home", new { area = "" });
            }

            return View();
        }
    }
}
