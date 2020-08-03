using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class SowIndexVM
    {
        public SowCreateVM SowCreateVM { get; set; }

        public IEnumerable<SowVM> Sows { get; set; }
    }
}
