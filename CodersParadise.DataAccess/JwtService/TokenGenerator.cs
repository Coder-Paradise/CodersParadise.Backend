using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace CodersParadise.DataAccess.JwtService
{
    public static class TokenGenerator
    {
        public static JwtToken GenerateToken(
           string key,
           string securityAlorithm,
           string issuer,
           string audience,
           int expirationMinutes,
           IEnumerable<Claim> claims = null)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, securityAlorithm);

            JwtSecurityToken tokenDefinition = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expirationMinutes),
                credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDefinition);

            return new JwtToken()
            {
                TokenDefinition = tokenDefinition,
                Token = token
            };
        }
    }
}
