using CodersParadise.Core.DTO;

namespace CodersParadise.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<Models.User?> GetUserByEmail(string email);

        Task<Models.User?> GetUserByToken(string token);

        Task UpdateUserVerifiedDate(int userId, DateTime verifiedDate);

        Task UpdateUserResetToken(int userId, string resetToken, DateTime tokenExpiry);
    }
}
