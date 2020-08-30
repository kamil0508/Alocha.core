using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Helpers
{
    public class TraceFilterLog
    {
        public string Date { get; set; }
        public string User { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public IDictionary<string, object> ActionParameters { get; set; }
        public string IpHostInfo { get; set; }
    }
}
