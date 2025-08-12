using CodersParadise.Core.DTO;
using CodersParadise.Core.Models;

namespace CodersParadise.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<Models.User?> GetUserByUsername(string username);

        Task<Models.User?> GetUserByToken(string token);

        Task<Models.User?> GetUserById(int userId);

        Task<Models.User?> GetUserByResetToken(string resetToken);

        Task UpdateUserVerifiedDate(int userId, DateTime verifiedDate);

        Task UpdateUserResetToken(int userId, string resetToken, DateTime tokenExpiry);

        Task UpdateUserPassword(Models.User user);

        Task StoreRefreshToken(JwtRefreshToken refreshToken, int userId);

        Task<JwtRefreshToken?> GetRefreshToken(Guid jwtId);

        Task DeleteRefreshToken(Guid jwtId);
    }
}
