using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Logic;
using CodersParadise.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodersParadise.Core.Logic
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IAuthService _authService;

        public AuthLogic(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<bool> Register(UserRegisterRequest request)
        {
            var existingUser = await _authService.GetUserByEmail(request.Email);

            if (existingUser != null)
                throw new Exception("User already exists");

            CreatePasswordHash(request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt);

            request.PasswordSalt = passwordSalt;
            request.PasswordHash =  passwordHash;
            request.VerificationToken = CreateRandomToken();

            var response = await _authService.Register(request);    
            return response;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
