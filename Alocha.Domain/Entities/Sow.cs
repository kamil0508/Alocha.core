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

        public DateTime? DateHappening { get; set; }

        public DateTime? DateBorn { get; set; }

        public DateTime? DateInsimination { get; set; }

        public DateTime? DateDetachment { get; set; }

        public DateTime? VaccineDate { get; set; }

        public bool IsVaccinated { get; set; }

        public string UserId{ get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SmallPig> SmallPigs { get; set; }

        public bool IsRemoved { get; set; }

        public bool IsEdit { get; set; }
    }
}
