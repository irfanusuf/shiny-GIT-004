using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{

    [Authorize]
    public class SellerController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly HybridViewModel viewModel;

        public SellerController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { UserRole = Role.Seller, IsLoggedin = true },    // hardcoded values 
            };
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    ViewBag.errorMessage = "All Details having * are required";
                    return View(viewModel);
                }
                Guid? userId = HttpContext.Items["UserId"] as Guid?;

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                if (user.Role == Role.Seller)
                {
                    product.SellerId = user.UserId;
                    await dbContext.Products.AddAsync(product);
                    await dbContext.SaveChangesAsync();
                    return RedirectToAction("MyProducts");
                }
                return RedirectToAction("login", "User");
            }
            catch (Exception error)
            {
                ViewBag.errorMessage = error.Message;
                return View("error", viewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> MyProducts()
        {

            Guid? userId = HttpContext.Items["UserId"] as Guid?;

            var myProducts = await dbContext.Products
            .Where(p => p.SellerId == userId && p.IsArchived == false && p.IsDeleted == false)
            .ToListAsync();

            viewModel.Products = myProducts;

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MyArchive()
        {
            Guid? userId = HttpContext.Items["UserId"] as Guid?;

            var myProducts = await dbContext.Products
            .Where(p => p.SellerId == userId && p.IsArchived == true && p.IsDeleted == false).ToListAsync();

            viewModel.Products = myProducts;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> DeletedProducts()
        {

            Guid? userId = HttpContext.Items["UserId"] as Guid?;

            var myProducts = await dbContext.Products
            .Where(p => p.SellerId == userId && p.IsDeleted == true && p.IsArchived == true)
            .ToListAsync();

            viewModel.Products = myProducts;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> ArchiveProduct(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product != null)
            {
                product.IsArchived = true;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("MyProducts");
        }

        [HttpGet]
        public async Task<IActionResult> UnArchiveProduct(Guid id)
        {

            var product = await dbContext.Products.FindAsync(id);
            if (product != null)
            {
                product.IsArchived = false;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("MyArchive");
        }

        [HttpGet]
        public async Task<IActionResult> SendToRecycle(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);

            if (product != null)
            {
                product.IsDeleted = true;
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("MyArchive");

        }

        [HttpGet]
        public async Task<IActionResult> RestoreFromRecycle(Guid id)
        {

            var product = await dbContext.Products.FindAsync(id);

            if (product != null)
            {
                product.IsDeleted = false;
                await dbContext.SaveChangesAsync();
            }


            return RedirectToAction("DeletedProducts");

        }


    }
}
