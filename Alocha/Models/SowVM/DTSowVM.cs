using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Models.SowVM
{
    public class DTSowVM
    {
        public string Draw { get; set; }

        public string Start { get; set; }

        public string Lenght { get; set; }

        public string SortColumn { get; set; }

        public string SortColumnDir { get; set; }

        public int PageSize { get; set; }

        public int Skip { get; set; }

        public int RecordsTotal { get; set; }

        public string NumberFilter { get; set; }
    }
}
