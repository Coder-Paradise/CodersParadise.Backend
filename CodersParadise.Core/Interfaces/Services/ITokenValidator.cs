namespace CodersParadise.Core.Interfaces.Services
{
    public interface ITokenValidator
    {
        bool ValidateAccessToken(string accessToken);
        Guid GetAndValidateRefreshToken(string refreshToken);
    }
}
