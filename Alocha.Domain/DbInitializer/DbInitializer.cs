using Alocha.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alocha.Domain.DbInitializer
{
    public class DbInitializer : IDBInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initializer()
        {
            if (!_roleManager.RoleExistsAsync("Administrator").GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole() { Name = "Administrator" }).GetAwaiter().GetResult();

            if (!_roleManager.RoleExistsAsync("Customer").GetAwaiter().GetResult())
                _roleManager.CreateAsync(new IdentityRole() { Name = "Customer" }).GetAwaiter().GetResult();

            if (_userManager.FindByEmailAsync("kaamilgrabowski1@wp.pl").GetAwaiter().GetResult() == null)
            {
                var user = new User() { Email = "kaamilgrabowski1@wp.pl", UserName = "kaamilgrabowski1@wp.pl", EmailConfirmed = true };
                var result = _userManager.CreateAsync(user, "Start123!").GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(user, "Administrator").GetAwaiter().GetResult();
            }
        }
    }
}
