using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.Core.Interfaces.Services;

namespace CodersParadise.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;

        public AuthService(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<bool> Register(UserRegisterRequest request)
        {
            var response = await _authRepository.Register(request);
            return response;
        }

        public async Task<Models.User?> GetUserByEmail(string email)
        {
            var response = await _authRepository.GetUserByEmail(email);
            return response;
        }

        public async Task<Models.User?> GetUserByToken(string token)
        {
            var response = await _authRepository.GetUserByToken(token);
            return response;
        }

        public async Task<bool> UpdateUserVerifiedDate(DateTime verifiedDate)
        {
            var response = await _authRepository.UpdateUserVerifiedDate(verifiedDate);
            return response;
        }
    }
}
