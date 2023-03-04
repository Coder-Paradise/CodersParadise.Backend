namespace CodersParadise.Api.ApiModels
{
    public class LoginResponse
    {
        public string AccessToken { get; set; }

        public DateTime TokenExpiry { get; set; }

        public string RefreshToken { get; set; }

        public int[] Roles { get; set; }
    }
}
