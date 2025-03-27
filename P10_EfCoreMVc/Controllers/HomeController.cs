
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Middlewares;
using WebApplication1.Models.ViewModel;



namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly ILogger<HomeController> logger;
          // logger.LogError(ex, "Error in HomeController Index method.");
        private readonly HybridViewModel viewModel;


        public HomeController(SqlDbContext dbContext, ITokenService tokenService, ILogger<HomeController> logger )
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { IsLoggedin = false },    // hardcoded values 
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Shop()
        {
            try
            {
                var products = await dbContext.Products.Where(p => p.IsDeleted == false).ToListAsync();   // array conversion 
                viewModel.Products = products;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                console.log(ex.Message , "Error in HomeController Shop method" );
                ViewBag.errorMessage = "Oops Some Error kindly try after Some time !";
                return View("Error" , viewModel);
            }
        }

        [HttpGet]
        public IActionResult About()
        {
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Services()
        {
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Blog()
        {
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View(viewModel);
        }
    }

 
}
