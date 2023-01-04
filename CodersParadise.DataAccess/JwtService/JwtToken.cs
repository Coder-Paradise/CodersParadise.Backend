using System.IdentityModel.Tokens.Jwt;

namespace CodersParadise.DataAccess.JwtService
{
    public class JwtToken
    {
        public JwtSecurityToken TokenDefinition { get; set; }

        public string Token { get; set; }
    }
}
