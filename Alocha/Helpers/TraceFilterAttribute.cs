using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Alocha.WebUi.Helpers.Constans;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Alocha.WebUi.Helpers
{
    public sealed class TraceFilterAttribute : ActionFilterAttribute
    {
        public async override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context != null && context.HttpContext != null)
            {
                try
                {
                    var ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
                    var descriptor = context.ActionDescriptor as ControllerActionDescriptor;

                    var log = new TraceFilterLog()
                    {
                        Date = DateTime.Now.ToString(),
                        User = context.HttpContext.User.Identity.Name,
                        ControllerName = descriptor.ControllerName,
                        ActionName = descriptor.ActionName,
                        ActionParameters = context.ActionArguments,
                        IpHostInfo = ipHostEntry.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork).ToString()
                    };

                    await SaveLogMessageToFile(JsonConvert.SerializeObject(log));
                }
                catch (Exception)
                {
                }
            }
            base.OnActionExecuting(context);
        }

        private async Task SaveLogMessageToFile(string message)
        {
            System.IO.Directory.CreateDirectory(PathConstans.TRACE_FILTER_LOG_PATH);
            using (System.IO.StreamWriter file = System.IO.File.AppendText(string.Format("{0}\\{1}.txt", PathConstans.TRACE_FILTER_LOG_PATH, DateTime.Now.Date.ToShortDateString())))
            {
                try
                {
                    await file.WriteLineAsync(message);
                }
                finally //Clean resource after write text
                {
                    file.Close();
                }
            }
        }
    }
}