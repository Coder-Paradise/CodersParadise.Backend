namespace CodersParadise.DataAccess.JwtService
{
    public class JwtConfiguration
    {
        public string TokenSecretKey { get; set; }

        public string RefreshTokenSecretKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public int TokenExpirationMinutes { get; set; }

        public int RefreshTokenExpirationMinutes { get; set; }

        public int MobileVerificationTokenExpirationMinutes { get; set; }
    }
}
