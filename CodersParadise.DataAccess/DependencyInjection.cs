using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.DataAccess.Databases.CodersParadise;
using CodersParadise.DataAccess.Respositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodersParadise.DataAccess
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessServices(this IServiceCollection services, IConfiguration config)
        {
            //Add Database Dependencies
            services.AddDbContext<CodersParadiseDbContext>(opt => opt.UseSqlServer(config["ConnectionStrings:CodersParadiseConnection"]));

            //Add DI Registrations
            services.AddScoped<IAuthRepository, AuthRepository>();

            return services;
        }
    }
}
