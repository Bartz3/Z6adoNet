using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class DeleteModel : PageModel
    {

        public IConfiguration _configuration { get; }
        private readonly ILogger<DeleteModel> _logger;
        public DeleteModel(IConfiguration configuration, ILogger<DeleteModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public Product deleteProduct =new Product();
        public void OnGet(int id)
        {
            deleteProduct.id = id;
        }
        public IActionResult OnPost(int id)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_productDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@productID", SqlDbType.Int);
            name_SqlParam.Value = id;
            cmd.Parameters.Add(name_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("List");
        }
    }
}
//public IActionResult OnPost(int id)
//{
//    string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

//    SqlConnection con = new SqlConnection(myCompanyDBcs);
//    string sql = "DELETE FROM Product WHERE Id = @Id";
//    SqlCommand cmd = new SqlCommand(sql, con);
//    cmd.Parameters.AddWithValue("@Id", id);
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