using Identity.Core.Helpers.Abstract;
using Identity.Domain.Models.Authentication;
using Identity.Domain.Results;
using Identity.Domain.Results.Authentication;
using Identity.Domain.Results.Token;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Concrete
{
    public sealed class AuthenticationHelper : IAuthenticationHelper
    {
        public Task<BaseResult<AccessTokenResult>> CreateAccessTokenByRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResult<LoginResult>> LoginAsync(LoginModel loginModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResult<RegisterResult>> RegisterAsync(RegisterModel registerModel)
        {
            throw new System.NotImplementedException();
        }

        public Task<BaseResult<AccessTokenResult>> RevokeRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
        {
            throw new System.NotImplementedException();
        }
    }
}
