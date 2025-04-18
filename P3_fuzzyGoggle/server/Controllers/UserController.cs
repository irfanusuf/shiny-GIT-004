
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Services;
using MongoDB.Driver;
using WebApplication1.Interfaces;
using MongoDB.Bson;


namespace WebApplication1.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]


    public class UserController(MongoDbService dbService, ICloudinaryService cloudinary, IMailService mailService) : ControllerBase    // inheritance with controller base 
    {

        private readonly MongoDbService _dbservice = dbService;
        private readonly ICloudinaryService _cloudinary = cloudinary;
        private readonly IMailService _mailService = mailService;

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {

                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password) || string.IsNullOrEmpty(user.Username))
                {
                    return BadRequest(new
                    {
                        message = "(*)are required fields"
                    });
                }

                var existingUser = await _dbservice.Users.Find(u => u.Email == user.Email).FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    return BadRequest(new      //400
                    {
                        message = "A user with this email already exists."
                    });
                }

                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);    // encryption 

                user.DateCreated = DateTime.UtcNow;     // universal time code 


                await _dbservice.Users.InsertOneAsync(user);

                // await _mailService.SendEmailAsync(user.Email, "Welcome Email", "Welcome to our Website Stay tuned for upcoming offers", false);

                return Ok(new
                {
                    message = "User created successfully! , Kindly Check ur mail for verification",
                    payload = new
                    {
                        id = user.Id.ToString()
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
                        message = "Feilds with * are required"
                    });
                }
                var user = await _dbservice.Users.Find(u => u.Email == request.Email).FirstOrDefaultAsync();
                if (user == null)
                {
                    return BadRequest(new      //400
                    {
                        message = "No User Found with this Email!"
                    });
                }

                var verify = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);


                if (verify)
                {
                    // json token create kerna hai 
                    var token = tokenService.CreateToken(user.Id.ToString(), user.Email, user.Username);
                    return Ok(new      //200
                    {
                        message = "Logged In succesFully!",
                        payload = token,
                        userId = user.Id.ToString()
                    });
                }
                else
                {
                    return BadRequest(new      //400
                    {
                        message = "Incorrect PassWord! "
                    });
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                return StatusCode(500, new
                {
                    message = "Server Error , Try Again after Sometime ",

                });
            }
        }



        [HttpGet("Verify/{token}")]

        public async Task<IActionResult> Verify(string token ,ITokenService tokenService)
        {
            try
            {
                var VerifyTokenAndGetId = tokenService.VerifyTokenAndGetId(token);

                if(ObjectId.Empty == VerifyTokenAndGetId)
                {
                    return StatusCode(401 , new
                    {
                        message = "Unauthorized!"
                    });
                }

                var user = await _dbservice.Users.Find(u => u.Id == VerifyTokenAndGetId).FirstOrDefaultAsync();
                
                return Ok(new
                {   
                    user = user,
                    success = true,
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
                var user = await _dbservice.Users.Find(u => u.Email == request.UserEmail).FirstOrDefaultAsync();

                if (user == null)
                {
                    return BadRequest(new      //400
                    {
                        message = "No User Found with this Email!"
                    });
                }

                var otp = "3456";     // automaticall generate 
                                      // save that otp on datanase

                // await _dbservice.Otps.InsertOneAsync(otp);   // check this errror
                await _mailService.SendEmailAsync(request.UserEmail, "OTP", otp, false);

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

        [HttpPost("Otp/Verify/{userId}/{otpId}")]
        public async Task<IActionResult> Verify(string userId, string otpId, [FromBody] OTP request)
        {
            try
            {
                var user = await _dbservice.Users.Find(u => u.Id.ToString() == userId).FirstOrDefaultAsync();

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

                    await _dbservice.Users.ReplaceOneAsync(u => u.Id.ToString() == userId, user);
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

                var findUser = await _dbservice.Users.Find(u => u.Id.ToString() == id).FirstOrDefaultAsync();

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

                await _dbservice.Users.ReplaceOneAsync(u => u.Id.ToString() == id, findUser);

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

                var delete = await _dbservice.Users.DeleteOneAsync(user => user.Id.ToString() == id);

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

        [HttpPost("Upload/profile/{id}")]
        public async Task<IActionResult> Upload(string id, IFormFile file)
        {
            try
            {

                var findUser = await _dbservice.Users.Find(u => u.Id.ToString() == id).FirstOrDefaultAsync();
                if (findUser == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                var uploadURL = _cloudinary.UploadImageAsync(file);

                findUser.ProfilePictureUrl = uploadURL.Result.ToString();

                await _dbservice.Users.ReplaceOneAsync(u => u.Id.ToString() == id, findUser);

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