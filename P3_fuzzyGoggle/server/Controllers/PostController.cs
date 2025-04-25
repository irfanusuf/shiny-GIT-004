using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Models;
using WebApplication1.Models.DomainModels;
using WebApplication1.Types;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {

        private readonly MongoDbContext dbContext;
        private readonly ITokenService tokenService;
        private readonly ICloudinaryService cloudinary;
        public PostController(MongoDbContext dbContext, ITokenService tokenService, ICloudinaryService cloudinary)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
            this.cloudinary = cloudinary;
        }


        [HttpPost("Create")]
        public async Task<IActionResult> Post( [FromForm] Post req, IFormFile? image , IFormFile? video)
        {

            try
            {
                // var imageUrl = (string?)null;

                // accepting token from cookies 
                var token = Request.Cookies["token"];

                if (string.IsNullOrEmpty(token))
                {
                    token = Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer", "");
                }

                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(403 , new { message = "Token missing" });
                }

                // userId from  verified token
                var userId = tokenService.VerifyTokenAndGetId(token);

                if (MongoDB.Bson.ObjectId.Empty == userId)
                {
                    return Unauthorized(new { message = "Invalid or expired token" });
                }

                // secure Url from clouidnary  if file is present 
                string imageUrl = image != null ? await cloudinary.UploadImageAsync(image) : "no-image";
                string vidUrl =  video !=null ? await cloudinary.UploadVideoAsync(video) : "no-video";

                req.IsEdited = false;
                req.PostPicUrl = imageUrl;
                req.PostVidUrl = vidUrl;
                req.UserId = userId;

                await dbContext.Posts.InsertOneAsync(req);

                var userFilter = Builders<User>.Filter.Eq(u => u.Id, userId);
                var userUpdate = Builders<User>.Update.AddToSet(u => u.Posts, req.Id);
                await dbContext.Users.UpdateOneAsync(userFilter, userUpdate);


                return Ok(new
                {
                    message = "Post Uploaded!",
                    post = req
                });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new
                {
                    message = "Server Error"
                });
            }
        }


        [HttpPut("Edit/{token}/{postId}")]
        public async Task<IActionResult> Edit(string token, string postId, [FromBody] Post req)
        {
            try
            {
                // token verification 
                // userId from  verified token
                var userId = tokenService.VerifyTokenAndGetId(token);

                if (MongoDB.Bson.ObjectId.Empty == userId)
                {
                    return NotFound(new { message = "User not found" });
                }

                var filter = Builders<Post>.Filter.Eq("Id", postId);

                var updateResult = await dbContext.Posts.ReplaceOneAsync(filter, req);

                if (updateResult.MatchedCount == 0)
                {
                    return NotFound(new { message = "Post not found" });
                }

                return Ok(new { message = "Post updated!" });
            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "Server Error" });
            }
        }


        [HttpPut("Archive/{token}/{postId}")]
        public async Task<IActionResult> Archive(string token, string postId)
        {
            try
            {
                var userId = tokenService.VerifyTokenAndGetId(token);

                if (MongoDB.Bson.ObjectId.Empty == userId)
                {
                    return NotFound(new { message = "User not found" });
                }
                var filter = Builders<Post>.Filter.Eq("Id", postId);

                var update = Builders<Post>.Update.Set("Visibility", Visibilty.Archived);

                var updateResult = await dbContext.Posts.UpdateOneAsync(filter, update);

                if (updateResult.MatchedCount == 0)
                {
                    return NotFound(new { message = "Post not found" });
                }

                return Ok(new { message = "Post archived!" });

            }
            catch (System.Exception)
            {
                return StatusCode(500, new { message = "Server Error" });
            }
        }





    }
}
