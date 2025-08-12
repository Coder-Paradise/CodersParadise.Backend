namespace CodersParadise.Core.DTO
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime AccessTokenExpiry { get; set; }
    }
}
