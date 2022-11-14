using CodersParadise.Core.Interfaces.Logic;
using CodersParadise.Core.Logic;

namespace CodersParadise.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApiDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthLogic, AuthLogic>();
            return services;
        }
    }
}
