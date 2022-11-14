using CodersParadise.Core.Interfaces.Logic;
using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Logic;
using CodersParadise.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CodersParadise.Core
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            //Add DI Registrations
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthLogic, AuthLogic>();

            return services;
        }       
    }
}
