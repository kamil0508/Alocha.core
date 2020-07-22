using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
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

        public async Task<IdentityResult> ChangePasswordAsync(UserManageVM model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);         
        }

        public async Task<UserManageVM> GetUserPhoneNumberById(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var model = _mapper.Map<IdentityUser, UserManageVM>(user); 
            return model;
        }
    }
}
