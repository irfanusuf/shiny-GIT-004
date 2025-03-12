using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.JunctionModels;
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
                Navbar = new NavbarModel { UserRole = Role.Buyer, IsLoggedin = true },    // hardcoded values 
           
            };

        }
    
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
           
                var token = Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("login");
                }

                // var userId = tokenService.VerifyTokenAndGetId(token);

                // var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

                 return View(viewModel);
               
             
        }

        [HttpGet]
        public async Task<ActionResult> Cart()
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User"); 
            }

            var userId = tokenService.VerifyTokenAndGetId(token);

            var cart = await dbContext.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.BuyerId == userId); // finding cart of user 

            if (cart == null || cart.CartProducts.Count == 0)
            {
                ViewBag.cartEmpty = "Cart is Empty";
                return View(viewModel);
            }

            var cartproducts = await dbContext.CartProducts 
            .Include(cp => cp.Product)
            .Where(cp => cp.CartId == cart.CartId)
            .ToListAsync();

            viewModel.CartProducts = cartproducts;
            viewModel.Cart = cart;

            return View(viewModel);
        }

    
        [HttpPost]
        public async Task<IActionResult> CreateAddress(Address address)
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            var userId = tokenService.VerifyTokenAndGetId(token);

            var availableAdderess = await dbContext.Addresses.FirstOrDefaultAsync(a => a.BuyerId == userId);


            if (ModelState.IsValid)
            {
                address.BuyerId = userId;
                await dbContext.Addresses.AddAsync(address);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("CheckOut");
            }

            ViewBag.ErrorMessage = "Address updation un-successfull !";
            return View("CheckOut", viewModel);
        }

       
    
    }

}
