using System;
using System.ComponentModel.DataAnnotations;

namespace LULUTest.Models
{
    public class BookModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; }

        public DateTime PubDate { get; set; }
    }   
    
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Автор")]
        public string Author { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Цена")]
        public int Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Количество")]
        public int Quantity { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [Display(Name = "Дата публикации")]
        public DateTime PubDate { get; set; }
    }
}