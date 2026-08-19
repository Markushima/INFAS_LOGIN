using INFAS_CORTES_PO.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace INFAS_CORTES_PO.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly string _connectionString;

        public AccountController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _connectionString = configuration.GetConnectionString("ConnectionString")
                                ?? throw new InvalidOperationException("Connection string 'ConnectionString' not found.");
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

            if (password != confirmPassword)
            {
                return Json(new { success = false, message = "Passwords do not match." });
            }

            try
            {
                User user = new User
                {
                    FullName = fullname,
                    Email = email,
                    Username = username,
                    Password = password,
                    ConfirmPassword = confirmPassword
                };

                
                //string sql = user._sql(
                //    new string[] { "FullName", "Email", "Username", "Password", "ConfirmPassword" },
                //    new string[] { user.FullName, user.Email, user.Username, user.Password, user.ConfirmPassword },
                //    "Users"
                //);

                string[] fields = { "FullName", "Email", "Username", "Password", "ConfirmPassword" };
                string[] values = { user.FullName, user.Email, user.Username, user.Password, user.ConfirmPassword };

                string sqlParameterized = user._sql(fields, values, "Users");


                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sqlParameterized, connection))
                    {
                        command.Parameters.AddWithValue("@FullName", user.FullName ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Email", user.Email ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Username", user.Username ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@Password", user.Password ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@ConfirmPassword", user.ConfirmPassword ?? (object)DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                }

                return Json(new
                {
                    success = true,
                    message = "Registration Successful!",
                    sql = sqlParameterized
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.InnerException?.Message ?? ex.Message
                });
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

