using Identity.Domain.Models.Authorize;
using Identity.Domain.Results;
using Identity.Domain.Results.Authorize;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Abstract
{
    public interface IAuthorizeHelper
    {
        Task<BaseResult<RegisterResult>> RegisterAsync(RegisterModel registerModel);
        Task<BaseResult<LoginResult>> LoginAsync(LoginModel loginModel);
        Task<BaseResult<LogoutResult>> LogoutAsync();
    }
}
