using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.AccountVM
{
    public class ForgotPasswordVM
    {
        [Required(ErrorMessage = "Pole Adres email jest wymagane.")]
        [Display(Name = "Adres Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
