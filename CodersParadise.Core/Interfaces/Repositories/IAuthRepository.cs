using CodersParadise.Core.DTO;

namespace CodersParadise.Core.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<Models.User?> GetUserByEmail(string email);

        Task<Models.User?> GetUserByToken(string token);

        Task<Core.Models.User?> GetUserById(int userId);

        Task<Models.User?> GetUserByResetToken(string token);

        Task UpdateUserVerifiedDate(int userId, DateTime dateTime);

        Task UpdateUserResetToken(int userId, string resetToken, DateTime tokenExpiry);

        Task UpdateUserPassword(Models.User user);

        Task StoreRefreshToken(Models.JwtRefreshToken refreshToken, int userId);

        Task<Models.JwtRefreshToken?> GetRefreshToken(Guid jwtId);

        Task DeleteRefreshToken(Guid jwtId);
    }
}
