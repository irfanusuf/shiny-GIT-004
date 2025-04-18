using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {

        [HttpPost("Create")]

        public IActionResult Blog(){



            return Ok();
        }

    }
}
