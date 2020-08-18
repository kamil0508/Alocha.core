using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Alocha.Api.DTOs.SowDTOs;
using Alocha.Api.Services;
using AutoMapper.Configuration.Annotations;
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

        //GET Sow
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if (currentUserEmail != null)
            {
                var dto = new SowIndexDTO()
                {
                    Sows = await _sowService.GetAllSowsAsync(currentUserEmail)
                };
                return Ok(dto);
            }
            return BadRequest(new UnauthorizedAccessException());
        }

        //GET Sow/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneSow(int id)
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if (currentUserEmail != null)
            {
                var dto = await _sowService.GetOneSowAsync(currentUserEmail, id);
                if(dto != null)
                    return Ok(dto);
                return NotFound();
            }
            return BadRequest(new UnauthorizedAccessException());
        }

        //POST Sow
        [HttpPost]
        public async Task<IActionResult> CreateSow(SowCreateDTO dto)
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if(currentUserEmail != null)
            {
                var resultDto = await _sowService.AddSowAsync(dto, currentUserEmail);
                if (resultDto != null)
                {
                    return Created(string.Format("/Sow/{0}", resultDto.SowId), resultDto);
                }
                return BadRequest(string.Format("Sow with number {0} is exist", dto.Number));
            }
            return BadRequest(new UnauthorizedAccessException());
        }

        //PUT Sow
        [HttpPut]
        public async Task<IActionResult> UpdateSow(SowOneDTO dto)
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if(currentUserEmail != null)
            {
                var result = await _sowService.EditSowAsync(dto, currentUserEmail);
                if(result)
                    return NoContent();
                return NotFound();
            }
            return BadRequest(new UnauthorizedAccessException());
        }

        [HttpGet("PregnantSows")]
        public async Task<IActionResult> GetPregnantSow()
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if(currentUserEmail != null)
            {
                var dto = new SowIndexDTO()
                {
                    Sows = await _sowService.GetPregnantSows(currentUserEmail)
                };
                return Ok(dto);
            }
            return BadRequest(new UnauthorizedAccessException());
        }

        //DELETE Sow/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveSow(int id)
        {
            var currentUserEmail = User.Claims.ElementAt(0).Value;
            if (currentUserEmail != null)
            {
                var result = await _sowService.RemoveSowAync(currentUserEmail, id);
                if (result)
                    return NoContent();
                return NotFound();
            }
            return BadRequest(new UnauthorizedAccessException());
        }
    }
}