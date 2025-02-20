using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models.ViewModel;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(SqlDbContext dbContext, ITokenService tokenService, ILogger<HomeController> logger)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            try
            {
                var token = Request.Cookies["AuthToken"];
                var viewModel = new HybridViewModel();

                if (string.IsNullOrEmpty(token))
                {

                    viewModel.Navbar.IsLoggedin = false;
                    return View(viewModel);
                }

                var userId = tokenService.VerifyTokenAndGetId(token);
             
                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId.ToString() == userId);
                

                if (user != null)
                {
                    viewModel.Navbar.UserRole = user.Role;
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
