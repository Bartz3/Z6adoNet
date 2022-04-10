using System.ComponentModel.DataAnnotations;

namespace Zadanie6.Models
{
    public class Product
    {
        [Display(Name = "Id")]
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage = "Pole Nazwa jest obowiązkowe!"), Display(Name = "Nazwa")]
        public string name { get; set; }
        [Required(ErrorMessage = "Pole Cena jest obowiązkowe!"), Display(Name = "Cena"),
            Range(0, double.MaxValue, ErrorMessage = "Podano niepoprawną cene."), DataType(DataType.Currency, ErrorMessage = "Podana wartość jest złahaha")]
        public decimal price { get; set; }
        public string? description { get; set; }
        public static List<Product> GetProducts()
        {
            Product pilka = new Product { id = 1, name = "Piłka nożna", price = 25.30M, description = "Czerwona piłka" };
            Product narty = new Product { id = 2, name = "Narty", price = 150.39M, description = "Narty zimowe dla dzieci" };
            Product rakieta = new Product { id = 3, name = "Rakieta ", price = 111.10M, description = "Rakieta do tenisa" };

            return new List<Product> { pilka, narty, rakieta };
        }

    }

}
