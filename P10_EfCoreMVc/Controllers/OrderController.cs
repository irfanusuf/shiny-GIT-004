using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;



namespace WebApplication1.Controllers
{
    public class OrderController : Controller
    {
        private readonly SqlDbContext _context;

        public OrderController(SqlDbContext context)
        {
            _context = context;
        }

     


    //  fetch all orders    // admin Route 
        public async Task<IActionResult> Index()
        {
           
            var orders = await _context.Orders.ToListAsync();

            return View(orders);
        }


        // fetch order by id 
        public async Task<IActionResult> Details(Guid id)
        {

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null)
            {
                return NotFound(); 
            }

            return View(order);
        }


      

    }
}