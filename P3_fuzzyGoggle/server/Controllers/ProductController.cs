using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase    // inheritance with controller base 
    {

        [HttpPost("Create")]
        public async Task<IActionResult> Create()
        {
            return Ok();
        }
    }
}
