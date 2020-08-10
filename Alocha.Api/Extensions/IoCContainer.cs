using Alocha.Api.Services;
using Alocha.Api.Services.Interfaces;
using Alocha.Domain.Interfaces;
using Alocha.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Alocha.Api.Extensions
{
    public static class IoCContainer
    {
        public static IServiceCollection RepositoryInjector(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
        public static IServiceCollection ServiceInjetor(this IServiceCollection services)
        {
            services.AddScoped<IJwTokenService, JwTokenService>();
            services.AddScoped<ISowService, SowService>();

            return services;
        }
    }
}
