using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Product editProduct { get; set; }
        public void OnGet(int id)
        {
            editProduct.id  = id;
        }
    }
}
