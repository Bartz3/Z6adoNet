using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Cryptography;
using System.Data.SqlClient;
using Zadanie6.Models;  


namespace Zadanie6.Pages.Register
{
    public class RegisterModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        public string passwd2 { get; set; }

        public RegisterModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (user.password == passwd2)
            {
                var hash = SecurePasswordHasher.Hash(user.password);
                string zad10cs = _configuration.GetConnectionString("MyCompanyDB");

                SqlConnection con = new SqlConnection(zad10cs);
                SqlCommand cmd = new SqlCommand("sp_userAdd", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter name_SqlParam = new SqlParameter("@userName", SqlDbType.VarChar,
                100);
                name_SqlParam.Value = user.userName;
                cmd.Parameters.Add(name_SqlParam);

                SqlParameter password_SqlParam = new SqlParameter("@password", SqlDbType.VarChar,
                100);
                password_SqlParam.Value = hash;
                cmd.Parameters.Add(password_SqlParam);

                SqlParameter productID_SqlParam = new SqlParameter("@Id",
                SqlDbType.Int);
                productID_SqlParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(productID_SqlParam);
                con.Open();
                int numAff = cmd.ExecuteNonQuery();
                con.Close();

                return RedirectToPage("/Index");
            }
            else return Page();
        }


    }
}
/*
 CREATE PROCEDURE [dbo].[sp_userAdd]
@userName VARCHAR (50),
@password VARCHAR (50),
@Id int OUTPUT
AS
INSERT INTO SiteUser (userName, password) VALUES (@userName, @password)
SET @Id = @@IDENTITY

CREATE PROCEDURE [dbo].[sp_productDisplay]
AS
SELECT *
FROM Product

CREATE PROCEDURE [dbo].[sp_productUpdate]
@name VARCHAR (50),
@price MONEY,
@productID int
AS
UPDATE Product SET price= @price, name=@name WHERE Id=@productID

CREATE PROCEDURE [dbo].[sp_productDelete]
@productID int OUTPUT
AS
DELETE FROM Product WHERE Id = @productID

CREATE PROCEDURE [dbo].[sp_productAdd]
@name VARCHAR (50),
@price MONEY,
@productID int OUTPUT
AS
INSERT INTO Product (name, price) VALUES (@name, @price)
SET @productID = @@IDENTITY

 
 
 */