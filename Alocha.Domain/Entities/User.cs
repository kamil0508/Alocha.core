using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.Entities
{
    public class User : IdentityUser 
    {
        public Customer Customer { get; set; }
    }
}
