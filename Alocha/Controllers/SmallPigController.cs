using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    public class SmallPigController : Controller
    {
        private readonly ISmallPigService _smallPigService;

        public SmallPigController(ISmallPigService smallPigService)
        {
            _smallPigService = smallPigService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}