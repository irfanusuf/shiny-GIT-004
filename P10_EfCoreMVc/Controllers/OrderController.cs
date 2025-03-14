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


        private readonly ILogger<OrderController> _logger;
        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly RazorpayService razorpayService;
        private readonly HybridViewModel viewModel;

        public OrderController(SqlDbContext dbContext, ITokenService tokenService, ILogger<OrderController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            razorpayService = new RazorpayService();
            viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { UserRole = Role.Buyer, IsLoggedin = true },    // hardcoded values 
                // RazorPayOrder = new RazorOrder()
            };
        }



        [HttpGet]
        public async Task<IActionResult> CheckOut(Guid CartId)
        {

            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);

            var cart = await dbContext.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.CartId == CartId); // finding cart of user 

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

            if (cart.CartValue == 0)
            {
                ViewBag.errorMessage = "Your Cart is empty! proceed Directly to the order's section";
                return View("error", viewModel);
            }



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

            return RedirectToAction("Payment", new { order.OrderId });
        }


        [HttpGet]
        public async Task<IActionResult> Payment(Guid OrderId)
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            var userId = tokenService.VerifyTokenAndGetId(token);

            var order = await dbContext.Orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.OrderId == OrderId); // finding order using orderId

            if (order == null || order.OrderProducts.Count == 0)
            {
                ViewBag.cartEmpty = "No recent Orders";
                return View(viewModel);
            }

            var orderproducts = await dbContext.OrderProducts
            .Include(op => op.Product)
            .Where(op => op.OrderId == order.OrderId)
            .ToListAsync();

            var address = await dbContext.Addresses.FirstOrDefaultAsync(a => a.BuyerId == userId);

            viewModel.OrderProducts = orderproducts;
            viewModel.Order = order;
            viewModel.Address = address;

            return View(viewModel);
        }


      
    }
}