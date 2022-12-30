using CodersParadise.Core.DTO;

namespace CodersParadise.Core.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<Models.User?> GetUserByEmail(string email);

        Task<Models.User?> GetUserByToken(string token);

        Task<bool> UpdateUserVerifiedDate(DateTime dateTime);
    }
}
