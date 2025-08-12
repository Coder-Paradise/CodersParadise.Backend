using CodersParadise.Core.Interfaces.Services;
using CodersParadise.Core.Models;

namespace CodersParadise.Core.Services
{
    public class JwtService : IJwtService
    {
        private readonly IAccessTokenGenerator _accessTokenGenerator;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly ITokenValidator _tokenValidator;

        public JwtService(
             IAccessTokenGenerator accessTokenGenerator
            ,IRefreshTokenGenerator refreshTokenGenerator
            ,ITokenValidator tokenValidator)
        {
            _accessTokenGenerator = accessTokenGenerator;
            _refreshTokenGenerator = refreshTokenGenerator;
            _tokenValidator = tokenValidator;
        }
        public JwtAccessToken GenerateAccessToken(long userId, string username)
        {
            var response = _accessTokenGenerator.GenerateToken(userId, username);
            return response;
        }

        public JwtRefreshToken GenerateRefreshToken()
        {
            var response = _refreshTokenGenerator.GenerateToken();
            return response;
        }

        public bool ValidateAccessToken(string accessToken)
        {
            var response = _tokenValidator.ValidateAccessToken(accessToken);
            return response;
        }

        public Guid GetAndValidateRefreshToken(string refreshToken)
        {
            var response = _tokenValidator.GetAndValidateRefreshToken(refreshToken);
            return response;
        }
    }
}
