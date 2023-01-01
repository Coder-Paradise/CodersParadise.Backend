using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Logic;
using CodersParadise.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

        public async Task<bool> Login(UserLoginRequest request)
        {
            var user = await _authService.GetUserByEmail(request.Email);

            if (user == null)
                throw new Exception("User not found");

            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Password is incorrect.");
            }

            if(user.VerifiedDate == null)
                throw new Exception("Not Verified");

            return true;
        }

        public async Task Verify(string token)
        {
            var user = await _authService.GetUserByToken(token);

            if (user == null)
                throw new Exception("User not found");

            await _authService.UpdateUserVerifiedDate(user.Id, DateTime.UtcNow);
        }

        public async Task ForgotPassword(string email)
        {
            var user = await _authService.GetUserByEmail(email);

            if (user == null)
                throw new Exception("User not found");

            await _authService.UpdateUserResetToken(user.Id, CreateRandomToken(), DateTime.UtcNow.AddDays(1));
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
               var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
        }
    }
}
