using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.Api.Services.Interfaces
{
    public interface IJwTokenService
    {
        Task<bool> IsValidUserEmailAndPassword(string email, string password);
        Task<string> GenerateToken(string email);
    }
}
