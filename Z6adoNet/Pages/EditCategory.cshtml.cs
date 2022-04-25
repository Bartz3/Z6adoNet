using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class EditCategoryModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public Category editCategory { get; set; }

        public IConfiguration _configuration { get; }
        private readonly ILogger<EditModel> _logger;
        public EditCategoryModel(IConfiguration configuration, ILogger<EditModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet(int id)
        {
            editCategory.id = id;
        }
        public IActionResult OnPost(Category c)
        {
            c.id = editCategory.id;
            c.shortName = editCategory.shortName;
            c.longName=editCategory.longName;


            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");
            SqlConnection con = new SqlConnection(myCompanyDBcs);

            SqlCommand cmd = new SqlCommand("sp_categoryUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter name_SqlParam = new SqlParameter("@shortName", SqlDbType.VarChar,
            30);
            name_SqlParam.Value = c.shortName;
            cmd.Parameters.Add(name_SqlParam);

            SqlParameter longName_SqlParam = new SqlParameter("@longName", SqlDbType.VarChar,
            80);
            longName_SqlParam.Value = c.longName;
            cmd.Parameters.Add(longName_SqlParam);

            SqlParameter categoryID_SqlParam = new SqlParameter("@categoryID",
            SqlDbType.Int);
            //productID_SqlParam.Direction = ParameterDirection.Input;
            categoryID_SqlParam.Value = c.id;
            cmd.Parameters.Add(categoryID_SqlParam);

            con.Open();
            int numAff = cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToPage("CategoryList");
        }

    }

}
