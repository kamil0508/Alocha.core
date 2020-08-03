using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class SowCreateVM
    {
        public SowCreateVM() => IsRemoved = false;

        [Required(ErrorMessage = "Pole Numer jest wymagane.")]
        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Pole Stan lochy jet wymagane.")]
        [Display(Name = "Stan lochy")]
        public string Status { get; set; }

        public bool IsRemoved { get; set; }
    }
}
