using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Api.DTOs.SowDTOs;
using Alocha.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class SowController : ControllerBase
    {
        private readonly ISowService _sowService;

        public SowController(ISowService sowService) => _sowService = sowService;

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if (currentUserEmail != null)
            {
                var model = new SowIndexDTO()
                {
                    Sows = await _sowService.GetAllSowsAsync(currentUserEmail)
                };
                return Ok(model);
            }
            return BadRequest(new UnauthorizedAccessException());
        }
    }
}