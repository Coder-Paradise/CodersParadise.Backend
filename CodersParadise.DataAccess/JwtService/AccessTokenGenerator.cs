using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CodersParadise.DataAccess.JwtService
{
    public class AccessTokenGenerator : IAccessTokenGenerator
    {
        private readonly JwtConfiguration _jwtConfig;

        public AccessTokenGenerator(JwtConfiguration jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public JwtAccessToken GenerateToken(long userId, string userName)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Name, userName.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var accessToken = TokenGenerator.GenerateToken(
                _jwtConfig.TokenSecretKey,
                SecurityAlgorithms.HmacSha512,
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                _jwtConfig.TokenExpirationMinutes,
                claims);

            return new JwtAccessToken()
            {
                AccessToken = accessToken.Token,
                TokenExpiry = accessToken.TokenDefinition.ValidTo
            };
        }
    }
}
