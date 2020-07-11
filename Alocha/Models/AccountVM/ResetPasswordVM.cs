using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Alocha.WebUi.Models.AccountVM
{
    public class ResetPasswordVM
    {
        [Required(ErrorMessage = "Pole Adres email jest wymagane.")]
        [Display(Name = "Adres Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Pole Hasło jest wymagane.")]
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Potwierdzenie hasła")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Hasła nie sa takie same.")]
        public string ConfirmPassword { get; set; }

        public string Token { get; set; }
    }
}
