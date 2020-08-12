using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.DTOs.SowDTOs
{
    public class SowCreateDTO
    {
        [Required]
        [MaxLength(4)]
        [RegularExpression(@"^\d{4}$", ErrorMessage = "The Number field can only contain numbers 0 - 9")]
        public string Number { get; set; }

        [Required]
        [RegularExpression(@"^Prośna|Laktacja|Luźna$", ErrorMessage = "The Status field can only contain 'Prośna', 'Luźna', 'Laktacja'.")]
        public string Status { get; set; }
    }
}
