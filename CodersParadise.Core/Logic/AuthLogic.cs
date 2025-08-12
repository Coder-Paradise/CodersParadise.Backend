using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Logic;
using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Models;
using System.Security.Cryptography;
using System.Text;

namespace CodersParadise.Core.Logic
{
    public class AuthLogic : IAuthLogic
    {
        private readonly IAuthService _authService;
        private readonly IJwtService _jwtService;

        public AuthLogic(IAuthService authService, IJwtService jwtService)
        {
            _authService = authService;
            _jwtService = jwtService;
        }

        public async Task<bool> Register(UserRegisterRequest request)
        {
            var existingUser = await _authService.GetUserByUsername(request.Username);

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

        public async Task<UserLoginResponse> Login(UserLoginRequest request)
        {
            var user = await _authService.GetUserByUsername(request.Username) ?? throw new UnauthorizedAccessException("User not found");
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new UnauthorizedAccessException("Password is incorrect.");
            }

            if(user.VerifiedDate == null)
                throw new Exception("Not Verified");

            var accessToken = _jwtService.GenerateAccessToken(user.Id, request.Username);
            var refreshToken = _jwtService.GenerateRefreshToken();
            await _authService.StoreRefreshToken(refreshToken, user.Id);

            return new UserLoginResponse()
            {
                AccessToken = accessToken.AccessToken,
                RefreshToken= refreshToken.RefreshToken,
                AccessTokenExpiry = accessToken.TokenExpiry
            };
        }

        public async Task Verify(string token)
        {
            var user = await _authService.GetUserByToken(token);

            if (user == null)
                throw new Exception("User not found");

            await _authService.UpdateUserVerifiedDate(user.Id, DateTime.UtcNow);
        }

        public async Task ForgotPassword(string username)
        {
            var user = await _authService.GetUserByUsername(username);

            if (user == null)
                throw new Exception("User not found");

            await _authService.UpdateUserResetToken(user.Id, CreateRandomToken(), DateTime.UtcNow.AddDays(1));
        }

        public async Task ResetPassword(ResetPasswordRequestDTO request)
        {
            var user = await _authService.GetUserByResetToken(request.Token);

            if (user == null || user.TokenExpiry < DateTime.UtcNow)
                throw new Exception("Invalid Token.");

            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.PasswordResetToken = null;
            user.TokenExpiry = null;

            await _authService.UpdateUserPassword(user);
        }

        public async Task<UserLoginResponse> RefreshToken(RefreshTokenRequest refreshRequest)
        {
            var validatedExpiredAccessToken = _jwtService.ValidateAccessToken(refreshRequest.ExpiredAccessToken);

            if (!validatedExpiredAccessToken)
            {
                throw new Exception("Jwt access token has not expired");
            }

            var jwtId = _jwtService.GetAndValidateRefreshToken(refreshRequest.RefreshToken);

            var storedRefreshTokenResponse = await _authService.GetRefreshToken(jwtId);

            if (storedRefreshTokenResponse == null)
            {
                throw new Exception("Refresh token not found from storage.");
            }

            var userResponse = await _authService.GetUserById(storedRefreshTokenResponse.UserId);

            if (userResponse == null)
            {
                throw new Exception("User not found.");
            }

            //Generate New Tokens
            var accessToken = _jwtService.GenerateAccessToken(userResponse.Id, userResponse.Username);
            var refreshToken = _jwtService.GenerateRefreshToken();
            await _authService.StoreRefreshToken(refreshToken, userResponse.Id);

            //Delete Old Refresh Token
            await _authService.DeleteRefreshToken(jwtId);

            return new UserLoginResponse
            {
                AccessToken = accessToken.AccessToken,
                RefreshToken = refreshToken.RefreshToken,
                AccessTokenExpiry = accessToken.TokenExpiry
            };
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
