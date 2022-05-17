using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Zadanie6.Pages.Login
{
    public class LogOutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await HttpContext.SignOutAsync("CookieAuthentication");
            return this.RedirectToPage("/Index");
        }
    }
}
