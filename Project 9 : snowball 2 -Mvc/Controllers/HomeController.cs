using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

  

    [HttpGet]

    public IActionResult Privacy()
    {
        return View();
    }

}

