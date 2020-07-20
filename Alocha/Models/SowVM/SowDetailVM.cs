using Alocha.WebUi.Models.SmallPigsVM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class SowDetailVM
    {
        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Display(Name = "Stan")]
        public string Status { get; set; }

        [Display(Name = "Data zdarzenia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateHappening { get; set; }

        [Display(Name = "Data porodu")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateBorn { get; set; }

        [Display(Name = "Data inseminacji")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateInsimination { get; set; }

        [Display(Name = "Data oderwania")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime DateDetachment { get; set; }

        [Display(Name = "Data szczepienia")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime VaccineDate { get; set; }

        [Display(Name = "Zaszczepiona?")]
        public bool IsVaccinated { get; set; }

        public IEnumerable<SmallPigVM> SmallPigs { get; set; }
    }
}
