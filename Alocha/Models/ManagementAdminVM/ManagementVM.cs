using Microsoft.AspNetCore.Cors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.ManagementAdminVM
{
    public class ManagementVM
    {
        [Display(Name = "Adres Email")]
        public string Email { get; set; }

        [Display(Name = "Potwierdzenie Email")]
        public bool EmailConfirmed { get; set; }

        [Display(Name = "Potwierdzenie numeru telefonu")]
        public bool PhoneNumberConfirmed { get; set; }

        [Display(Name = "Liczba nieudanych prób logowania")]
        public int AccessFailedCount { get; set; }

        [Display(Name = "Czas blokady konta")]
        public DateTimeOffset LockoutEnd { get; set; }
    }
}
