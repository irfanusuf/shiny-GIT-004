using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.ViewModel;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{
    public class SellerController : Controller
    {
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        public SellerController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }

        [HttpGet]
        public IActionResult CreateProduct()
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }

            var viewModel = new NavbarModel
            {
                UserRole = Role.Seller,
                IsLoggedin = true
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> MyProducts()
        {
            var token = Request.Cookies["AuthToken"];

             if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }
            var userId = tokenService.VerifyTokenAndGetId(token);   // logged in userId
            var myProducts = await dbContext.Products.Where(p => p.SellerId.ToString() == userId).ToListAsync();   //filter products of only logged in user 
            return View(myProducts);
        }

        [HttpGet]
        public async Task<IActionResult> MyArchive()
        {

            var myProducts = await dbContext.Products.Where(p => p.IsArchived == true).ToListAsync();   //filter products of only logged in user 
            return View(myProducts);
        }

        [HttpGet]
        public async Task<IActionResult> DeletedProducts()
        {

            var myProducts = await dbContext.Products.Where(p => p.IsDeleted == true).ToListAsync();   //filter products of only logged in user 
            return View(myProducts);
        }


        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login", "User");
                }
                var userId = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

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
                Console.WriteLine(error.Message);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
