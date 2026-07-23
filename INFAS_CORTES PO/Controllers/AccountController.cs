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
            string[] f = ["fullname" , "email" , "username" ,"password" , "confirmPassword", "Age", "ContactNumber" ];
            //string[] d = ["John", "John@gmail.com", "User1", "123", "123", "23", "12345678809"];
            string[] d = [fullname, email, username, password, confirmPassword, "23", "12345678809"];
            string sql = user._sql(f, d, "User");
            return Content(sql);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
