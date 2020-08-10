using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SowController : ControllerBase
    {
        private readonly ISowService _sowService;

        public SowController(ISowService sowService) => _sowService = sowService;


    }
}