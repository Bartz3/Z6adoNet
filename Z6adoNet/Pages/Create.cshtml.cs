using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class CreateModel : PageModel
    {
        public Product newProduct { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {

            return RedirectToPage("List");
        }
    }
}
