using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Helpers;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class EmailService : IEmailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> SendConfirmEmail(string callBackUrl, string email)
        {
            return await EmailSender.MailSender(email, EmailConstans.SUBJECT_CONFIRM_EMAIL, EmailConstans.BODY_CONFIRM_EMAIL + "<a href =\"" + callBackUrl + "\">POTWIERDZENIE KONTA</a>");
        }
    }
}
