using Alocha.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendConfirmEmail(string callBackUrl, string email);
    }
}
