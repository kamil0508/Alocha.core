using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Alocha.WebUi.Helpers
{
    public class SmsSender
    {
        public static async Task<MessageResource> SendSmsAsync(string number, string message)
        {
            var accountSid = "";
            var authToken = "";

            TwilioClient.Init(accountSid, authToken);

            return await MessageResource.CreateAsync(
              to: new PhoneNumber(number),
              from: new PhoneNumber(""),
              body: message);
        }
    }
}
