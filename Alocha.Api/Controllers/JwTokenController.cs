using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Api.DTOs;
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

        [HttpPost]
        public async Task<IActionResult> Create(UserCardDTO dto)
        {
            var result = await _jwTokenService.IsValidUserEmailAndPassword(dto.Email, dto.Password);
            if (result)
            {
                var token = await _jwTokenService.GenerateToken(dto.Email);
                return Ok(token);
            }
            else
            {
                return NotFound();    
            }
        }
    }
}