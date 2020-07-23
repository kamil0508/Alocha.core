using Alocha.WebUi.Helpers;
using Alocha.WebUi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio.Rest.Api.V2010.Account;

namespace Alocha.WebUi.Services
{
    public class SmsService : ISmsService
    {
        public async Task<MessageResource> SendConfirmPhoneNumberSmsAsync(string phoneNumber, string code)
        {
            return await SmsSender.SendSmsAsync(string.Format("48{0}", phoneNumber), string.Format("Twój kod potwierdzający to:{0}", code));
        }
    }
}
