using CodersParadise.Core.DTO;

namespace CodersParadise.Core.Interfaces.Logic
{
    public interface IAuthLogic
    {
        Task<bool> Register(UserRegisterRequest request);

        Task<bool> Login(UserLoginRequest request);

        Task<bool> Verify(string token);
    }
}
