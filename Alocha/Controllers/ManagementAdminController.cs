using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.ManagementAdminVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ManagementAdminController : Controller
    {
        private readonly IManagementAdminService _managementAdminService;
        private readonly IEmailService _emailService;

        public ManagementAdminController(IManagementAdminService managementAdminService, IEmailService emailService)
        {
            _managementAdminService = managementAdminService;
            _emailService = emailService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ManagementIndexVM()
            {
                Users = await _managementAdminService.GetAllUsersAsync()
            };
            return View(model);
        }

        public async Task<IActionResult> SendConfirm(string id)
        {
            var user = await _managementAdminService.GetUserByEmailAsync(id);
            var token = await _managementAdminService.GenerateConfirmTokenAsync(user);
            var callBackUrl = Url.ActionLink("Confirmed", "Account", new { Token = token, UserId = user.Id });
            var resultEmail = await _emailService.SendConfirmEmailAsync(callBackUrl, user.Email);
            if (resultEmail)
                return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminSendConfirmationEmailSucces });
            return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminSendConfirmationEmailError });
        }
    }
}