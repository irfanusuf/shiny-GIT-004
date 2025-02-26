using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;
using WebApplication1.Types;


namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly HybridViewModel viewModel;

        public UserController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { IsLoggedin = false },
                Products = []
            };
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return View(viewModel);
            }

            var userId = tokenService.VerifyTokenAndGetId(token);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user.Role == Role.Seller)
            {
                return RedirectToAction("SellerDashboard");
            }
            else if (user.Role == Role.Buyer)
            {
                return RedirectToAction("BuyerDashboard");
            }
             else if(user.Role == Role.Admin){
                return RedirectToAction("AdminDashboard");
            }
            else
            {
                return View(viewModel);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return View(viewModel);
            }

            var userId = tokenService.VerifyTokenAndGetId(token);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user.Role == Role.Seller)
            {
                return RedirectToAction("SellerDashboard");
            }
            else if (user.Role == Role.Buyer)
            {
                return RedirectToAction("BuyerDashboard");
            }
            else if(user.Role == Role.Admin){
                return RedirectToAction("AdminDashboard");
            }
            {
                return View(viewModel);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Register(User user)
        {
            try
            {
                // var finduser = await _dbContext.Users.FindAsync(user.Email);    this method works with {PK}

                if (!ModelState.IsValid)
                {
                    ViewData["ErrorMessage"] = "All feilds are required!";
                    return View(viewModel);
                }

                var findUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (findUser != null)
                {
                    ViewData["ErrorMessage"] = "User already exists.";
                    return View(viewModel);
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);


                await dbContext.Users.AddAsync(user);
                await dbContext.SaveChangesAsync();
                ViewData["SuccessMessage"] = "User created successfully!";
                return View(viewModel);

            }

            catch (Exception)
            {
                ViewData["ErrorMessage"] = "An error occurred: ";
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                //  if (!ModelState.IsValid)
                // {
                //     ViewData["ErrorMessage"] = "All feilds are required!";
                //     return View(viewModel);
                // }


                var findUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);

                if (findUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, findUser.Password))
                {

                    ViewData["ErrorMessage"] = "Invalid email or password.";
                    return View(viewModel);
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
                    return RedirectToAction("BuyerDashboard");
                }


            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = "An error occurred: " + ex.Message;
                return View();
            }
        }

        // authorizing the dashborads so that respective users can acces it 
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

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                if (user.Role == Role.Admin)
                {
                    viewModel.Navbar.UserRole = Role.Admin;
                    viewModel.Navbar.IsLoggedin = true;
                    return View(viewModel);
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
        public async Task<IActionResult> BuyerDashboard()
        {
            try
            {
                var token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login");
                }

                var userId = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                if (user.Role == Role.Buyer)
                {
                    viewModel.Navbar.UserRole = Role.Buyer;
                    viewModel.Navbar.IsLoggedin = true;
                    return View(viewModel);
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
                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user.Role == Role.Seller)
                {
                    viewModel.Navbar.UserRole = Role.Seller;
                    viewModel.Navbar.IsLoggedin = true;

                    return View(viewModel);
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
        public async Task<IActionResult> ChangeRoleToSeller()
        {
            var token = Request.Cookies["AuthToken"];
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return RedirectToAction("login");
            }

            if (user.Role == Role.Buyer)
            {
                user.Role = Role.Seller;
                await dbContext.SaveChangesAsync();
                return RedirectToAction("SellerDashboard");
            }

            return RedirectToAction("login");
        }

        [HttpGet]
        public async Task<IActionResult> ChangeRoleToBuyer()
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
            {
                return RedirectToAction("login");
            }

            if (user.Role == Role.Seller)
            {
                user.Role = Role.Buyer;
                await dbContext.SaveChangesAsync();
                return RedirectToAction("BuyerDashboard");
            }

            return RedirectToAction("login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("AuthToken");
            return RedirectToAction("index", "home");
        }
    }
}
