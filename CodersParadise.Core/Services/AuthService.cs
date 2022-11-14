using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Repositories;
using CodersParadise.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
