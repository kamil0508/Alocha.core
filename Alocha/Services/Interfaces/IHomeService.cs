using Alocha.WebUi.Models.HomeVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IHomeService
    {
        Task<IEnumerable<UpcomingTaskVM>> GetUpcomingTaskAsync(string userId);
        Task<string> GetUpcomingTaskCountAsync(string userId);
    }
}
