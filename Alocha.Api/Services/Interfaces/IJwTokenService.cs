using Alocha.Api.DTOs.AuthenticationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Alocha.Api.Services.Interfaces
{
    public interface IJwTokenService
    {
        Task<bool> IsValidUserEmailAndPassword(string email, string password);
        Task<AccessTokenDTO> GenerateToken(string email);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        Task<AccessTokenDTO> GenerateFreshToken(ClaimsPrincipal principal, string refreshToken);

    }
}
