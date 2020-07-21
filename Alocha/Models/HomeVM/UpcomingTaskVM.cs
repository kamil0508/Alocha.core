using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.HomeVM
{
    public class UpcomingTaskVM
    {
        public int SowId { get; set; }

        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Display(Name = "Stan")]
        public string Status { get; set; }

        [Display(Name = "Data zdarzenia")]
        [DataType(DataType.Date)]
        public DateTime DateHappening { get; set; }

        [Display(Name = "Data porodu")]
        [DataType(DataType.Date)]
        public DateTime DateBorn { get; set; }

        [Display(Name = "Data inseminacji")]
        [DataType(DataType.Date)]
        public DateTime DateInsimination { get; set; }

        [Display(Name = "Data oderwania")]
        [DataType(DataType.Date)]
        public DateTime DateDetachment { get; set; }

        [Display(Name = "Data szczepienia")]
        [DataType(DataType.Date)]
        public DateTime VaccineDate { get; set; }

        public bool IsVaccinated { get; set; }
    }
}
