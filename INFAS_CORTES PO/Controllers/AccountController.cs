using INFAS_CORTES_PO.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFAS_CORTES_PO.Controllers
{
    
    public class AccountController : Controller
    {
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("User") != null)
            {
                ViewBag.AlreadyLoggedIn = true;
                ViewBag.FullName = HttpContext.Session.GetString("FullName");
            }

            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Restrictions
            if (string.IsNullOrWhiteSpace(username))
            {
                ViewBag.Error = "Username is required.";
                return View();
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                ViewBag.Error = "Password is required.";
                return View();
            }

            var user = FakeDB.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);

            if (user != null)
            {
                HttpContext.Session.SetString("User", user.Username);
                HttpContext.Session.SetString("FullName", user.FullName);

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "Invalid Username or Password.";
            return View();
        }


        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(string fullname, string email, string username, string password, string confirmPassword)
        {
            if (string.IsNullOrEmpty(fullname) || string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ViewBag.Error = "All fields are required.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Passwords do not match.";
                return View();
            }

            if (FakeDB.Users.Any(u => u.Username == username))
            {
                ViewBag.Error = "Username already exists.";
                return View();
            }

            var newUser = new User
            {
                FullName = fullname,
                Email = email,
                Username = username,
                Password = password
            };

            FakeDB.Users.Add(newUser);

            ViewBag.Success = true;
            ViewBag.FullName = newUser.FullName;
            ViewBag.Email = newUser.Email;
            ViewBag.Username = newUser.Username;

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
