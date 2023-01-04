namespace CodersParadise.Core.Models
{
    public class JwtAccessToken
    {
        public string AccessToken { get; set; }

        public DateTime TokenExpiry { get; set; }
    }
}
