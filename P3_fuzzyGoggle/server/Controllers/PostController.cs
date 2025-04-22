using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models.DomainModels;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly MongoDbContext dbContext;
        public PostController(MongoDbContext dbContext)
        {
            this.dbContext = dbContext ;
        }

  
        [HttpPost("Create")]
        public IActionResult Post ([FromBody] Post req ){

            try
            {


                
            }
            catch (System.Exception )
            {
                return StatusCode(500 , new {
                    message = "Server Error"
                });
      
            }

            return Ok();
        }



        
    }
}
