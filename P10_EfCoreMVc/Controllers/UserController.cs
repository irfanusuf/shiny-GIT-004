using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        public UserController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }

        // public string ErrorMessage = "";
        // public string SuccessMessage = "";

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
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);
            var user = dbContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> AdminDashboard()
        {

            try
            {
                var token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login");
                }

                var userId = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

                if (user.Role == Role.Admin)
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            catch (Exception)
            {

                return View();
            }


        }


        [HttpGet]
        public async Task<IActionResult> SellerDashboard()
        {

            try
            {
                var token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login");
                }

                var userId = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

                if (user.Role == Role.Seller)
                {
                    return View(user);
                }
                else
                {
                    return RedirectToAction("login");
                }
            }
            catch (Exception)
            {

                return View();
            }


        }


        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                // var finduser = await _dbContext.Users.FindAsync(user.Email);    this method works with {PK}
                var findUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (findUser != null)
                {
                    ViewData["ErrorMessage"] = "User already exists.";
                    return View();
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                if (ModelState.IsValid)
                {
                    await dbContext.Users.AddAsync(user);
                    await dbContext.SaveChangesAsync();
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
                var findUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (findUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password))
                {


                    ViewData["ErrorMessage"] = "Invalid email or password.";
                    return View();
                }

                var token = tokenService.CreateToken(findUser.UserId.ToString(), findUser.Email, findUser.Username);

                HttpContext.Response.Cookies.Append(

                   "AuthToken",
                   token,
                   new CookieOptions
                   {
                       HttpOnly = true,
                       Secure = true,
                       SameSite = SameSiteMode.Strict,
                       Expires = DateTimeOffset.UtcNow.AddHours(24)
                   }
               );

                ViewData["SuccessMessage"] = "Login successful!";



                if (findUser.Role == Role.Admin)
                {

                    return RedirectToAction("AdminDashboard");
                }
                else if (findUser.Role == Role.Seller)
                {
                    return RedirectToAction("SellerDashboard");
                }
                else
                {
                    return RedirectToAction("Dashboard");
                }


            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }


        [HttpGet]

        public async Task<IActionResult> ChangeRoleToSeller()
        {

            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

            if (user.Role == Role.Buyer)
            {

                user.Role = Role.Seller;
                dbContext.Users.Update(user);
                return View(user);
            }

            return View();
        }



        [HttpGet]
        public IActionResult Logout()
        {



            HttpContext.Response.Cookies.Delete("AuthToken");
            return RedirectToAction("Login");

        }

    }









}
