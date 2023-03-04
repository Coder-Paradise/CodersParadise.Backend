using CodersParadise.Core.Models;

namespace CodersParadise.Core.Interfaces.Services
{
    public interface IJwtService
    {
        JwtAccessToken GenerateAccessToken(long userId, string userName);

        JwtRefreshToken GenerateRefreshToken();

        bool ValidateAccessToken(string accessToken);

        string GetAndValidateRefreshToken(string refreshToken);
    }
}
