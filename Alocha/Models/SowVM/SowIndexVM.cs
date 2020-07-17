using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class SowIndexVM
    {

        [Required(ErrorMessage = "Pole Numer jest wymagane.")]
        [Display(Name = "Numer")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Pole Stan lochy jet wymagane.")]
        [Display(Name = "Stan lochy")]
        public string Status { get; set; }

        public bool IsRemoved { get; set; }

        public IEnumerable<SowVM> Sows { get; set; }
    }
}
