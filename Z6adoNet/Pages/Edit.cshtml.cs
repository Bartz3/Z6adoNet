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

            //string sql = "UPDATE Product (name,price) VALUES('" + p.name + "','" + p.price.ToString() + "')";
            string sql = "UPDATE Product SET price=" + p.price + " WHERE Id=" + p.id.ToString();

            SqlCommand cmd = new SqlCommand(sql, con);
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
            string sql2 = "UPDATE Product SET name='" +p.name+"'WHERE Id=" + p.id.ToString();
            SqlCommand cmd2 = new SqlCommand(sql2, con);
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
            //using (con)
            //{
            //    con.Open();
            //    SqlCommand cmd = new SqlCommand(sql, con);
            //    SqlDataReader reader = cmd.ExecuteReader();
            //    reader.Close();
            //    con.Close();
            //}


            return RedirectToPage("List");
            return RedirectToPage("List");
        }
    }
}
