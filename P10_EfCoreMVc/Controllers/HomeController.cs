using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
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
                Navbar = new NavbarModel { IsLoggedin = false },    // hardcoded values 
                Products = [],
                // Error =  new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
            };
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(viewModel);
        }

        // public IActionResult Error()
        // {
   
        //     return View(viewModel);
        // }




        [HttpGet]
        public async Task<IActionResult> Shop()
        {
            try
            {
                var products = await dbContext.Products.Where(p => p.IsDeleted == false).ToListAsync();
                viewModel.Products = products;
                return View(viewModel);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error in HomeController Index method.");
                return View("Error");
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




        public IActionResult Privacy()
        {
            return View(viewModel);
        }
    }
}
