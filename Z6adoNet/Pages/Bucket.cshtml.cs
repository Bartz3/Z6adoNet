using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie6.Models;

namespace Zadanie6.Pages
{

    public class BucketModel : PageModel
    {
        public List<Product> bucketList = new List<Product>();

        public Product product { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Response.Cookies.Delete("ciastkowyProdukt");
            return RedirectToPage("Bucket");
        }

    }
}
