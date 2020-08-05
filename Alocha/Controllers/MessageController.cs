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
            if (message == IdMessage.ChangePasswordSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Hasło zostało pomyślnie zmienione.";
                model.LinkUrl = "/User/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.AddPhoneNumberSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Numer wymaga potwierdzenie dlatego został wysłany sms z kodem potwierdzającym.";
                model.LinkUrl = "/User/ConfirmPhoneNumber";
                model.LinkName = "Potwierdź";
            }
            if (message == IdMessage.AddPhoneNumberError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało się dodać numeru telefonu.";
                model.LinkUrl = "/User/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.RemovePhoneNumberSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Numer telefonu został usunięty pomyślnie.";
                model.LinkUrl = "/User/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.RemovePhoneNumberError)
            {
                model.Topic = "Błąd";
                model.Message = "Nie udało się usunąć numeru telefonu.";
                model.LinkUrl = "/User/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.ConfirmedPhoneNumberSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Numer telefonu został potwierdzony pomyślnie.";
                model.LinkUrl = "/User/Index";
                model.LinkName = "OK";
            }
            else
            if (message == IdMessage.ConfirmedPhoneNumberError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało się potwierdzić numeru telefonu.";
                model.LinkUrl = "/User/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.AdminSendConfirmationEmailSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Na adres klienta został wysłany link do potwierdzenia adresu Email.";
                model.LinkUrl = "/ManagementAdmin/Index";
                model.LinkName = "OK";
            }
            else
            if (message == IdMessage.AdminSendConfirmationEmailError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało się wysłać potwierdzenia na adres Email.";
                model.LinkUrl = "/ManagementAdmin/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.AdminDeleteAccountSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Użytkownik został prawidłowo usunięty.";
                model.LinkUrl = "/ManagementAdmin/Index";
                model.LinkName = "OK";
            }
            else
            if (message == IdMessage.AdminDeleteAccountError)
            {
                model.Topic = "Błąd";
                model.Message = "Niestety nie udało się usunąć użytkownika w logach zostały zawarte szczegóły.";
                model.LinkUrl = "/ManagementAdmin/Index";
                model.LinkName = "Wróć";
            }
            else
            if (message == IdMessage.UserDeleteAccountSucces)
            {
                model.Topic = "Powodzenie";
                model.Message = "Konto zostało pomyślnie usunięte, dziękujemy za uzytkowanie naszego oprogramowania.";
                model.LinkUrl = "/Account/LogIn";
                model.LinkName = "OK";
            }
            else
                return RedirectToAction("Index", "Home");

            return View(model);

        }
    }
}