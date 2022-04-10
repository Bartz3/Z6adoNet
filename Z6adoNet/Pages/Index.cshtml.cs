using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Z6adoNet.Pages
{
    public class IndexModel : PageModel
    {

        public IActionResult OnGet()
        {
            return RedirectToPage("List");
        }
    }
}