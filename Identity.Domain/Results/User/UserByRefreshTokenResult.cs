using Identity.Data.Dtos.User;
using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.Domain.Results.User
{
    public sealed class UserByRefreshTokenResult
    {
        public UserDto UserDto { get; set; }
        public IEnumerable<Claim> Claims { get; set; }
    }
}
