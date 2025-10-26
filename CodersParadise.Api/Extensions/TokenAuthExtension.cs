using CodersParadise.DataAccess.JwtService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CodersParadise.Api.Extensions
{
    public static class TokenAuthExtension
    {
        public static IServiceCollection AddTokenAuthentication(this IServiceCollection services, JwtConfiguration jwtConfiguration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.TokenSecretKey)),
                    ValidIssuer = jwtConfiguration.Issuer,
                    ValidAudience = jwtConfiguration.Audience,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero
                };

                o.Events = new JwtBearerEvents
                {
                    OnMessageReceived = ctx =>
                    {
                        ctx.Request.Cookies.TryGetValue("accessToken", out var accessToken);
                        if (!string.IsNullOrEmpty(accessToken))
                            ctx.Token = accessToken;

                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
