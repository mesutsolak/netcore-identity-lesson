using Microsoft.IdentityModel.Tokens;

namespace Identity.Core.Helpers.Abstract
{
    public interface ISecurityHelper
    {
        SecurityKey GetSecurityKey(string securityKey);
    }
}
