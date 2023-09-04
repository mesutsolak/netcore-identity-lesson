using Identity.Data.Dtos.User;
using Identity.Domain.Results;
using Identity.Domain.Results.User;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Abstract
{
    public interface IUserHelper
    {
        Task<BaseResult<UserDto>> UpdateUserAsync(UserDto userDto, string userName);
        Task<BaseResult<UserDto>> GetUserByUserNameAsync(string userName);
        Task<bool> UploadUserPictureAsync(string picturePath, string userName);
        Task<UserByRefreshTokenResult> GetUserByRefreshTokenAsync(string refreshToken);
        Task<bool> RevokeRefreshTokenAsync(string refreshToken);
    }
}
