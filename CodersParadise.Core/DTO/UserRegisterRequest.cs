namespace CodersParadise.Core.DTO
{
    public class UserRegisterRequest
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string? VerificationToken { get; set; }
    }
}
