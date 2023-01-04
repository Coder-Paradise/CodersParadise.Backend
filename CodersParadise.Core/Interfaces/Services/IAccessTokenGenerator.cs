using CodersParadise.Core.Models;

namespace CodersParadise.Core.Interfaces.Services
{
    public interface IAccessTokenGenerator
    {
        JwtAccessToken GenerateToken(long userId, string userName);
    }
}
