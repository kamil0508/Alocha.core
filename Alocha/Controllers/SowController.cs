using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Domain.Entities;
using Alocha.WebUi.Helpers;
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
            var validationResult = CustomNumberValidator.NumberValidation(model.Number);
            if (validationResult != null)
                ModelState.AddModelError("", validationResult);
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

        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _sowService.GetSowForEditAsync(id, currentUserId);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SowEditVM model)
        {
            if(ModelState.IsValid)
            {
                var result = await _sowService.EditSowAsync(model);
                if(result)
                    return RedirectToAction("Index");
                ModelState.AddModelError("", "Niestety nie udało się dokonać zmian");
            }
            return View(model);
        }

        public string CalculateDate(DateTime date,string status)
        {
            var response = _sowService.CalculateDate(date, status);
            return response;
        }

        public async Task<IActionResult> Remove(int id)
        {
            var result = await _sowService.RemoveSowAsync(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _sowService.DetailsSowAsync(id, currentUserId);
            return View(model);
        }


    }
}