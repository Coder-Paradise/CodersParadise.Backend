using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CodersParadise.DataAccess.JwtService
{
    internal class RefreshTokenGenerator : IRefreshTokenGenerator
    {
        private readonly JwtConfiguration _jwtConfig;

        public RefreshTokenGenerator(JwtConfiguration jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public JwtRefreshToken GenerateToken()
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var refreshToken = TokenGenerator.GenerateToken(
                _jwtConfig.RefreshTokenSecretKey,
                SecurityAlgorithms.HmacSha256,
                _jwtConfig.Issuer,
                _jwtConfig.Audience,
                _jwtConfig.RefreshTokenExpirationMinutes,
                claims);

            return new JwtRefreshToken()
            {
                JwtId = new Guid(refreshToken.TokenDefinition.Id),
                RefreshToken = refreshToken.Token,
                TokenExpiry = refreshToken.TokenDefinition.ValidTo
            };
        }
    }
}
