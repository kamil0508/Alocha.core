using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models.AccountVM;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LogIn(LogInVM model)
        {
            if(ModelState.IsValid)
            {
                var result = await _accountService.LogInAsync(model);
                //if (result.Succeeded)
                //    return RedirectToAction();
                //if (result.IsLockedOut)
                    return RedirectToAction("Index", "Message", new { Message = IdMessage.AccountLock });
                //ModelState.AddModelError("", "Niepoprawny login lub hasło");
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
    }
}