using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models.ViewModel;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{
    public class BuyerController : Controller
    {


        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly HybridViewModel viewModel;


        public BuyerController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel {UserRole = Role.Buyer , IsLoggedin = false },    // hardcoded values 
                Products = []
            };
        }



        [HttpGet]
        public async Task<IActionResult> Dashboard()
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
        public ActionResult Cart()
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            var userId = tokenService.VerifyTokenAndGetId(token);

            return View(viewModel);
        }
    }
}
