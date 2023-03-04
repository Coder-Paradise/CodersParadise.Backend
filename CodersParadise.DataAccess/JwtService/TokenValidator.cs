using CodersParadise.Core.Interfaces.Services;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodersParadise.DataAccess.JwtService
{
    public class TokenValidator : ITokenValidator
    {
        private readonly JwtConfiguration _jwtConfig;

        public TokenValidator(JwtConfiguration jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GetAndValidateRefreshToken(string refreshToken)
        {
            var validationParameters = SetTokenValidationParameters(_jwtConfig.RefreshTokenSecretKey, true, SecurityAlgorithms.HmacSha256);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(refreshToken, validationParameters, out SecurityToken validatedToken);

                if (validatedToken.Id == null)
                    throw new();

                return validatedToken.Id;
            }
            catch (Exception)
            {
                throw new Exception("Invalid refresh token");
            }
        }

        public SecurityToken ValidateExpiredAccessToken(string accessToken)
        {
            var validationParameters = SetTokenValidationParameters(_jwtConfig.TokenSecretKey, false, SecurityAlgorithms.HmacSha512);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                tokenHandler.ValidateToken(accessToken, validationParameters, out SecurityToken validatedToken);
                return validatedToken;
            }
            catch (Exception)
            {
                throw new Exception("Invalid Expired Access Token");
            }
        }

        public bool ValidateAccessToken(string accessToken)
        {
            var securityToken = ValidateExpiredAccessToken(accessToken);
            return CheckTokenExpired(securityToken.ValidTo);
        }


        public bool CheckTokenExpired(DateTime tokenExpiry)
        {
            return DateTime.UtcNow >= tokenExpiry;
        }

        private TokenValidationParameters SetTokenValidationParameters(
            string secretKey,
            bool validateExpiration,
            string SecurityAlgorithm)
        {
            return new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = validateExpiration,
                ClockSkew = TimeSpan.Zero,
                ValidAlgorithms = new List<string>() { SecurityAlgorithm }
            };
        }
    }
}
