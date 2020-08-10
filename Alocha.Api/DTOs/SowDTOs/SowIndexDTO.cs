using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.DTOs.SowDTOs
{
    public class SowIndexDTO
    {
        public IEnumerable<SowDTO> Sows { get; set; }
    }
}
