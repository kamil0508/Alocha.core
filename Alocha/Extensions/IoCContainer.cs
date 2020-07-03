using Alocha.Domain.Interfaces;
using Alocha.Domain.Repositories;
using Alocha.WebUi.Services;
using Alocha.WebUi.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alocha.WebUi.Extensions
{
    public static class IoCContainer
    {
        public static IServiceCollection RepositoryInjector(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection ServiceInjector(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ISmallPigService, SmallPigService>();
            services.AddScoped<ISowService, SowService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
