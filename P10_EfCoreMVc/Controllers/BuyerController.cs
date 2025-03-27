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


    [Authorize]
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
        public IActionResult Dashboard()
        {
         return View(viewModel);
        }

        [HttpGet]
        public async Task<ActionResult> Cart()
        {
            Guid? userId = HttpContext.Items["UserId"] as Guid?;

            var cart = await dbContext.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.BuyerId == userId); // finding cart of user 

            if (cart == null || cart.CartProducts.Count == 0)
            {
                ViewBag.cartEmpty = "Cart is Empty";
                return View(viewModel);
            }

            // for efficency there is serperated cart profucts db query
            var cartproducts = await dbContext.CartProducts 
            .Include(cp => cp.Product)
            .Where(cp => cp.CartId == cart.CartId)
            .ToListAsync();

            viewModel.CartProducts = cartproducts;
            viewModel.Cart = cart;

            return View(viewModel);
        }

        [HttpGet]
        public async Task <IActionResult> Orders (){

        Guid? userId = HttpContext.Items["UserId"] as Guid?;

        // var orders = await dbContext.Orders.Where(o => o.BuyerId == userId).ToListAsync();// finding order using orderId

        var orders = await dbContext.Orders
         .Include(o => o.OrderProducts)  // Include OrderProducts
         .ThenInclude(op => op.Product)  // Include related Product
         .Where(o => o.BuyerId == userId)
         .ToListAsync();


        if(orders.Count == 0){
            ViewBag.errorMessage = "No Orders Found";
            return View(viewModel);
        }

            //  var orderproducts = await dbContext.OrderProducts
            // .Include(op => op.Product)
            // .Where(op => orders.Select(o=>o.OrderId).Contains(op.OrderId))
            // .ToListAsync();
            
            // viewModel.OrderProducts = orderproducts;

            viewModel.Orders = orders;
        
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAddress(Address address)
        {
             Guid? userId = HttpContext.Items["UserId"] as Guid?;

            var availableAdderess = await dbContext.Addresses.FirstOrDefaultAsync(a => a.BuyerId == userId);


            if (ModelState.IsValid)
            {
                address.BuyerId = (Guid)userId;
                await dbContext.Addresses.AddAsync(address);
                await dbContext.SaveChangesAsync();
                return RedirectToAction("CheckOut");
            }

            ViewBag.ErrorMessage = "Address updation un-successfull !";
            return View("CheckOut", viewModel);
        }
    }
}
