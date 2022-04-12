using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Zadanie6.Models;

namespace Zadanie6.Pages
{

    public class BucketModel : PageModel
    {
        public List<Product> bucketList = new List<Product>();

        public Product product { get; set; }
        public IConfiguration _configuration { get; }
        private readonly ILogger<BucketModel> _logger;
        public BucketModel(IConfiguration configuration, ILogger<BucketModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {
            var cookie = Request.Cookies["ciastkowyProdukt"]; // Odebrania ciastka
            if (cookie == null)
            {
                return;
            }
            string[] IDs = cookie.Split(',');
            int pom;
            Product produkt;
            foreach (var id in IDs) // Dodawanie wszystkich produktów z ciastka do koszyka
            {
                bool bool2 = int.TryParse(id, out pom);
                if (!bool2)
                    continue;
                
                // Tutaj
                string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

                SqlConnection con = new SqlConnection(myCompanyDBcs);
                string sql = "SELECT name FROM Product where Id=" + id.ToString()+"AND Id IS NOT NULL";
                SqlCommand cmd = new SqlCommand(sql, con);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                Product _product;
                while (reader.Read())
                {
                    //if (int.Parse(reader["Id"].ToString()) != null)
                    //{
                        _product = new Product();
                        _product.id = int.Parse(reader["Id"].ToString());
                        _product.name = reader["Name"].ToString();
                        if (_product.description != null)
                        {
                            _product.description = reader["Description"].ToString();
                        }
                        _product.price = Decimal.Parse(reader["Price"].ToString());

                        bucketList.Add(_product);

                 //   }
                   // else continue;
                }
                reader.Close(); con.Close();

                //Tutaj
            }

        }

        public IActionResult OnPost()
        {
            Response.Cookies.Delete("ciastkowyProdukt");
            return RedirectToPage("Bucket");
        }

    }
}
