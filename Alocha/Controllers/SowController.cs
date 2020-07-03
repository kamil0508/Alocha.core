using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Domain.Entities;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    public class SowController : Controller
    {
        private readonly ISowService _sowService;

        public SowController(ISowService sowService)
        {
            _sowService = sowService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}