using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Zadanie6.Models;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Data;

namespace Zadanie6.Pages
{
    public class ListModel : PageModel
    {


        public List<Product> productList = new List<Product>();

        public Product product { get; set; }
        public IConfiguration _configuration { get; }
        private readonly ILogger<ListModel> _logger;
        public ListModel(IConfiguration configuration, ILogger<ListModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {    
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_productDisplay", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Product _product;
            while (reader.Read())
            {
                _product = new Product();
                _product.id = int.Parse(reader["Id"].ToString());
                _product.name = reader["Name"].ToString();
                if (_product.description != null) 
                {
                      _product.description = reader["Description"].ToString();
                }
                _product.price = Decimal.Parse(reader["Price"].ToString());
                
                productList.Add(_product);
            }
            reader.Close(); con.Close();      
        }

        public IActionResult OnPost()
        {
            return RedirectToPage("Bucket");
        }
    }
}
