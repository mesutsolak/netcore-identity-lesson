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
using System.Security.Claims;
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

        public async Task<UserByRefreshTokenResult> GetUserByRefreshTokenAsync(string refreshToken)
        {
            var claimRefreshToken = new Claim("refreshToken", refreshToken);

            var users = await _userManager.GetUsersForClaimAsync(claimRefreshToken);

            if (!users.Any())
                return null;

            var user = users.First();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var refreshTokenEndDate = userClaims.First(c => c.Type == "refreshTokenEndDate").Value;

            var userByRefreshTokenResult = new UserByRefreshTokenResult
            {
                Claims = userClaims,
                UserDto = user.Adapt<UserDto>()
            };

            return DateTime.Parse(refreshTokenEndDate) > DateTime.Now
                ? userByRefreshTokenResult
                : null;
        }

        public async Task<BaseResult<UserDto>> GetUserByUserNameAsync(string userName)
        {
            var foundUser = await _userManager.FindByNameAsync(userName);

            if (foundUser is null)
                return new BaseResult<UserDto>(ErrorMessage.UserNotFoundByUserName);

            return new BaseResult<UserDto>(foundUser.Adapt<UserDto>());
        }

        public async Task<bool> RevokeRefreshTokenAsync(string refreshToken)
        {
            var result = await GetUserByRefreshTokenAsync(refreshToken);

            if (result.UserDto is null)
                return false;

            var applicationUser = await _userManager.FindByIdAsync(result.UserDto.Id);

            var response = await _userManager.RemoveClaimsAsync(applicationUser, result.Claims);

            return response.Succeeded;
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

        public async Task<BaseResult<UserDto>> UploadUserPictureAsync(string picturePath, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);

            user.Picture = picturePath;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? new BaseResult<UserDto>(user.Adapt<UserDto>()) : new BaseResult<UserDto>(result.Errors.First().Description);
        }
    }
}
