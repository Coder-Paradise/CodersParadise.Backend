using Microsoft.OpenApi.Models;

namespace CodersParadise.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "CSI.MobileApi",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer {JwtAccessToken}'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                     new OpenApiSecurityScheme
                     {
                     Reference = new OpenApiReference
                         {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                         }
                     },
                     new string[] { }
                }
                });
            });
            return services;
        }
    }
}
