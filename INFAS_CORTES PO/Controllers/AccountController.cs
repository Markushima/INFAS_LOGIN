using INFAS_CORTES_PO.Models;

using Microsoft.AspNetCore.Mvc;


namespace INFAS_CORTES_PO.Controllers
{
   
    public class AccountController : Controller
    {
        User user = new User();
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
            string[] f =
            {
                "fullname", "email", "username", "password", "confirmPassword"
            };

            string[] d =
            {
               fullname, email, username, password, confirmPassword
            };

            string sql = user._sql(f, d, "User");

            FakeDB.Users.Add(new User
            {
                FullName = fullname,
                Email = email,
                Username = username,
                Password = password
            });

            return Json(new
            {
                success = true,
                message = "Registration Successful!",
                //sql = sql,
                users = user.ViewAll("Product")
            });
        }

        [HttpGet]
        public IActionResult ViewAll(string table)
        {
            return Json(user.ViewAll(table));
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
