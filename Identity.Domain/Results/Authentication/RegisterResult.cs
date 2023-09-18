using Identity.Data.Dtos.User;

namespace Identity.Domain.Results.Authentication
{
    public sealed class RegisterResult
    {
        public UserDto UserDto { get; set; }
    }
}
