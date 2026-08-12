using INFAS_CORTES_PO.Models;
using Microsoft.AspNetCore.Mvc;

namespace INFAS_CORTES_PO.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

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
        public IActionResult Login(string username, string password)
        {
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

            var loggedInUser = FakeDB.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);

            if (loggedInUser != null)
            {
                HttpContext.Session.SetString("User", loggedInUser.Username);
                HttpContext.Session.SetString("FullName", loggedInUser.FullName);

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
            if (string.IsNullOrWhiteSpace(fullname))
            {
                return Json(new
                {
                    success = false,
                    message = "Full Name is required."
                });
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                return Json(new
                {
                    success = false,
                    message = "Email is required."
                });
            }

            if (string.IsNullOrWhiteSpace(username))
            {
                return Json(new
                {
                    success = false,
                    message = "Username is required."
                });
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return Json(new
                {
                    success = false,
                    message = "Password is required."
                });
            }

            if (password != confirmPassword)
            {
                return Json(new
                {
                    success = false,
                    message = "Passwords do not match."
                });
            }

            User newUser = new User
            {
                FullName = fullname,
                Email = email,
                Username = username,
                Password = password,
                ConfirmPassword = confirmPassword
            };

            _context.Users.Add(newUser);

            _context.SaveChanges();

            return Json(new
            {
                success = true,
                message = "Registration Successful!"
            });
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
