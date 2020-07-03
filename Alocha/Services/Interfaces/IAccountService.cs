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
        Task<SignInResult> LogInAsync(LogInVM model);
        Task<bool> LogOutAsync();
    }
}
