﻿namespace CodersParadise.Core.DTO
{
    public class ResetPasswordRequestDTO
    {
        public string Token { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
