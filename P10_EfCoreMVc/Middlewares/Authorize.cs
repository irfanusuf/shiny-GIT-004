using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication1.Interfaces;

public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Request.Cookies["AuthToken"];
        
        var tokenService = context.HttpContext.RequestServices.GetService(typeof(ITokenService)) as ITokenService;

        if (string.IsNullOrEmpty(token) || tokenService?.VerifyTokenAndGetId(token) == null)
        {
            
            var returnUrl = context.HttpContext.Request.Path + context.HttpContext.Request.QueryString;
            context.HttpContext.Session.SetString("ReturnUrl", returnUrl);

 
            context.Result = new RedirectToActionResult("Login", "User", null);
        }
        else
        {
          
            context.HttpContext.Items["UserId"] = tokenService.VerifyTokenAndGetId(token);
        }
    }
}
