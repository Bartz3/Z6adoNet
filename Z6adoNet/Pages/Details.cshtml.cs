using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class DetailsModel : PageModel
    {
       //[BindProperty]
       //public Product detailsProduct { get; set; }
        public Product detailsProduct = new Product();
        public void OnGet(int id)
        {
            detailsProduct.id = id;
        }
    }
}
