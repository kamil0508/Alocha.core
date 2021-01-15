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
        public string DateHappening { get; set; }

        [Display(Name = "Data porodu")]
        public string DateBorn { get; set; }

        [Display(Name = "Data inseminacji")]
        public string DateInsimination { get; set; }

        [Display(Name = "Data oderwania")]
        public string DateDetachment { get; set; }

        [Display(Name = "Data szczepienia")]
        public string VaccineDate { get; set; }

        [Display(Name = "Szczepienie")]
        public bool IsVaccinated { get; set; }

        public bool IsEdit { get; set; }
    }
}
