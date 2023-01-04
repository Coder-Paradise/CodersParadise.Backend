using CodersParadise.Core.DTO;
using CodersParadise.Core.Models;

namespace CodersParadise.Core.Interfaces.Logic
{
    public interface IAuthLogic
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<JwtAccessToken> Login(UserLoginRequest request);

        Task Verify(string token);

        Task ForgotPassword(string email);

        Task ResetPassword(ResetPasswordRequestDTO request);
    }
}
