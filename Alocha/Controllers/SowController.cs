using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.Domain.Entities;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.SowVM;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace Alocha.WebUi.Controllers
{
    [TraceFilter]
    [Authorize]
    public class SowController : Controller
    {
        private readonly ISowService _sowService;
        private readonly IAttachmentService _attachmentService;

        public SowController(ISowService sowService, IAttachmentService attachmentService)
        {
            _sowService = sowService;
            _attachmentService = attachmentService;
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
        public async Task<IActionResult> Create(SowCreateVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var validationResult = CustomNumberValidator.NumberValidation(model.Number);
            if (validationResult != null)
                ModelState.AddModelError("", validationResult);
            if(ModelState.IsValid)
            {
                var result = await _sowService.CreateSowAsync(model, currentUserId);
                if(result)
                    return RedirectToAction("IndexServerSide");
                ModelState.AddModelError("", "Locha o podanym numerze już istnieje.");
            }

            return RedirectToAction("IndexServerSide"); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _sowService.GetSowForEditAsync(id, currentUserId);
            if (model == null)
                return RedirectToAction("IndexServerSide");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SowEditVM model)
        {
            if(ModelState.IsValid)
            {
                var result = await _sowService.EditSowAsync(model);
                if(result)
                    return RedirectToAction("IndexServerSide");
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
            return RedirectToAction("IndexServerSide");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _sowService.DetailsSowAsync(id, currentUserId);
            if (model.SmallPigs.Count() > 0)
            {
                var average = 0.0;
                var count = 0;
                _sowService.CalcualteCountAndAverageSmallPigs(model.SmallPigs, ref average, ref count);
                ViewBag.AveragePigsCount = average;
                ViewBag.BornCount = count;
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Vaccinate()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _sowService.GetPregnancySows(currentUserId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> WasVaccinated(int id)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _sowService.SowWasVacinated(id, currentUserId);
            return RedirectToAction("Vaccinate");
        }

        [HttpGet]
        public async Task<IActionResult> DownloadAttachment()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var fileContents = await _attachmentService.GenerateSowsListPdfAttachmentAsync(currentUserId);            
            var fileName = string.Format("SpisLoch_{0}.pdf", DateTime.Today.ToShortDateString());

            return File(fileContents, "application/pdf", fileName);
        }

        [HttpGet]
        public IActionResult IndexServerSide()
        {
            var model = new SowCreateVM();

            return View(model);
        }

        [HttpPost]
        public IActionResult LoadData()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var sows = _sowService.GetAllSowsAsync(currentUserId).GetAwaiter().GetResult();

            var dtModel = DataTablesHelper.BindRequestForm(Request.Form);
            var sowsFilter = DataTablesHelper.FilterData(ref dtModel, sows);


            return Json(new { draw = dtModel.Draw, recordsFiltered = dtModel.RecordsTotal, recordsTotal = dtModel.RecordsTotal, data = sowsFilter });
        }
    }
}