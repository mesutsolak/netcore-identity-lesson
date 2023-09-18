using Identity.Domain.Models.Token;
using Identity.Domain.Results.Token;

namespace Identity.Core.Helpers.Abstract
{
    public interface ITokenHelper
    {
        AccessTokenResult CreateAccessToken(CreateAccessTokenModel createAccessTokenModel);
        void RevokeRefreshToken(RevokeRefreshToken revokeRefreshToken);
    }
}
