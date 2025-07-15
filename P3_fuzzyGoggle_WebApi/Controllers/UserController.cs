
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using MongoDB.Driver;
using WebApplication1.Interfaces;
using MongoDB.Bson;
using WebApplication1.Data;



namespace WebApplication1.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]


    public class UserController(MongoDbContext dbContext, ICloudinaryService cloudinary, IMailService mailService, ITokenService tokenService) : ControllerBase    // inheritance with controller base 
    {

        private readonly MongoDbContext dbContext = dbContext;
        private readonly ICloudinaryService cloudinary = cloudinary;
        private readonly IMailService mailService = mailService;

        private readonly ITokenService tokenService = tokenService;




        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User req)
        {
            try
            {

                if (string.IsNullOrEmpty(req.Email) || string.IsNullOrEmpty(req.Password) || string.IsNullOrEmpty(req.Username))
                {
                    return BadRequest(new
                    {
                        message = "(*)are required fields"
                    });
                }

                var existingUser = await dbContext.Users.Find(u => u.Email == req.Email).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return BadRequest(new      //400
                    {
                        message = "A user with this email already exists."
                    });
                }

                req.Password = BCrypt.Net.BCrypt.HashPassword(req.Password);    // encryption 

                req.DateCreated = DateTime.UtcNow;     // universal time code 


                await dbContext.Users.InsertOneAsync(req);

                // await mailService.SendEmailAsync(user.Email, "Welcome Email", "Welcome to our Website Stay tuned for upcoming offers", false);

                return Ok(new
                {
                    message = "User created successfully! , Kindly Check ur mail for verification",
                    username = req.Username,
                    email = req.Email,
                    payload = new
                    {
                        id = req.Id.ToString()
                    }
                });
            }
            catch (Exception error)
            {

                Console.WriteLine(error.Message);
                return StatusCode(500, new
                {
                    message = "Server Error , try Again after Sometime ",
                });
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login request, ITokenService tokenService)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest(new
                    {
                        message = "Fields with * are required"
                    });
                }

                var user = await dbContext.Users.Find(u => u.Email == request.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest(new
                    {
                        message = "No User Found with this Email!"
                    });
                }

                var verify = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);
                if (verify)
                {
                    var token = tokenService.CreateToken(user.Id.ToString(), user.Email, user.Username);

                    // Set token in cookie
                    Response.Cookies.Append("authorization_token", token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,           // set to true in production (HTTPS)
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(72)
                    });

                    return Ok(new
                    {
                        message = "Logged in successfully!",
                        email = request.Email,
                        username = user.Username,
                        // payload = token
                        // No token in payload now
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        message = "Incorrect Password!"
                    });
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, new
                {
                    message = "Server Error, try again after some time"
                });
            }
        }


        [HttpGet("Token/Verify")]   // token mechanism will be shifted to headers or cookies 
        public async Task<IActionResult> Verify()
        {
            try
            {

                var token = Request.Cookies["authorization_token"];   /// ager yaha token nahi hai 

                if (string.IsNullOrEmpty(token))
                {
                    token = Request.Headers.Authorization.FirstOrDefault()?.Replace("Bearer", "");  /// token 
                }

                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(403, new { message = "Token missing" });
                }

                // userId from  verified token
                var userId = tokenService.VerifyTokenAndGetId(token);


                if (ObjectId.Empty == userId)
                {
                    return StatusCode(401, new
                    {
                        message = "Unauthorized!"
                    });
                }

                var user = await dbContext.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();

                return Ok(new
                {
                    username = user.Username,
                    email = user.Email,
                    message = "User Verified Successfully!"
                });
            }
            catch (Exception error)
            {
                return StatusCode(500, new
                {
                    message = $"Server Error : {error.Message}"
                });
            }
        }

        [HttpPost("Forgot/Password")]
        public async Task<IActionResult> Forgot([FromBody] Email request)
        {
            try
            {
                var user = await dbContext.Users.Find(u => u.Email == request.UserEmail).FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest(new      //400
                    {
                        message = "No User Found with this Email!"
                    });
                }

                var otp = "3456";     // automaticall generate 
                                      // save that otp on datanase

                // await dbContext.Otps.InsertOneAsync(otp);   // check this errror
                await mailService.SendEmailAsync(request.UserEmail, "OTP", otp, false);

                return Ok(new
                {
                    message = "OTP has been to your registered Email!"

                });

            }
            catch (Exception error)
            {
                return StatusCode(500, new
                {

                    message = $"Server Error : {error.Message}"
                });
            }
        }

        [HttpPost("Otp/Verify/{otpToken}")]
        public async Task<IActionResult> Verify(string otpToken, [FromBody] OTP request)
        {
            try
            {

                var userId = tokenService.VerifyTokenAndGetId(otpToken);

                if (ObjectId.Empty == userId)
                {
                    return StatusCode(401, new
                    {
                        message = "Unauthorized!"
                    });
                }

                var user = await dbContext.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest(new      //400
                    {
                        message = "No User Found with this Email!"
                    });
                }

                if (request.Pass != request.ConfirmPass)
                {
                    return BadRequest(new
                    {
                        message = "Password doesnot match!"
                    });
                }


                var otp = "3456";    // retreive otp from db using otp id


                if (request.Otp == otp)
                {
                    // first u have to encrypt incomingh pass
                    user.Password = request.ConfirmPass;
                    user.DateModified = DateTime.UtcNow;

                    await dbContext.Users.ReplaceOneAsync(u => u.Id == userId, user);
                    // delete the otp database

                    return Ok(new
                    {
                        message = "Password changed Sucessfully!"
                    });

                }
                else
                {

                    return BadRequest(
                      new
                      {
                          message = "OTP error"

                      }
                    );
                }
            }
            catch (Exception error)
            {
                return StatusCode(500, new
                {
                    message = $"Server Error : {error.Message}"
                });
            }
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> EditUser(string id, [FromBody] User user)
        {
            try
            {

                var findUser = await dbContext.Users.Find(u => u.Id.ToString() == id).FirstOrDefaultAsync();

                if (findUser == null)
                {
                    return NotFound(new
                    {
                        message = "User not found."
                    });
                }
                findUser.Email = user.Email ?? findUser.Email;
                findUser.Username = user.Username ?? findUser.Username;
                findUser.Phone = user.Phone ?? findUser.Phone;

                await dbContext.Users.ReplaceOneAsync(u => u.Id.ToString() == id, findUser);

                return Ok(new
                {
                    message = "User edited successfully!"
                });
            }
            catch (Exception ex)
            {


                return StatusCode(500, new
                {
                    message = "Server Error | 500",
                    error = ex.Message
                });
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {

                var delete = await dbContext.Users.DeleteOneAsync(user => user.Id.ToString() == id);

                if (delete.DeletedCount == 0)
                {
                    return BadRequest(new
                    {
                        message = "User not found or may have already been deleted."
                    });
                }

                return Ok(new
                {
                    message = "User deleted successfully!",
                    payload = new
                    {
                        deleteCount = delete.DeletedCount
                    }
                });
            }
            catch (Exception ex)
            {


                return StatusCode(500, new
                {
                    message = "Server Error!",
                    error = ex.Message
                });
            }
        }

        [HttpPut("Upload/profile/{id}")]
        public async Task<IActionResult> Upload(string id, IFormFile file)
        {
            try
            {

                var findUser = await dbContext.Users.Find(u => u.Id.ToString() == id).FirstOrDefaultAsync();
                if (findUser == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var uploadURL = cloudinary.UploadImageAsync(file);

                findUser.ProfilePictureUrl = uploadURL.Result.ToString();

                await dbContext.Users.ReplaceOneAsync(u => u.Id.ToString() == id, findUser);

                return Ok(new
                {
                    message = "File uploaded successfully.",
                    imageUrl = uploadURL.Result.ToString()
                });
            }

            catch (Exception error)
            {
                return StatusCode(500, new { message = $"Server Error: {error.Message}" });
            }
        }
    }


}