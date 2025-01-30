using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: AdminController
        public ActionResult Register()
        {
            return View();
        }


         public ActionResult Login()
        {
            return View();
        }

    }
}
