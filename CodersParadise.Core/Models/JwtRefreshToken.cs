namespace CodersParadise.Core.Models
{
    public class JwtRefreshToken
    {
        public string RefreshToken { get; set; }

        public DateTime TokenExpiry { get; set; }
    }
}
