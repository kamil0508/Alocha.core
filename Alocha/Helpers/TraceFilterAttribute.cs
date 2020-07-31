using System;
using Alocha.WebUi.Helpers.Constans;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace Alocha.WebUi.Helpers
{
    public sealed class TraceFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext != null && filterContext.HttpContext != null)
            {
                try
                {
                    var descriptor = filterContext.ActionDescriptor as ControllerActionDescriptor;

                    var log = new TraceFilterLog()
                    {
                        Date = DateTime.Now.ToString(),
                        User = filterContext.HttpContext.User.Identity.Name,
                        ControllerName = descriptor.ControllerName,
                        ActionName = descriptor.ActionName,
                        ActionParameters = filterContext.ActionArguments
                    };

                    LogMessageToFile(JsonConvert.SerializeObject(log));
                }
                catch (Exception)
                {
                }
            }

            base.OnActionExecuting(filterContext);
        }

        private void LogMessageToFile(string msg)
        {
            System.IO.Directory.CreateDirectory(PathConstans.TRACE_FILTER_LOG_PATH);
            System.IO.StreamWriter sw = System.IO.File.AppendText(PathConstans.TRACE_FILTER_LOG_PATH + "\\" + DateTime.Now.Date.ToShortDateString() + ".txt");
            try
            {
                sw.WriteLine(msg);
            }
            finally
            {
                sw.Close();
            }
        }
    }
}
