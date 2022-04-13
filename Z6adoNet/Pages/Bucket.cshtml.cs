using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using Zadanie6.Models;
using Microsoft.Extensions.Configuration;
using System.Text;


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
            //Response.Cookies.Delete("ciastkowyProdukt");
            var cookie = Request.Cookies["ciastkowyProdukt"]; // Odebrania ciastka
            if (cookie == null)
            {
                return;
            }
            string[] IDs = cookie.Split(',', StringSplitOptions.RemoveEmptyEntries);

            int idOut;
            int[] newIds = new int[IDs.Length];
            for (int i = 0; i < IDs.Length; i++)
            {
                newIds[i] = 0;
            }
            int c = 0;
            foreach (var id in IDs) // Dodawanie wszystkich produkt�w z ciastka do koszyka
            {
                bool bool2 = int.TryParse(id, out idOut);
                if (!bool2)
                    continue;

                newIds[c++] = idOut;
            }
            for (int i = 0; i < newIds.Length; i++)
            {

            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);

            string sql = "SELECT * FROM Product WHERE Id="+newIds[c-1].ToString();
            SqlCommand cmd = new SqlCommand(sql, con);

            //cmd.Parameters.AddWithValue("@Id",newIds[c-1]);

           Product _product;
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

                
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

                bucketList.Add(_product);
            }
            reader.Close(); con.Close();
            }

        }
        //public void OnGet()
        //{
        //    //Response.Cookies.Delete("ciastkowyProdukt");
        //    var cookie = Request.Cookies["ciastkowyProdukt"]; // Odebrania ciastka
        //    if (cookie == null)
        //    {
        //        return;
        //    }
        //    string[] IDs = cookie.Split(',', StringSplitOptions.RemoveEmptyEntries);

        //    int idOut;
        //    int[]newIds=new int[IDs.Length];
        //    for (int i = 0; i < IDs.Length; i++)
        //    {
        //        newIds[i] = 0;
        //    }
        //    int c = 0;


        //    string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

        //    SqlConnection con = new SqlConnection(myCompanyDBcs);
        //    Product _product;
        //    foreach (var id in IDs) // Dodawanie wszystkich produkt�w z ciastka do koszyka
        //    {
        //        bool bool2 = int.TryParse(id, out idOut);
        //        if (!bool2)
        //            continue;

        //        //string sql = "SELECT * FROM Product where Id= @Id AND Id IS NOT NULL";
        //        string sql = "SELECT * FROM Product where Id=@Id";
        //        SqlCommand cmd = new SqlCommand(sql, con);
        //        cmd.Parameters.AddWithValue("@Id", idOut); 
        //        SqlDataReader reader = cmd.ExecuteReader();

        //            try
        //            {
        //                con.Open();
        //                //int numAff = cmd.ExecuteNonQuery();
        //                _product = new Product();

        //                // _product.id = int.Parse(reader["Id"].ToString());
        //                _product.id = reader.GetInt32(idOut);
        //                //_product.name = reader["Name"].ToString();
        //                _product.name = reader.GetString(idOut);
        //                if (_product.description != null)
        //                {
        //                    _product.description = reader["Description"].ToString();
        //                }
        //                _product.price = Decimal.Parse(reader["Price"].ToString());

        //                bucketList.Add(_product);

        //            }
        //            catch (SqlException exc)
        //            {
        //                ;
        //            }
        //            finally { con.Close(); }


                
        //    }
        //        //newIds[c] = idOut;
        //    }
   
        
        public IActionResult OnPost()
        {
            Response.Cookies.Delete("ciastkowyProdukt");
            return RedirectToPage("Bucket");
        }

    }
}


//public void OnGet()
//{
//    string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
//    SqlConnection con = new SqlConnection(myCompanyDBcs);

//    string sql = "SELECT * FROM Product where Id=@Id";

//    SqlCommand cmd = new SqlCommand(sql, con);
//    cmd.Parameters.AddWithValue("@Id", 1003);
//    Product _product;
//    con.Open();
//    SqlDataReader reader = cmd.ExecuteReader();


//    while (reader.Read())
//    {
//        _product = new Product();
//        _product.id = int.Parse(reader["Id"].ToString());
//        _product.name = reader["Name"].ToString();
//        if (_product.description != null)
//        {
//            _product.description = reader["Description"].ToString();
//        }
//        _product.price = Decimal.Parse(reader["Price"].ToString());

//        bucketList.Add(_product);
//    }
//    reader.Close(); con.Close();

//}