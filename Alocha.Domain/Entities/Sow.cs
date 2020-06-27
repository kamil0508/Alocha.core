using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.Entities
{
    public class Sow
    {
        public int SowId { get; set; }

        public string Number { get; set; }

        public string Status { get; set; }

        public DateTime DateHappening { get; set; }

        public DateTime DateBorn { get; set; }

        public DateTime DateInsimination { get; set; }

        public DateTime DateDetachment { get; set; }

        public DateTime VaccineDate { get; set; }

        public string Result { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ICollection<SmallPig> SmallPigs { get; set; }
    }
}
