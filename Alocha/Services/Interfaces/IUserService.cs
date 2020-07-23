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
        Task<UserManageVM> GetUserPhoneNumberEmailByIdAsync(string userId);
        Task<IdentityResult> ChangePasswordAsync(UserManageVM model, string userId);
        Task<IdentityResult> AddPhoneNumberAsync(UserManageVM model, string userId);
        Task<IdentityResult> RemovePhoneNumberAsync(UserManageVM model, string userId);
        Task<string> GenerateConfirmedPhoneNumberCodeAsync(string phoneNumber, string userId);
        Task<IdentityResult> ConfirmPhoneNumberAsync(string code, string userId);
    }
}
