﻿using System;
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
            if (message == IdMessage.SendConfirmEmailSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Na Adres Email został wysłany link potwierdzajacy konto.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.SendConfirmEmailError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało się wysłać maila z potwierdzeniem, skontaktuj się proszę z administratorem.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.EmailConfirmedSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Dziękujemy za potwierdzenie konta, zostaniesz przekierowany do ekranu logowania.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.EmailConfirmedError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało się potwierdzic konta, skontaktuj się proszę z administartorem.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.ResetPasswordTokenSendSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Link do zmiany hasła został wysłany na podany adres Email.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.ResetPasswordTokenSendError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało sie wysłać liku do zmiany hasła.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.ResetPasswordSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Hasło zostało pomyślnie zresetowane.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "Wróć";
            }
            else
                return RedirectToAction("Index", "Home");

            return View(model);

        }
    }
}