using System.ComponentModel.DataAnnotations;

namespace Zadanie6.Models
{
    public class Category
    {
        [Display(Name = "Id")]
        [Key]
        public int id { get; set; }

        [Required(ErrorMessage = "Pole shortName jest obowiązkowe!"), Display(Name = "Nazwa krótka")]
        public string shortName { get; set; }

        [Required(ErrorMessage = "Pole longName jest obowiązkowe!"), Display(Name = "Nazwa długa")]
        public string longName { get; set; }
    }
}
