using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "INSERT INTO Product (name,price) VALUES(@name,@price)";

            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", p.name);
            cmd.Parameters.AddWithValue("@price", p.price);
            try
            {
                con.Open();
                int numAff = cmd.ExecuteNonQuery();
                //lblInfoText += string.Format("<br />Deleted <b>{0}</b> record(s)<br />", numAff);
            }
            catch (SqlException exc)
            {
                //lblInfoText += string.Format("<b>Error:</b> {0}<br /><br />", exc.Message);
            }
            finally { con.Close(); }

            return RedirectToPage("List");
        }
    }
}
