using Alocha.Domain.Entities;
using Alocha.WebUi.Models.AccountVM;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services.Interfaces
{
    public interface IAccountService
    {
        Task<IdentityResult> RegisterAsync(RegisterVM model);
        Task<SignInResult> LogInAsync(LogInVM model);
        Task<bool> LogOutAsync();
        Task<User> GetUserByEmailAsync(string email);
        Task<string> GenerateConfirmTokenAsync(User user);
    }
}
