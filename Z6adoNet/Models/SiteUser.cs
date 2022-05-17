using System.ComponentModel.DataAnnotations;

namespace Zadanie6.Models
{
    public class SiteUser
    {
        [Display(Name = "Nazwa użytkownika")]
        public string userName { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
