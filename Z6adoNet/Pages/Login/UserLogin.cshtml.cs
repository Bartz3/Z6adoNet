using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;
using System.Security.Claims;
using Zadanie6.Models;

namespace Zadanie6.Pages.Login
{
    public class UserLoginModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public string Message { get; set; }
        [BindProperty]
        public SiteUser user { get; set; }
        public UserLoginModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void OnGet()
        {
        }
        private bool ValidateUser(SiteUser UserToValidate)
        {
            List<SiteUser> siteUsers = new List<SiteUser>();

            string myCompanyDBcs = _configuration.GetConnectionString("MyCompanyDB");

            SqlConnection con = new SqlConnection(myCompanyDBcs);
            string sql = "SELECT * FROM SiteUser";
            SqlCommand cmd = new SqlCommand(sql, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            SiteUser _user;

            while (reader.Read())
            {
                _user = new SiteUser();
                _user.Id = int.Parse(reader["Id"].ToString());
                _user.userName = reader["userName"].ToString();
                _user.password = reader["password"].ToString();

                siteUsers.Add(_user);
            }
            reader.Close(); con.Close();

            foreach (var User in siteUsers)
            {

                string log = String.Concat(User.userName.Where(c => !Char.IsWhiteSpace(c)));
                string psswd = String.Concat(User.password.Where(c => !Char.IsWhiteSpace(c)));

                if ((UserToValidate.userName ==log ) && (UserToValidate.password == psswd))
                {
                    ViewData["userNick"]=UserToValidate.userName;
                    return true;

                }
            }
            return false;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl=null)
        {
            if (ValidateUser(user))
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, user.userName)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "CookieAuthentication");
                await HttpContext.SignInAsync("CookieAuthentication", new
                    ClaimsPrincipal(claimsIdentity));
                
                return RedirectToPage(returnUrl);
            }
            return Page();
        }


    }
}
