using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Domain.Entities;
using Alocha.WebUi.Models.SowVM;
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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = new SowIndexVM()
            {
                Sows = await _sowService.GetAllSowsAsync(currentUserId)
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind(include:"Number, Status, IsRemoved")]SowIndexVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            if(ModelState.IsValid)
            {
                var result = await _sowService.CreateSowAsync(model, currentUserId);
                if(result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Locha o podanym numerze już istnieje.");
            }
            model.Sows = await _sowService.GetAllSowsAsync(currentUserId);
            return View("Index", model); 
        }
    }
}