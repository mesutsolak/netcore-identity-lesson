using Identity.Core.Helpers.Abstract;
using Identity.Data.Dtos.User;
using Identity.Data.Entities;
using Identity.Domain.Constants;
using Identity.Domain.Results;
using Identity.Domain.Results.User;
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

        public Task<UserByRefreshTokenResult> GetUserByRefreshTokenAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseResult<UserDto>> GetUserByUserNameAsync(string userName)
        {
            var foundUser = await _userManager.FindByNameAsync(userName);

            if (foundUser is null)
                return new BaseResult<UserDto>(ErrorMessage.UserNotFoundByUserName);

            return new BaseResult<UserDto>(foundUser.Adapt<UserDto>());
        }

        public Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<BaseResult<UserDto>> UpdateUserAsync(UserDto userDto, string userName)
        {
            var foundUser = await _userManager.FindByNameAsync(userName);

            if (foundUser is null)
                return new BaseResult<UserDto>(ErrorMessage.UserNotFoundByUserName);

            if (_userManager.Users.Any(x => x.PhoneNumber == userDto.PhoneNumber))
                return new BaseResult<UserDto>(ErrorMessage.PhoneNumberInUse);

            foundUser.PhoneNumber = userDto.PhoneNumber;

            var identityResult = await _userManager.UpdateAsync(foundUser);

            if (!identityResult.Succeeded)
                return new BaseResult<UserDto>(identityResult.Errors.First().Description);

            return new BaseResult<UserDto>(foundUser.Adapt<UserDto>());
        }

        public Task<bool> UploadUserPictureAsync(string picturePath, string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
