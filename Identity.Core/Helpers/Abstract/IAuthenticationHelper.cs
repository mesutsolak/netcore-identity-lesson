using Identity.Domain.Models.Authentication;
using Identity.Domain.Results;
using Identity.Domain.Results.Authentication;
using Identity.Domain.Results.Token;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Abstract
{
    public interface IAuthenticationHelper
    {
        Task<BaseResult<RegisterResult>> RegisterAsync(RegisterModel registerModel);
        Task<BaseResult<LoginResult>> LoginAsync(LoginModel loginModel);
        Task<BaseResult<AccessTokenResult>> CreateAccessTokenByRefreshTokenAsync(RefreshTokenModel refreshTokenModel);
        Task<BaseResult<AccessTokenResult>> RevokeRefreshTokenAsync(RefreshTokenModel refreshTokenModel);
    }
}
