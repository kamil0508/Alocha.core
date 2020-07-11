using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.Entities
{
    public class SmallPig
    {
        public int SmalPigsId { get; set; }

        public DateTime BornDate { get; set; }

        public int PigsQuantity { get; set; }

        public int SowId { get; set; }
        public virtual Sow Sow { get; set; }

        public bool IsRemoved { get; set; }
    }
}
