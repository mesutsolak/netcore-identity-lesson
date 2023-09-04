using Identity.Core.Helpers.Abstract;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Identity.Core.Helpers.Concrete
{
    public sealed class SecurityHelper : ISecurityHelper
    {
        public SecurityKey GetSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
