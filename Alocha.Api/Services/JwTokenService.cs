using Alocha.Api.DTOs.AuthenticationDTOs;
using Alocha.Api.Helpers;
using Alocha.Api.Services.Interfaces;
using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Castle.Core.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Alocha.Api.Services
{
    public class JwTokenService : IJwTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public JwTokenService(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<bool> IsValidUserEmailAndPassword(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return await _userManager.CheckPasswordAsync(user, password); 
        }

        public async Task<AccessTokenDTO> GenerateToken(string email)
        {
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == email);
            if (user != null)
            {
                var role = await _userManager.GetRolesAsync(user);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("Tomojsekretnyklucz");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Token contain
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, email),
                        new Claim(ClaimTypes.Role, role[0])
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                                         SecurityAlgorithms.HmacSha256)

                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var writeToken = tokenHandler.WriteToken(token);
                var refreshToken = GenerateRefreshTokenJwt.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                await _unitOfWork.SaveChangesAsync();

                return new AccessTokenDTO()
                {
                    AccessToken = writeToken,
                    RefreshToken = refreshToken
                };
            }
            return null;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Tomojsekretnyklucz")),
                ValidateLifetime = false //here we are saying that we don't care about the token's expiration date
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Nieważny token");

            return principal;
        }

        public async Task<AccessTokenDTO> GenerateFreshToken(ClaimsPrincipal principal, string refreshToken)
        {
            var username = principal.Claims.ElementAt(0).Value;
            var user = await _unitOfWork.User.FindOneAsync(u => u.Email == username); //retrieve the refresh token from a data store
            if (user != null)
            {
                if (user.RefreshToken != refreshToken)
                    throw new SecurityTokenException("Nieważny token odświeżania");

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("Tomojsekretnyklucz");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    //Token contain
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, principal.Claims.ElementAt(0).Value),
                    new Claim(ClaimTypes.Role, principal.Claims.ElementAt(1).Value)
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                             SecurityAlgorithms.HmacSha256)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var writeToken = tokenHandler.WriteToken(token);
                var newRefreshToken = GenerateRefreshTokenJwt.GenerateRefreshToken();
                user.RefreshToken = newRefreshToken;
                await _unitOfWork.SaveChangesAsync();

                return new AccessTokenDTO()
                {
                    AccessToken = writeToken,
                    RefreshToken = newRefreshToken
                };
            }
            return null;
        }
    }
}
