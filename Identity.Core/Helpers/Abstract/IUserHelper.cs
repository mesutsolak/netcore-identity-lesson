using Identity.Data.Dtos.User;
using Identity.Domain.Results;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Abstract
{
    public interface IUserHelper
    {
        Task<BaseResult<UserDto>> UpdateUserAsync(UserDto userDto, string userName);
        Task<BaseResult<UserDto>> GetUserByUserNameAsync(string userName);
        Task<BaseResult<UserDto>> UploadUserPictureAsync(string picturePath, string userName);
    }
}
