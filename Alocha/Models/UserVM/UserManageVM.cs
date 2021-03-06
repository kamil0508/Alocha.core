﻿using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.UserVM
{
    public class UserManageVM
    {
        public string Email { get; set; }

        [Display(Name = "Numer telefonu")]
        [RegularExpression(@"\d{8}[0-9]", ErrorMessage = "Numer może się składać z liczb [0-9].")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Pole Stare hasło jest wymagane.")]
        [Display(Name = "Stare hasło")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Pole Nowe hasło jest wymagane.")]
        [Display(Name = "Nowe hasło")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Display(Name = "Potwierdzenie nowego hasło")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Hasła nie są takie same.")]
        public string ConfirmNewPassword { get; set; }

        [Display(Name = "Kod potwierdzający")]
        public string Token { get; set; }

    }
}
