using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.Entities
{
    public class User : IdentityUser 
    {
        public string RefreshToken { get; set; }

        public virtual ICollection<Sow> Sows { get; set; }
    }
}
