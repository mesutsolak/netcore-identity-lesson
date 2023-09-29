using Identity.Core.Helpers.Abstract;
using Identity.Data.Dtos.User;
using Identity.Data.Entities;
using Identity.Domain.Constants;
using Identity.Domain.Results;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Concrete
{
    public sealed class UserHelper : BaseHelper, IUserHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserHelper(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        }

        public async Task<BaseResult<UserDto>> GetUserByUserNameAsync(string userName)
        {
            var foundUser = await _userManager.FindByNameAsync(userName);

            if (foundUser is null)
                return new BaseResult<UserDto>(MessageText.UserNotFoundByUserName);

            return new BaseResult<UserDto>(foundUser.Adapt<UserDto>());
        }

        public async Task<BaseResult<UserDto>> UpdateUserAsync(UserDto userDto, string userName)
        {
            var foundUser = await _userManager.FindByNameAsync(userName);

            if (foundUser is null)
                return new BaseResult<UserDto>(MessageText.UserNotFoundByUserName);

            if (_userManager.Users.Any(x => x.PhoneNumber == userDto.PhoneNumber))
                return new BaseResult<UserDto>(MessageText.PhoneNumberInUse);

            foundUser.PhoneNumber = userDto.PhoneNumber;

            var identityResult = await _userManager.UpdateAsync(foundUser);

            if (!identityResult.Succeeded)
                return new BaseResult<UserDto>(identityResult.Errors.First().Description);

            return new BaseResult<UserDto>(foundUser.Adapt<UserDto>());
        }

        public async Task<BaseResult<UserDto>> UploadUserPictureAsync(string picturePath, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            user.Picture = picturePath;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? new BaseResult<UserDto>(user.Adapt<UserDto>()) : new BaseResult<UserDto>(result.Errors.First().Description);
        }
    }
}
