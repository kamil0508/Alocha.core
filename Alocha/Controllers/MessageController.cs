using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Alocha.WebUi.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index(IdMessage message)
        {
            MessageVM model = new MessageVM();
            if (message == IdMessage.AccountLock)
            {
                model.Topic = "Blokada";
                model.Message = "Twoje konto zostało zablokowane spróbuj ponownie później.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.SendConfirmEmail)
            {
                model.Topic = "Powodzenie";
                model.Message = "Na Adres Email został wysłany link potwierdzajacy konto.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
                return RedirectToAction("Index", "Home");

            return View(model);

        }
    }
}