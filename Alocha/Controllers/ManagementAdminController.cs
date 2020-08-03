using System.Threading.Tasks;
using Alocha.Domain.Entities;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.ManagementAdminVM;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Data;

namespace Alocha.WebUi.Controllers
{
    [TraceFilter]
    [Authorize(Roles = "Administrator")]
    public class ManagementAdminController : Controller
    {
        private readonly IManagementAdminService _managementAdminService;
        private readonly IEmailService _emailService;
        private readonly ILogger<ManagementAdminController> _logger;

        public ManagementAdminController(IManagementAdminService managementAdminService, IEmailService emailService, ILogger<ManagementAdminController> logger)
        {
            _managementAdminService = managementAdminService;
            _emailService = emailService;
            _logger = logger;
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

        public async Task<IActionResult> Delete(string id)
        {
            var user = (User)await _managementAdminService.GetUserByEmailAsync(id);
            var result = await _managementAdminService.DeleteUserAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("Delete user: {0}", id);
                return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminDeleteAccountSucces });
            }
            _logger.LogError("Delete user: {0} errors: {1}", id, result.Errors);
            return RedirectToAction("Index", "Message", new { Message = IdMessage.AdminDeleteAccountError });
        }
    }
}