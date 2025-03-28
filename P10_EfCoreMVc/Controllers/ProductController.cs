using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Middlewares;
using WebApplication1.Models;
using WebApplication1.Models.JunctionModels;
using WebApplication1.Models.ViewModel;



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
                Navbar = new NavbarModel { IsLoggedin = false  },   // hardcoded values 
            };
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid ProductId)
        {
            var product = await dbContext.Products.FindAsync(ProductId);
            var products = await dbContext.Products
            .Where(p => p.Category == product.Category && p.SubCategory == product.SubCategory)
            .ToListAsync();
            viewModel.Product = product;
            viewModel.Products = products;
            return View(viewModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> AddToCart(Guid ProductId)
        {
            try
            {
                Guid? userId = HttpContext.Items["UserId"] as Guid?;
                var product = await dbContext.Products.FindAsync(ProductId);

                if (userId == product.SellerId)
                {
                    ViewBag.errorMessage = "Can't Buy our own Product!";
                    return View("Error", viewModel);
                }

                var cart = await dbContext.Carts
                .Include(c => c.CartProducts)
                .FirstOrDefaultAsync(c => c.BuyerId == userId);    // cart ko find kerhay hai 

                if (cart == null)
                {
                    cart = new Cart
                    {
                        BuyerId = (Guid)userId,
                        CartValue = 0
                    };
                    await dbContext.Carts.AddAsync(cart);
                    await dbContext.SaveChangesAsync();
                }

                var existingCartProduct = await dbContext
                .CartProducts.FirstOrDefaultAsync(cp => cp.CartId == cart.CartId && cp.ProductId == ProductId);   // finding cartProduct 

                if (existingCartProduct == null)
                {
                    var cartProduct = new CartProduct
                    {
                        CartId = cart.CartId,
                        ProductId = ProductId,
                        Quantity = 1
                    };

                    await dbContext.CartProducts.AddAsync(cartProduct);    
                    cart.CartValue += product.Price ;
                    await dbContext.SaveChangesAsync();
                }

                if (existingCartProduct != null && existingCartProduct.ProductId == ProductId)
                {
                    existingCartProduct.Quantity += 1;
                    cart.CartValue += product.Price ;
                    await dbContext.SaveChangesAsync();
                }

                return RedirectToAction("Cart", "Buyer");
            }
            catch (Exception ex)
            {
                console.log( ex.Message , "Error in Product/AddtoCart.");
                ViewBag.errorMessage = "Oops some Error.Kindly try again after some time !" ;
                return View("Error", viewModel);
            }
        }
    }
}
