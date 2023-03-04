using CodersParadise.Core.DTO;
using CodersParadise.Core.Models;

namespace CodersParadise.Core.Interfaces.Logic
{
    public interface IAuthLogic
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<UserLoginResponse> Login(UserLoginRequest request);

        Task Verify(string token);

        Task ForgotPassword(string email);

        Task ResetPassword(ResetPasswordRequestDTO request);
    }
}
