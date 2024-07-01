using Banking.Core.Services;
using Banking.Services.Maps;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Services
{
    /// <summary>
    ///     Provides registration of services via extension methods.
    /// </summary>
    public static class ServiceConfig
    {
        /// <summary>
        ///     Registers common services
        /// </summary>
        /// <param name="services">Service collection (IoC container) where to register the services.</param>
        /// <returns>Service collection  (IoC container) where the services were registered.</returns>
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            return services
                .AddSingleton(MappingInitializer.Intialize())
                .AddTransient<IUserService, UserService>()
                .AddTransient<IAccountService, AccountService>()
                .AddTransient<ITransactionService, TransactionService>()
                .AddTransient<IUserIdentityService, UserIdentityService>();
        }
    }
}
