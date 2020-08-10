using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Api.DTOs;
using Alocha.Api.DTOs.AuthenticationDTOs;
using Alocha.Api.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class JwTokenController : ControllerBase
    {
        private readonly IJwTokenService _jwTokenService;

        public JwTokenController(IJwTokenService jwTokenService)
        {
            _jwTokenService = jwTokenService;
        }

        [HttpPost("CreateToken")]
        public async Task<IActionResult> SignIn(UserCardDTO dto)
        {
            var result = await _jwTokenService.IsValidUserEmailAndPassword(dto.Email, dto.Password);
            if (result)
            {
                var token = await _jwTokenService.GenerateToken(dto.Email);
                if(token != null)
                    return Ok(token);
                return NotFound();
            }
            else
            {
                return NotFound();    
            }
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh(AccessTokenDTO dto)
        {
            var principal = _jwTokenService.GetPrincipalFromExpiredToken(dto.AccessToken);
            if(principal != null)
            {
                var token = await _jwTokenService.GenerateFreshToken(principal, dto.RefreshToken);
                if (token != null)
                    return Ok(token);
            }
            return NotFound();
        }
    }
}