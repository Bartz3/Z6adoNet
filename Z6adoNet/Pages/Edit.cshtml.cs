using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
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
            //editProduct.name = _name;
        }
        public IConfiguration _configuration { get; }
        private readonly ILogger<EditModel> _logger;
        public EditModel(IConfiguration configuration, ILogger<EditModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public IActionResult OnPost(Product p)
        {
            p.id = editProduct.id;
            p.name = editProduct.name;
            p.price = editProduct.price;


            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);

            SqlCommand cmd = new SqlCommand("sp_productUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter name_SqlParam = new SqlParameter("@name", SqlDbType.VarChar,
            50);
            name_SqlParam.Value = p.name;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter price_SqlParam = new SqlParameter("@price", SqlDbType.Money);
            price_SqlParam.Value = p.price;
            cmd.Parameters.Add(price_SqlParam);

            SqlParameter productID_SqlParam = new SqlParameter("@productID",
            SqlDbType.Int);
            //productID_SqlParam.Direction = ParameterDirection.Input;
            productID_SqlParam.Value = p.id;
            cmd.Parameters.Add(productID_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("List");
        }
    }
}
