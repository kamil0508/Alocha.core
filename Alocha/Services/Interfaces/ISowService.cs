using Alocha.WebUi.Models.SowVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface ISowService
    {
        Task<IEnumerable<SowVM>> GetAllSowsAsync(string userId);
        Task<bool> CreateSowAsync(SowIndexVM model, string userId);
    }
}
