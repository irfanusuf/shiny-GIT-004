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
    public class ProductController : Controller
    {

        private readonly SqlDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly HybridViewModel viewModel;


        public ProductController(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.viewModel = new HybridViewModel
            {
                Navbar = new NavbarModel { IsLoggedin = false },    // hardcoded values 
                Products = []
            };
        }



        // GET: Product
        public async Task<ActionResult> Details(Guid id)
        {
            var product = await dbContext.Products.FindAsync(id);
            var products = await dbContext.Products
            .Where(p => p.Category == product.Category && p.SubCategory == product.SubCategory)
            .ToListAsync();

            viewModel.Product = product;
            viewModel.Products = products;

            return View(viewModel);
        }



        [HttpGet]
        public async Task<ActionResult> AddToCart(Guid ProductId)
        {

            var token = Request.Cookies["AuthToken"];

            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("login", "User");
            }

            var userId = tokenService.VerifyTokenAndGetId(token);   // logged in userId
            var product = await dbContext.Products.FindAsync(ProductId);

            if (userId == product.SellerId)
            {
                return RedirectToAction("index", "Home");
            }
            else
            {

                var cart = await dbContext.Carts.Include(c => c.Products).FirstOrDefaultAsync(c => c.BuyerId == userId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        BuyerId = userId,
                        CartValue = 0
                    };
                    await dbContext.Carts.AddAsync(cart);
                    await dbContext.SaveChangesAsync();
                }


                var existingCartProduct = await dbContext.CartProducts.FirstOrDefaultAsync(cp => cp.CartId == cart.CartId && cp.ProductId == ProductId);


                if (existingCartProduct == null)
                {

                    var cartProduct = new CartProduct
                    {
                        CartId = cart.CartId,
                        ProductId = ProductId
                    };

                    await dbContext.CartProducts.AddAsync(cartProduct);

                    // Update cart value
                    cart.CartValue += product.Price;
                }

                await dbContext.SaveChangesAsync();


                return RedirectToAction("Cart", "Buyer");
            }

        }

    }
}
