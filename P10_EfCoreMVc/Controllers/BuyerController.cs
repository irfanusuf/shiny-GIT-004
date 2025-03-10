using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
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
                Products = [],
                CartProducts = []
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

                if (user.Role == Role.Buyer)
                {
                    viewModel.Navbar.UserRole = Role.Buyer;
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
        public async Task<ActionResult> Cart()
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
                return View(viewModel);
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


        [HttpGet]
        public async Task<IActionResult> CreateOrder(Guid CartId)
        {
            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "User");
            }
            var userId = tokenService.VerifyTokenAndGetId(token);

            var cart = await dbContext.Carts.Include(c => c.CartProducts).FirstOrDefaultAsync(c => c.BuyerId == userId); // finding cart of user 


            // var products =  cart.CartProducts.ToList();

            var cartproducts = await dbContext.CartProducts
            .Include(cp => cp.Product)
            .Where(cp => cp.CartId == cart.CartId)
            .ToListAsync();


            var order = new Order
            {
                OrderStatus = Status.Pending,
                OrderPrice = cart.CartValue,
                BuyerId = userId,
                OrderProducts = (ICollection<Models.JunctionModels.OrderProduct>)cartproducts

            };

            var createOrder = await dbContext.Orders.AddAsync(order);

            return View(viewModel);
        }

    }

}
