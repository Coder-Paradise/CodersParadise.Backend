using System.ComponentModel.DataAnnotations;

namespace CodersParadise.Api.ApiModels
{
    public class UserRegisterRequest
    {

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required, MinLength(6, ErrorMessage = "Please enter at least 6 characters.")]
        public string Password { get; set; } = string.Empty;

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
