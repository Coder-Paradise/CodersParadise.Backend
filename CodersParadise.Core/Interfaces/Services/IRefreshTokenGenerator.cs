using CodersParadise.Core.Models;

namespace CodersParadise.Core.Interfaces.Services
{
    public interface IRefreshTokenGenerator
    {
        JwtRefreshToken GenerateToken();
    }
}
