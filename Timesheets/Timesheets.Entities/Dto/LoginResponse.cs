namespace Timesheets.Entities.Dto
{
    /// <summary>
    /// Provides response for the user authetication request.
    /// </summary>
    public sealed class LoginResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
