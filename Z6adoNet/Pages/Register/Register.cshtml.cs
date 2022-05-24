using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
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
                //SiteUser siteUser = new SiteUser();
                //siteUser
                string zad10cs = _configuration.GetConnectionString("MyCompanyDB");
                SqlConnection con = new SqlConnection(zad10cs);
                SqlCommand cmd = new SqlCommand("sp_userAdd", con);

                cmd.Parameters.AddWithValue("@password", user.password);
                cmd.Parameters.AddWithValue("@userName", user.userName);
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