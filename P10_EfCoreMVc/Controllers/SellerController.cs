using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
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

        public IActionResult CreateProduct(){

            return View();
        }




        [HttpPost("create-product")]

        public async Task<IActionResult> CreateProduct(Product product)
        {

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                var token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login");
                }

                var userId = tokenService.VerifyTokenAndGetId(token);

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId.ToString() == userId);

                if (user == null || user.Role != Role.Seller)
                {
                    return Unauthorized(); 
                }

                product.SellerId = user.UserId;

                await dbContext.Products.AddAsync(product);
                await dbContext.SaveChangesAsync();

                return RedirectToAction("SellerDashboard", "User");


            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return View();
            }
        }
    }
}
