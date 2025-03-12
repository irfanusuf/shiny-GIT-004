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
    public class OrderController : Controller
    {
      

  
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly HybridViewModel viewModel;

        public OrderController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { UserRole = Role.Buyer, IsLoggedin = true },    // hardcoded values 
            };
        }


        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {

            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);

            var cart = await dbContext.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.BuyerId == userId); // finding cart of user 

            if (cart == null)
            {
                ViewBag.cartEmpty = "Cart is Empty";
                return View(viewModel);     // have to watch it in future if there are no items in cart .. 
            }

            var address = await dbContext.Addresses.FirstOrDefaultAsync(a => a.BuyerId == userId);

            var cartproducts = await dbContext.CartProducts
            .Include(cp => cp.Product)
            .Where(cp => cp.CartId == cart.CartId)
            .ToListAsync();


            viewModel.CartProducts = cartproducts;
            viewModel.Cart = cart;
            viewModel.Address = address;

            return View(viewModel);
        }
     
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            var userId = tokenService.VerifyTokenAndGetId(token);

            var cart = await dbContext.Carts
            .Include(c => c.CartProducts)
            .ThenInclude(cp => cp.Product)
            .FirstOrDefaultAsync(c => c.BuyerId == userId);


            // Convert CartProducts to OrderProducts

            var orderProducts = cart.CartProducts.Select(cp => new OrderProduct
            {
                ProductId = cp.ProductId,
                Quantity = cp.Quantity
            }).ToList();

            var order = new Order
            {
                OrderStatus = Status.Pending,
                OrderPrice = cart.CartValue,
                BuyerId = userId,
                OrderProducts = orderProducts

            };

            var createOrder = await dbContext.Orders.AddAsync(order);

            dbContext.CartProducts.RemoveRange(cart.CartProducts);
            cart.CartValue = 0;
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Payment");
        }

        [HttpGet]
        public async Task <IActionResult> Payment(){

            return View(viewModel);
        }



    }




}