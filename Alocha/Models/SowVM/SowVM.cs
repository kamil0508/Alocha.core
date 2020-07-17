using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class SowVM
    {
        public int SowId { get; set; }

        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Display(Name = "Stan")]
        public string Status { get; set; }

        [Display(Name = "Data zdarzenia")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateHappening { get; set; }

        [Display(Name = "Data porodu")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateBorn { get; set; }

        [Display(Name = "Data inseminacji")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateInsimination { get; set; }

        [Display(Name = "Data oderwania")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateDetachment { get; set; }

        [Display(Name = "Data szczepienia")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime VaccineDate { get; set; }

        public bool IsVaccinated { get; set; }
    }
}
