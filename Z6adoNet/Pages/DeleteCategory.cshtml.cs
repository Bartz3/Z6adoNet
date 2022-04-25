using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class DeleteCategoryModel : PageModel
    {
        public IConfiguration _configuration { get; }
        private readonly ILogger<DeleteModel> _logger;
        public DeleteCategoryModel(IConfiguration configuration, ILogger<DeleteModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public Category deleteCategory = new Category();
        public void OnGet(int id)
        {
            deleteCategory.id = id;
        }
        public IActionResult OnPost(int id)
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_categoryDelete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter name_SqlParam = new SqlParameter("@categoryID", SqlDbType.Int);
            name_SqlParam.Value = id;
            cmd.Parameters.Add(name_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();

            return RedirectToPage("CategoryList");
        }
    }
}
