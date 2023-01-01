using CodersParadise.Core.DTO;

namespace CodersParadise.Core.Interfaces.Logic
{
    public interface IAuthLogic
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<bool> Login(UserLoginRequest request);

        Task Verify(string token);

        Task ForgotPassword(string email);

        Task ResetPassword(ResetPasswordRequestDTO request);
    }
}
