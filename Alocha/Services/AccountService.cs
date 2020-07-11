using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.AccountVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task<SignInResult> LogInAsync(LogInVM model)
        {
            return await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);        
        }

        public async Task<bool> LogOutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }

        public Task<IdentityResult> RegisterAsync(RegisterVM model)
        {
            var user = new User() { UserName = model.Email, Email = model.Email };
            return _userManager.CreateAsync(user, model.Password);
        }
    }
}
