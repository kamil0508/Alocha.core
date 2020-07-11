using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Helpers.Constans;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _unitOfWork.User.FindOneAsync(u => u.Email == email);
        }

        public async Task<string> GenerateConfirmTokenAsync(User user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }

        public async Task<IdentityResult> ConfirmEmailAsync(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ConfirmEmailAsync(user, token);    
        }

        public async Task<string> GenerateResetPasswordTokenAsync(ForgotPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
                return await _userManager.GeneratePasswordResetTokenAsync(user);
            return null;
        }

        public async Task<IdentityResult> ResetPasswordAsync(ResetPasswordVM model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if(user != null)
                return await _userManager.ResetPasswordAsync(user, model.Token, model.Password);    
            return IdentityResult.Failed(new IdentityError() { Description = IdentityResultErrorsConstans.RESET_PASSWORD_USER_NOT_FOUND });
        }
    }
}
