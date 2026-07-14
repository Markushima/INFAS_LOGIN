using System.Diagnostics;
using System.Linq;
using INFAS_CORTES_PO.Models;
using Microsoft.AspNetCore.Mvc;


namespace INFAS_CORTES_PO.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //public IActionResult Index()
        //{
        //    if (HttpContext.Session.GetString("User") == null)
        //    {
        //        return RedirectToAction("Login", "Account");
        //    }

        //    return View();
        //}

        //public IActionResult StaticPage()
        //{
        //    return View("~/Views/Home/index.html");
        //    //if (HttpContext.Session.GetString("User") == null)
        //    //{
        //    //    return RedirectToAction("Login", "Account");
        //    //}

        //    //return View();
        //}

        public IActionResult Index()
        {
            var username = HttpContext.Session.GetString("User");

            if (username != null)
            {
                var user = FakeDB.Users.FirstOrDefault(u => u.Username == username);
                ViewBag.FullName = user?.FullName;
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
