﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Z6adoNet.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IConfiguration _configuration { get; } 
        public IndexModel(IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public string lblInfoText;
        public void OnGet()
        {
            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM Product";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder htmlStr = new StringBuilder("");
            while (reader.Read())
            {
                htmlStr.Append("<li>");
                htmlStr.Append(reader["Id"].ToString() + " ");
                htmlStr.Append(reader.GetString(1) + " ");
                htmlStr.Append(String.Format("{0:0.00}",
                Decimal.Parse(reader["Price"].ToString())));
                htmlStr.Append("</li>");
            }
            reader.Close(); con.Close();
            lblInfoText = htmlStr.ToString();

        }
    }
}