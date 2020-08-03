using Alocha.Domain.Entities;
using Alocha.Domain.Interfaces;
using Alocha.WebUi.Models.ManagementAdminVM;
using Alocha.WebUi.Services.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Services
{
    public class ManagementAdminService : IManagementAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public ManagementAdminService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ManagementVM>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.User.GetAllAsync();
            var model = _mapper.Map<IEnumerable<User>, IEnumerable<ManagementVM>>(users);
            return model;
        }

        public async Task<IdentityUser> GetUserByEmailAsync(string userEmail)
        {
            return await _userManager.FindByEmailAsync(userEmail);
        }

        public async Task<string> GenerateConfirmTokenAsync(IdentityUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);    
        }

        public async Task<IdentityResult> DeleteUserAsync(User user)
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
    }
}
