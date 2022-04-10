using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class DeleteModel : PageModel
    {
       
        public Product deleteProduct =new Product();
        public void OnGet(int id)
        {
            deleteProduct.id = id;
        }
        public IActionResult OnPost(int id)
        {

            return RedirectToPage("List");
        }
    }
}
