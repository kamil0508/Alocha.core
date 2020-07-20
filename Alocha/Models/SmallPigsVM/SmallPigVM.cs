using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SmallPigsVM
{
    public class SmallPigVM
    {
        public int SmalPigsId { get; set; }

        [Display(Name = "Data porodu")]
        [DataType(DataType.Date)]
        public DateTime BornDate { get; set; }

        [Display(Name = "Ilość prosiąt")]
        public int PigsQuantity { get; set; }
    }
}
