using System.Data.SqlClient;
using System.Text;
using Zadanie6.Models;

namespace Zadanie6.Models
{
    public class MyPageModel
    {

        private readonly ILogger<MyPageModel> _logger;
        private List<Product> products;
        public IConfiguration _configuration { get; }

        public MyPageModel(IConfiguration configuration, ILogger<MyPageModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }
        public void Load()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Product";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder htmlStr = new StringBuilder("");
            Product _product = new Product();
            while (reader.Read())
            {
                _product.id = int.Parse(reader["Id"].ToString());
                _product.name = reader["Name"].ToString();
                _product.description = reader["Description"].ToString();
                _product.price = Decimal.Parse(reader["Price"].ToString());
                products.Add(_product);
            }
            reader.Close(); con.Close();
        }
        public List<Product> List()
        {
            return products;
        }

    }
}
