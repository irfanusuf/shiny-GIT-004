
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Driver;
using WebApplication1.Models;
using WebApplication1.Services;



namespace WebApplication1.Pages
{
    public class RegisterModel(MongoDbService dbService) : PageModel
    {
      private readonly MongoDbService _dbservice = dbService;

      [BindProperty]
      public required User RegisterUser {get; set;}

      public string errorMessage = "";
      public string successMessage = "";

        public void OnGet()
        {
        }


        public async Task <IActionResult> OnPostAsync()
        {
            try
            {
                if(!ModelState.IsValid){
                errorMessage = "All credentials Required!";
                return Page();
            }
                var user = await _dbservice.Users.Find(u => u.Email == RegisterUser.Email).FirstOrDefaultAsync();

                if(user != null){
                    errorMessage = "User Already Exists!";
                    return Page();
                }

                var hashPass = BCrypt.Net.BCrypt.HashPassword(RegisterUser.Password);

                RegisterUser.Password = hashPass;


                if(RegisterUser != null){
                  await _dbservice.Users.InsertOneAsync(RegisterUser);
                }

             
                return RedirectToPage("/Login");

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
                throw;
            }

           
            
        }
    }


}



