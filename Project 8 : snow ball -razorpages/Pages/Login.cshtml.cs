
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using WebApplication1.Interfaces;
using WebApplication1.Services;



namespace WebApplication1.Pages
{
    public class LoginModel(MongoDbService dbService, ITokenService tokenService) : PageModel
    {
        private readonly MongoDbService _dbservice = dbService;
        private readonly ITokenService _tokenService = tokenService;
        [BindProperty]
        public required UserViewModel MyUser { get; set; }

        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                errorMessage = "All credentials Required!";
                return Page();
            }
            try
            {
                var user = await _dbservice.Users.Find(u => u.Email == MyUser.Email).FirstOrDefaultAsync();

                if (user == null)
                {
                    errorMessage = "User not Found";
                    return Page();
                }

                var verifyHash = BCrypt.Net.BCrypt.Verify(MyUser.Password, user.Password);

                if (!verifyHash)
                {
                    errorMessage = "Password Incorrect!";
                    return Page();
                }
                // depedency injection 
                // creating token 
                var token = _tokenService.CreateToken(user.Id, user.Email, user.Username);

                // send token to cookies

                HttpContext.Response.Cookies.Append(

                    "AuthToken",
                    token,
                    new CookieOptions
                    {

                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict,
                        Expires = DateTimeOffset.UtcNow.AddHours(1)

                    }

                );


                return RedirectToPage("/UserProfile");

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                throw;
            }



        }
    }

    public class UserViewModel
    {

        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}



