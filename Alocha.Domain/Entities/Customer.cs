using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Nip { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public string Regon { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Sow> Sows { get; set; }
    }
}
