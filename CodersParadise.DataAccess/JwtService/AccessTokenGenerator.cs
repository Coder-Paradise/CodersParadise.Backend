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

            //TODO: Roles Hardcoded for now and added to JWT Claims.  Will build roles architecture as future intiative.
            var roles = new string[] { "2001", "1984", "5150" };
            var roleClaims = roles.Select(roleId => new Claim(ClaimTypes.Role, roleId));
            claims.AddRange(roleClaims);

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
