using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;
using System.Data.SqlClient;
using Zadanie6.Models;
namespace Zadanie6.Pages
{
    public class CategoryListModel : PageModel
    {
        [BindProperty]
        public Category category { get; set; }
        public List<Category> categoryList = new List<Category>();
        public IConfiguration _configuration { get; }
        private readonly ILogger<ListModel> _logger;
        public CategoryListModel(IConfiguration configuration, ILogger<ListModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            SqlCommand cmd = new SqlCommand("sp_categoryDisplay", con);
            cmd.CommandType = CommandType.StoredProcedure;

            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            Category _category;
            while (reader.Read())
            {
                _category = new Category();
                _category.id = int.Parse(reader["Id"].ToString());
                _category.shortName = reader["shortName"].ToString();
                _category.longName = reader["longName"].ToString();

                categoryList.Add(_category);
            }
            reader.Close(); con.Close();
        }
        public IActionResult OnPost()
        {
            return RedirectToPage("List");
        }
    }
}
