using Identity.Domain.Models.Token;
using Identity.Domain.Results;
using Identity.Domain.Results.Token;

namespace Identity.Core.Helpers.Abstract
{
    public interface ITokenHelper
    {
        BaseResult<AccessTokenResult> CreateAccessToken(CreateAccessTokenModel createAccessTokenModel);
        void RevokeRefreshToken(RevokeRefreshToken revokeRefreshToken);
    }
}
