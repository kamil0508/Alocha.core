using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.Entities
{
    public class User : IdentityUser 
    {
        public virtual ICollection<Sow> Sows { get; set; }
    }
}
