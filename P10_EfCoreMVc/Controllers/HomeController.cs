using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models.ViewModel;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly ILogger<HomeController> logger;
        private readonly HybridViewModel viewModel;



        public HomeController(SqlDbContext dbContext, ITokenService tokenService, ILogger<HomeController> logger)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.logger = logger;
              this.viewModel = new HybridViewModel
            {

                Navbar = new NavbarModel(),   
                Products = []

            };

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    return View(viewModel);
                }

                var userId = tokenService.VerifyTokenAndGetId(token);
             
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId == userId);
             

                if (user != null)
                {
                    viewModel.Navbar.UserRole = user.Role;     // dynamic Role
                    viewModel.Navbar.IsLoggedin = true;   
                }

                return View(viewModel);
            }
            catch (Exception)
            {
                // _logger.LogError(ex, "Error in HomeController Index method.");
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
