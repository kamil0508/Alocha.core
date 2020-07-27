using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Alocha.Models;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account.Manage;
using Alocha.WebUi.Services.Interfaces;

namespace Alocha.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService _homeService;

        public HomeController(IHomeService homeService)
        {
            _homeService = homeService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> UpcomingTask()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _homeService.GetUpcomingTaskAsync(currentUserId);
            return View(model);
        }

        [HttpGet]
        public async Task<string> UpcomingTaskCount()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var count = await _homeService.GetUpcomingTaskCountAsync(currentUserId);
            return count;
        }

    }
}
