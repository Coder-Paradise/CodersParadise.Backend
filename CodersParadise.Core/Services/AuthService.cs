using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Models;

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

        public async Task<Models.User?> GetUserByUsername(string username)
        {
            var response = await _authRepository.GetUserByUsername(username);
            return response;
        }

        public async Task<Models.User?> GetUserByToken(string token)
        {
            var response = await _authRepository.GetUserByToken(token);
            return response;
        }

        public async Task<Models.User?> GetUserById(int userId)
        {
            var response = await _authRepository.GetUserById(userId);
            return response;
        }

        public async Task<Models.User?> GetUserByResetToken(string resetToken)
        {
            var response = await _authRepository.GetUserByResetToken(resetToken);
            return response;
        }

        public async Task UpdateUserVerifiedDate(int userId, DateTime verifiedDate)
        {
            await _authRepository.UpdateUserVerifiedDate(userId, verifiedDate);
        }

        public async Task UpdateUserResetToken(int userId, string resetToken, DateTime tokenExpiry)
        {
            await _authRepository.UpdateUserResetToken(userId, resetToken, tokenExpiry);
        }

        public async Task UpdateUserPassword(Models.User user)
        {
            await _authRepository.UpdateUserPassword(user);
        }

        public async Task StoreRefreshToken(JwtRefreshToken refreshToken, int userId)
        {     
            await _authRepository.StoreRefreshToken(refreshToken, userId);
        }

        public async Task DeleteRefreshToken(Guid jwtId)
        {
            await _authRepository.DeleteRefreshToken(jwtId);
        }

        public async Task<JwtRefreshToken?> GetRefreshToken(Guid jwtId)
        {
            var response = await _authRepository.GetRefreshToken(jwtId);
            return response;
        }
    }
}
