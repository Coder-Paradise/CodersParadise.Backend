namespace CodersParadise.DataAccess.Databases.CodersParadise.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public byte[] PaswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public string? VerificationToken { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public string? PasswordResetToken { get; set; }
        public DateTime? TokenExpiry { get; set; }
    }
}
