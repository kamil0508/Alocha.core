using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.UserVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
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
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ISmsService smsService, ILogger<UserController> logger)
        {
            _userService = userService;
            _smsService = smsService;
            _logger = logger;
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
                _logger.LogError(string.Format("The confirmation SMS has not been sent to the user : {0}.", User.Identity.Name));
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

        public async Task<IActionResult> Delete([Bind(include: "Email")] UserManageVM model)
        {
            var currentUserId = User.Claims.ElementAt(0).Value;
            var result = await _userService.DeleteUserAsync(model.Email, currentUserId);
            if (result.Succeeded)
            {
                _logger.LogInformation("User with email: {0} delete account.", model.Email);
                return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminDeleteAccountSucces });
            }
            _logger.LogError("User with email: {0} delete account errors: {1}", model.Email, result.Errors);
            return View(model);
        }
    }
}