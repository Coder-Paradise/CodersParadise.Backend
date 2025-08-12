namespace CodersParadise.Core.Models
{
    public class JwtRefreshToken
    {
        public Guid JwtId { get; set; }

        public string RefreshToken { get; set; }

        public DateTime TokenExpiry { get; set; }

        public int UserId { get; set; }
    }
}
