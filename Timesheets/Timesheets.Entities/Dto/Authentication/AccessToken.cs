using System;

namespace Timesheets.Entities.Dto.Authentication
{
    public sealed class AccessToken
    {
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
    }
}
