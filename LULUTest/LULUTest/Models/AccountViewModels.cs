using System.ComponentModel.DataAnnotations;

namespace LULUTest.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [Display(Name = "Логин")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомнить")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage ="Влогине можно использовать только латиницу")]
        public string Login { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(40, ErrorMessage = "{0} должен быть минимум {2} символов.", MinimumLength = 2)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(40, ErrorMessage = "{0} должен быть минимум {2} символов.", MinimumLength = 2)]
        public string SecondName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(15, ErrorMessage = "{0} должен быть минимум {2} символов.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        [RegularExpression(@"(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{8,15})$", ErrorMessage ="Пароль должен содержать цыфри и буквы в верхнем и нижнем регистре и состоять из латинских символов")]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(13, ErrorMessage = "{0} должен быть минимум {2} символов.", MinimumLength = 10)]
        [RegularExpression(@"(^(\+?\-? *[0-9]+)([,0-9 ]*)([0-9 ])*$)|(^ *$)", ErrorMessage = "Неверный формат номера")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Поле обязательно к заполнению")]
        [StringLength(300, ErrorMessage = "{0} должен быть минимум {2} символов.", MinimumLength = 10)]
        public string Adress { get; set; }

    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
