using Identity.Domain.Results.Token;

namespace Identity.Domain.Results.Authentication
{
    public sealed class LoginResult
    {
        public AccessTokenResult AccessToken { get; set; }
    }
}
