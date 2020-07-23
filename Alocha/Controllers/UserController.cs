﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.UserVM;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
                var smsResult = SmsSender.SendSmsAsync(string.Format("48{0}", model.PhoneNumber), string.Format("Twój kod potwierdzający to:{0}", code));
                return RedirectToAction("Index", "Message", new { Message = IdMessage.AddPhoneNumberSucces });
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