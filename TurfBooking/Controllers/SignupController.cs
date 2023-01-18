using Microsoft.AspNetCore.Mvc;
using TurfBooking.Data;
using TurfBooking.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;

namespace TurfBooking.Controllers
{
    
    public class SignupController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        
        public IActionResult Index(UserModel e)
        {
            UserContext DupContext = new UserContext();
            var DupUser = DupContext.Users
           .Where(s => s.Email == e.Email)
           .ToList();

            if (DupUser.Count == 1)
            {


                ViewBag.Message = "User Already Exists!";



                return View();
            }




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
            UserModel Instance = new UserModel()
            {
                Name = e.Name,
                Email = e.Email,
                EncryptPass = e.EncryptPass,
            };
            Context.Add(Instance);
            Context.SaveChanges();
            return RedirectToAction("Index", "Login", new { area = "" });
        }
    }
}
