using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.DTOs.SowDTOs
{
    public class SowDTO
    {
        public int SowId { get; set; }

        public string Number { get; set; }

        public string Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateHappening { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBorn { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateInsimination { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateDetachment { get; set; }

        [DataType(DataType.Date)]
        public DateTime VaccineDate { get; set; }

        [Display(Name = "Szczepienie")]
        public bool IsVaccinated { get; set; }

        public bool IsEdit { get; set; }
    }
}
