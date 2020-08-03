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
        Task<bool> CreateSowAsync(SowCreateVM model, string userId);
        Task<SowEditVM> GetSowForEditAsync(int sowId, string userId);
        Task<bool> EditSowAsync(SowEditVM model);
        string CalculateDate(DateTime date, string status);
        Task<SowDetailVM> DetailsSowAsync(int sowId, string userId);
        Task<bool> RemoveSowAsync(int sowId);
        Task<IEnumerable<SowVM>> GetPregnancySows(string userId);
        Task<bool> SowWasVacinated(int sowId, string userId);
    }
}
