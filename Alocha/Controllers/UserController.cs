using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.UserVM;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Twilio.TwiML.Voice;

namespace Alocha.WebUi.Controllers
{
    [TraceFilter]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ISmsService _smsService;
        private readonly ILogger<UserController> _loger;

        public UserController(IUserService userService, ISmsService smsService, ILogger<UserController> loger)
        {
            _userService = userService;
            _smsService = smsService;
            _loger = loger;
        }

        public async Task<IActionResult> Index()
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var model = await _userService.GetUserPhoneNumberEmailByIdAsync(currentUserId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserManageVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _userService.ChangePasswordAsync(model, currentUserId);
            if (result.Succeeded)
                return RedirectToAction("Index", "Message", new { Message = IdMessage.ChangePasswordSucces});
            result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
            return View("Index",model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoneNumber(UserManageVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _userService.AddPhoneNumberAsync(model, currentUserId);
            if (result.Succeeded)
            {
                var code = await _userService.GenerateConfirmedPhoneNumberCodeAsync(model.PhoneNumber, currentUserId);
                var smsResult = await _smsService.SendConfirmPhoneNumberSmsAsync(model.PhoneNumber, code);
                if(smsResult != null)
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.AddPhoneNumberSucces });
                _loger.LogError(string.Format("The confirmation SMS has not been sent to the user : {0}.", User.Identity.Name));
            }
            result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
            return View("Index", model);
        }

        [HttpGet]
        public IActionResult ConfirmPhoneNumber()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPhoneNumber(UserManageVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _userService.ConfirmPhoneNumberAsync(model.Token, currentUserId);
            if (result.Succeeded)          
                return RedirectToAction("Index", "Message", new { Message = IdMessage.ConfirmedPhoneNumberSucces });         
            return RedirectToAction("Index", "Message", new { Message = IdMessage.ConfirmedPhoneNumberError });
        }


        public async Task<IActionResult> RemovePhoneNumber(UserManageVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _userService.RemovePhoneNumberAsync(model, currentUserId);
            if (result.Succeeded)
                return RedirectToAction("Index", "Message", new { Message = IdMessage.RemovePhoneNumberSucces });
            result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
            return View("Index", model);
        }
    }
}