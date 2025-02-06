using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using WebApplication1.Interfaces;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class UserProfileModel(MongoDbService dbService , ITokenService tokenService) : PageModel
    {

        private readonly MongoDbService  _dbService = dbService;
        private readonly ITokenService _tokenService = tokenService;

        [BindProperty]

        public IFormFile MyFile {get; set;}

        public string username = "";
        public string profilePic = "";

        public async Task <IActionResult> OnGetAsync()
        {
            // conditional rendering 
            // get token from cookies

            var token = Request.Cookies["AuthToken"];

               if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Login");
                }

            var userId = _tokenService.VerifyTokenAndGetId(token);

            var user = await _dbService.Users.Find(u => u.Id == userId).FirstOrDefaultAsync();   // 

            if(user != null ){

                    username = user.Username;
                    profilePic = user.ProfilePictureUrl; 
            }



            // verify using method from token service
            Console.WriteLine("some body is viewing the page");


            return Page();
        }
  
        public async Task <IActionResult> OnPostAsync(){



                return Page();

            
        }
  
  
  
    }

 
}
