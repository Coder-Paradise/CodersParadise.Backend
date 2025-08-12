using System.ComponentModel.DataAnnotations;

namespace CodersParadise.Core.DTO
{
    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredAccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
