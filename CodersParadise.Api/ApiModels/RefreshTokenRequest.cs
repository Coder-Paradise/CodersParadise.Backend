using System.ComponentModel.DataAnnotations;

namespace CodersParadise.Api.ApiModels
{
    public class RefreshTokenRequest
    {
        [Required]
        public string ExpiredAccessToken { get; set; }

        [Required]
        public string RefreshToken { get; set; }
    }
}
