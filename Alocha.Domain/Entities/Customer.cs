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
        public User User { get; set; }

        public ICollection<Sow> Sows { get; set; }
    }
}
