using CodersParadise.Core.DTO;

namespace CodersParadise.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<Models.User?> GetUserByEmail(string email);
    }
}
