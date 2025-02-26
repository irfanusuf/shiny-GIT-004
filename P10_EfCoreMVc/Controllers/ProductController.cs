using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Details(Guid id)
        {
            
            return View();
        }

    }
}
