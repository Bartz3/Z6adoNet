using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class CreateCategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Category category { get; set; }

        public IConfiguration _configuration { get; }
        private readonly ILogger<CreateModel> _logger;
        public CreateCategoryModel(IConfiguration configuration, ILogger<CreateModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(Category c)
        {
            c.shortName = category.shortName;
            c.longName = category.longName;
           // c.CategoryID = category.id;

            string myCompanyDBcs =
           _configuration.GetConnectionString("myCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_categoryAdd", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@shortName", SqlDbType.VarChar, 30);

            name_SqlParam.Value = c.shortName;
            cmd.Parameters.Add(name_SqlParam);
            SqlParameter price_SqlParam = new SqlParameter("@longName", SqlDbType.VarChar,80);
            price_SqlParam.Value = c.longName;
            cmd.Parameters.Add(price_SqlParam);

            SqlParameter productID_SqlParam = new SqlParameter("@categoryID",
            SqlDbType.Int);

            productID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(productID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToPage("CategoryList");
        }
    }
}
