using Alocha.Domain.Entities;
using Alocha.WebUi.Models.ManagementAdminVM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IManagementAdminService
    {
        Task<IEnumerable<ManagementVM>> GetAllUsersAsync();
        Task<IdentityUser> GetUserByEmailAsync(string userEmail);
        Task<string> GenerateConfirmTokenAsync(IdentityUser user);
    }
}
