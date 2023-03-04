namespace CodersParadise.Core.Interfaces.Services
{
    public interface ITokenValidator
    {
        bool ValidateAccessToken(string accessToken);
        string GetAndValidateRefreshToken(string refreshToken);
    }
}
