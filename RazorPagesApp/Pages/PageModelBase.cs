using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace RazorPagesApp.Pages
{
    public class PageModelBase : PageModel
    {
        public string GetUserId()
        {
            var claims = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            return claims.Value;
        }

    }
}
