using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.AccountVM;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Alocha.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccountController> _loger;

        public AccountController(IAccountService accountService, IEmailService emailService, ILogger<AccountController> loger)
        {
            _accountService = accountService;
            _emailService = emailService;
            _loger = loger;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountService.LogInAsync(model);
                if (result.Succeeded)
                {
                    _loger.LogInformation(string.Format("The user: {0} logs in.", model.Email));
                    if (!string.IsNullOrEmpty(returnUrl))
                        return LocalRedirect(returnUrl);
                    return RedirectToAction("Index", "Home");
                }
                if (result.IsLockedOut)
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.AccountLock });
                if(result.IsNotAllowed)
                    ModelState.AddModelError("", "Adres Email nie został potwierdzony.");
                else
                ModelState.AddModelError("", "Niepoprawny login lub hasło");
            }
            return View(model);
        }

        public async Task<IActionResult> LogOut()
        {
            var result = await _accountService.LogOutAsync();
            if (result)
                return RedirectToAction("LogIn");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.RegisterAsync(model);
                if (result.Succeeded)
                {
                    _loger.LogInformation(string.Format("The user: {0} is registering.", model.Email));
                    //send confirmation email
                    var user = await _accountService.GetUserByEmailAsync(model.Email);
                    var token = await _accountService.GenerateConfirmTokenAsync(user);
                    var callBackUrl = Url.ActionLink("Confirmed", "Account", new { Token = token, UserId = user.Id });
                    var resultEmail = await _emailService.SendConfirmEmailAsync(callBackUrl, user.Email);
                    if(resultEmail)
                        return RedirectToAction("Index", "Message", new { Message = IdMessage.SendConfirmEmailSucces });
                    _loger.LogError(string.Format("The confirmation Email has not been sent to the user : {0}.", model.Email));
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.SendConfirmEmailError });
                }
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Confirmed(string token, string userId)
        {
            var result = await _accountService.ConfirmEmailAsync(token, userId);
            if (result.Succeeded)
                return RedirectToAction("Index", "Message", new { Message = IdMessage.EmailConfirmedSucces });
            return RedirectToAction("Index", "Message", new { Message = IdMessage.EmailConfirmedError });
        }

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var token = await _accountService.GenerateResetPasswordTokenAsync(model);
                if (token != null)
                {
                    var user = await _accountService.GetUserByEmailAsync(model.Email);
                    var callbackUrl = Url.ActionLink("ResetPassword", "Account", new { Token = token, UserId = user.Id });
                    var resultEmail = await _emailService.SendResetPasswordEmailAsync(callbackUrl, user.Email);
                    if (resultEmail)
                        return RedirectToAction("Index", "Message", new { Message = IdMessage.ResetPasswordTokenSendSucces});
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.ResetPasswordTokenSendError });
                }
                ModelState.AddModelError("", "Niepoprawny adres E-mail");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string userId)
        {
            if (token == null || userId == null)
                ModelState.AddModelError("", "Nieprawidłowy token do zmiany hasła.");
            var model = new ResetPasswordVM()
            {
                Token= token
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountService.ResetPasswordAsync(model);
                if(result.Succeeded)
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.ResetPasswordSucces });
                result.Errors.ToList().ForEach(e => ModelState.AddModelError("", e.Description));
            }
            return View(model);
        }
    }
}