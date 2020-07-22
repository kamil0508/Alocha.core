using Alocha.Domain.Entities;
using Alocha.WebUi.Models.UserVM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserManageVM> GetUserPhoneNumberById(string userId);
        Task<IdentityResult> ChangePasswordAsync(UserManageVM model, string userId);
    }
}
