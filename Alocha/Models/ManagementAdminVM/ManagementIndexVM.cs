using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.ManagementAdminVM
{
    public class ManagementIndexVM
    {
        public IEnumerable<ManagementVM> Users { get; set; }
    }
}
