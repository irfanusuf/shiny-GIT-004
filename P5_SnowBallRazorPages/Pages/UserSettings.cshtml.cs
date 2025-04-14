using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApplication1.Pages
{
    public class UserSettingsModel : PageModel
    {
        public async Task <IActionResult> OnGetAsync()
        {

              var token = Request.Cookies["AuthToken"];

               if (string.IsNullOrEmpty(token))
                {
                    return RedirectToPage("/Login");
                }
                return Page();
        }
    }
}
