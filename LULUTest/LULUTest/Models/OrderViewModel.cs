using System.ComponentModel.DataAnnotations;

namespace LULUTest.Models
{
    public class OrderViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Адрес")]
        public string Adress { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Город")]
        public string City { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Индекс")]
        [RegularExpression(@"[^0-9]|\S", ErrorMessage = "Индекс может содержать только цыфры")]
        public string PostalCode { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [EmailAddress(ErrorMessage = "Неверный адрес почты")]
        public string Email { get; set; }
    }
}