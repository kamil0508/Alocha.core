using Alocha.WebUi.Models.ManagementAdminVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IManagementAdminService
    {
        Task<IEnumerable<ManagementVM>> GetAllUsersAsync();
    }
}
