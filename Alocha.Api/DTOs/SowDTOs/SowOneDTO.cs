using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.DTOs.SowDTOs
{
    public class SowOneDTO
    {
        public int SowId { get; set; }

        [Required]
        [MaxLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "The number field can only contain numbers 0 - 9")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^Prośna|Laktacja|Luźna$", ErrorMessage = "The Status field can only contain 'Prośna', 'Luźna', 'Laktacja'.")]
        public string Status { get; set; }

        [Required]
        public DateTime DateHappening { get; set; }

        public DateTime? DateBorn { get; set; }

        public DateTime? DateInsimination { get; set; }

        public DateTime? DateDetachment { get; set; }

        public DateTime? VaccineDate { get; set; }

        public bool IsVaccinated { get; set; }
    }
}
