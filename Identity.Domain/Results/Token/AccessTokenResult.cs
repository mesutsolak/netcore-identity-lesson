using System;

namespace Identity.Domain.Results.Token
{
    public sealed class AccessTokenResult
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string RefreshToken { get; set; }
    }
}
