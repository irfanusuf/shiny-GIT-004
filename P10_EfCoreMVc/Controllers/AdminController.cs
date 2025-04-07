using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models.ViewModel;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {


        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly HybridViewModel viewModel;


        public AdminController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { UserRole = Role.Admin, IsLoggedin = false },
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
        public ActionResult Index()
        {
            return View();
        }

    }
}
