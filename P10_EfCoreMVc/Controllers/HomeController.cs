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

            // return View();
            try
            {
                var token = Request.Cookies["AuthToken"];
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login" , "User");
                }

                var userId = tokenService.VerifyTokenAndGetId(token);
                if (string.IsNullOrEmpty(userId))
                {    
                    return RedirectToAction("Login", "User");
                }

                var user = await dbContext.Users.SingleOrDefaultAsync(u => u.UserId.ToString() == userId);
                var viewModel = new NavbarModel();

                if (user != null)
                {
                    viewModel.UserRole = user.Role;
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
