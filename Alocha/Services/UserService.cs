using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Helpers.Constans;
using Alocha.WebUi.Models.UserVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddPhoneNumberAsync(UserManageVM model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
        }

        public async Task<IdentityResult> ChangePasswordAsync(UserManageVM model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);         
        }

        public async Task<UserManageVM> GetUserPhoneNumberEmailByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = _mapper.Map<IdentityUser, UserManageVM>(user); 
            return model;
        }

        public async Task<IdentityResult> RemovePhoneNumberAsync(UserManageVM model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.SetPhoneNumberAsync(user, null);
        }

        public async Task<string> GenerateConfirmedPhoneNumberCodeAsync(string phoneNumber, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.GenerateChangePhoneNumberTokenAsync(user, phoneNumber);
        }

        public async Task<IdentityResult> ConfirmPhoneNumberAsync(string code, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, code);
        }

        public async Task<IdentityResult> DeleteUserAsync(string email, string userId)
        {
            var user = (User)await _userManager.FindByEmailAsync(email);
            if(user.Id == userId)
            {
                if (user.Sows.Count() > 0)
                {
                    user.Sows.ToList().ForEach(s => _unitOfWork.Smallpig.RemoveRange(s.SmallPigs));
                    _unitOfWork.Sow.RemoveRange(user.Sows);
                    await _unitOfWork.SaveChangesAsync();
                }
                var role = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRoleAsync(user, role.First());
                return await _userManager.DeleteAsync(user);
            }
            return IdentityResult.Failed(new IdentityError() { Description = IdentityResultErrorsConstans.DELETE_USER_ERROR_FROM_USER_SERVICE });
        }
    }
}
