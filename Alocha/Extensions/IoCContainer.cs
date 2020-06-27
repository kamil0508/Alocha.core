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
            //TODO inject repository

            return services;
        }

        public static IServiceCollection ServiceInjector(this IServiceCollection services)
        {
            //TODO inject services

            return services;
        }
    }
}
