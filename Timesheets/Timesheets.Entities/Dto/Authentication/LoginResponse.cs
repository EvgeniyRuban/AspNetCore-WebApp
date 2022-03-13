namespace Timesheets.Entities.Dto.Authentication
{
    public sealed class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
