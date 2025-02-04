using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly SqlDbContext _dbContext;

        public UserController(SqlDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public string ErrorMessage = "";
        public string SuccessMessage = "";


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View();
        }



        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                // var finduser = await _dbContext.Users.FindAsync(user.Email);    this method works with {PK}
                var findUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (findUser != null)
                {
                    ViewData["ErrorMessage"] = "User already exists.";
                    return View();
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                if (ModelState.IsValid)
                {
                    await _dbContext.Users.AddAsync(user);
                    await _dbContext.SaveChangesAsync();
                    ViewData["SuccessMessage"] = "User created successfully!";
                    return View();
                }
                else
                {
                    ViewData["ErrorMessage"] = "All feilds are required!";
                    return View();
                }
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: ";
                Console.WriteLine(ex.Message);
                return View();
            }
        }





        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                var findUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (findUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password))
                {
                    ViewData["ErrorMessage"] = "Invalid email or password.";
                    return View();
                }

                ViewData["SuccessMessage"] = "Login successful!";
                return RedirectToAction("Dashboard");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }


    }









}
