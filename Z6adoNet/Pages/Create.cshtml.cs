using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Product newProduct { get; set; }

        public IConfiguration _configuration { get; }
        private readonly ILogger<CreateModel> _logger;
        public CreateModel(IConfiguration configuration, ILogger<CreateModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {

        }
        public IActionResult OnPost(Product p)
        {
            p.name = newProduct.name;
            p.price = newProduct.price;

            string myCompanyDBcs =
           _configuration.GetConnectionString("myCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_productAdd", con);
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
            productID_SqlParam.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(productID_SqlParam);
            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToPage("List");
        }
    }
}
//public IActionResult OnPost(Product p)
//{
//    p.name = newProduct.name;
//    p.price = newProduct.price;
//    string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
//    SqlConnection con = new SqlConnection(myCompanyDBcs);
//    //string sql = "INSERT INTO Product VALUES(5,'"+p.name+"','"+ p.price.ToString()+"')";
//    // string sql = "INSERT INTO Product (name,price) VALUES('"+p.name+"','"+ p.price.ToString()+"')";
//    string sql = "INSERT INTO Product (name,price) VALUES(@name,@price)";

//    SqlCommand cmd = new SqlCommand(sql, con);
//    cmd.Parameters.AddWithValue("@name", p.name);
//    cmd.Parameters.AddWithValue("@price", p.price);
//    try
//    {
//        con.Open();
//        int numAff = cmd.ExecuteNonQuery();
//        //lblInfoText += string.Format("<br />Deleted <b>{0}</b> record(s)<br />", numAff);
//    }
//    catch (SqlException exc)
//    {
//        //lblInfoText += string.Format("<b>Error:</b> {0}<br /><br />", exc.Message);
//    }
//    finally { con.Close(); }

//    return RedirectToPage("List");
//}