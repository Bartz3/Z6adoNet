using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
            var cookie = Request.Cookies["UserLoginCookie"];
            if (cookie == null) { return RedirectToPage("Index"); }

            p.id = editProduct.id;
            p.name = editProduct.name;
            p.price = editProduct.price;
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);
            //string sql = "UPDATE Product SET price=" + p.price +",name='"+p.name+ "' WHERE Id=" + p.id.ToString();
            string sql = "UPDATE Product SET price= @price, name=@name WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", p.name);
            cmd.Parameters.AddWithValue("@price", p.price);
            cmd.Parameters.AddWithValue("@Id", p.id);
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
