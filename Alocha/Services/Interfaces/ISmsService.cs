using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface ISmsService
    {
        Task<MessageResource> SendConfirmPhoneNumberSmsAsync(string phoneNumber, string code);
    }
}
