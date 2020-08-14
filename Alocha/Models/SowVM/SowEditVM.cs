using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class SowEditVM
    {
        public SowEditVM()
        {
            IsEdit = true;
            IsSmsSend = false;
        }

        public int SowId { get; set; }

        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Required(ErrorMessage = "Pole Wydarzenie jest wymagane.")]
        [Display(Name = "Wydarzenie:")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Pole Data zdarzenia jest wymagane.")]
        [Display(Name = "Data zdarzenia")]
        [DataType(DataType.Date)]
        public DateTime DateHappening { get; set; }

        [Display(Name = "Data porodu")]
        public DateTime? DateBorn { get; set; }

        [Display(Name = "Data inseminacji")]
        public DateTime? DateInsimination { get; set; }

        [Display(Name = "Data oderwania")]
        public DateTime? DateDetachment { get; set; }

        [Display(Name = "Data szczepienia")]
        public DateTime? VaccineDate { get; set; }

        [Display(Name = "Szczepienie(ilość dni przed porodem)")]
        public int VaccineDays { get; set; }

        public bool IsVaccinated { get; set; }

        public bool IsEdit { get; set; }

        public bool IsSmsSend { get; set; }

        public DateTime BornDate { get; set; }

        [Display(Name = "Ilość prosiąt")]
        public int? PigsQuantity { get; set; }
    }
}
