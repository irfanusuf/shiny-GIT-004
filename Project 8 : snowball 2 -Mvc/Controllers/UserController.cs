using Microsoft.AspNetCore.Mvc;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController(ISqlService sqlService) : Controller
    {
        private readonly ISqlService _sqlService = sqlService;
        // ?? throw new ArgumentNullException(nameof(sqlService));

        [HttpGet]
        public IActionResult Index(int id)
        {

            var user = _sqlService.FindUserById(id);     // db query 
            ViewBag.Username = user.Username;
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.ErrorMessage = "Please provide valid data.";
                return View();
            }
            try
            {
                var findUser = _sqlService.FindUserByEmail(user.Email);


                if (findUser != null)
                {
                    ViewBag.ErrorMessage = "User Already Exists";
                    return View();
                }

                var result = _sqlService.CreateUser(user);

                if (result == "done")
                {
                    ViewBag.SuccessMessage = "Registration successful!";
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.ErrorMessage = "Registration failed. Please try again.";
                    return View();
                }
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = $"An error occurred: {ex.Message}";
                return View();
            }
        }
    }
}
