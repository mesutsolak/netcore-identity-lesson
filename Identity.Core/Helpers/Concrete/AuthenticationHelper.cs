using Identity.Core.Helpers.Abstract;
using Identity.Data.Dtos.User;
using Identity.Data.Entities;
using Identity.Domain.Models.Authentication;
using Identity.Domain.Models.Token;
using Identity.Domain.Results;
using Identity.Domain.Results.Authentication;
using Identity.Domain.Results.Token;
using Identity.Domain.Settings;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Concrete
{
    public sealed class AuthenticationHelper : BaseHelper, IAuthenticationHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserHelper _userHelper;
        private readonly ITokenSetting _tokenSetting;

        public AuthenticationHelper(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _tokenHelper = serviceProvider.GetRequiredService<ITokenHelper>();
            _userHelper = serviceProvider.GetRequiredService<IUserHelper>();
            _tokenSetting = serviceProvider.GetRequiredService<ITokenSetting>();
        }

        public async Task<BaseResult<AccessTokenResult>> CreateAccessTokenByRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
        {
            var userClaim = await _userHelper.GetUserByRefreshTokenAsync(refreshTokenModel.RefreshToken);

            if (userClaim.UserDto is null)
                return new BaseResult<AccessTokenResult>("Böyle bir refresh token'a ait kullanıcı bulunamadı");

            var accessToken = _tokenHelper.CreateAccessToken(new CreateAccessTokenModel
            {
                Email = userClaim.UserDto.Email,
                Id = userClaim.UserDto.Id,
                UserName = userClaim.UserDto.UserName
            });

            var user = await _userManager.FindByEmailAsync(userClaim.UserDto.Email);

            if (user is null)
                return new BaseResult<AccessTokenResult>("Email bilgisine göre kullanıcı bulunamadı");

            var refreshTokenClaim = new Claim("refreshToken", accessToken.RefreshToken);
            var refreshTokenEndDateClaim = new Claim("refreshTokenEndDate", DateTime.Now.AddMinutes(_tokenSetting.AccessTokenExpiration).ToString());

            await _userManager.ReplaceClaimAsync(user, userClaim.Claims.First(), refreshTokenClaim);
            await _userManager.ReplaceClaimAsync(user, userClaim.Claims.Last(), refreshTokenEndDateClaim);

            return new BaseResult<AccessTokenResult>(accessToken);
        }

        public async Task<BaseResult<LoginResult>> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
                return new BaseResult<LoginResult>("Email yanlış");

            var isUser = await _userManager.CheckPasswordAsync(user, loginModel.Password);

            if (isUser)
                return new BaseResult<LoginResult>("Şifre yanlış");

            var accessToken = _tokenHelper.CreateAccessToken(new CreateAccessTokenModel
            {
                Email = user.Email,
                Id = user.Id,
                UserName = user.Email
            });

            var refreshTokenClaim = new Claim("refreshToken", accessToken.RefreshToken);
            var refreshTokenEndDateClaim = new Claim("refreshTokenEndDate", DateTime.Now.AddMinutes(_tokenSetting.AccessTokenExpiration).ToString());

            var refreshClaimList = _userManager.GetClaimsAsync(user).Result.Where(c => c.Type.Contains("refreshToken")).ToList();

            if (refreshClaimList.Any())
            {
                await _userManager.ReplaceClaimAsync(user, refreshClaimList[0], refreshTokenClaim);
                await _userManager.ReplaceClaimAsync(user, refreshClaimList[1], refreshTokenEndDateClaim);
            }
            else
                await _userManager.AddClaimsAsync(user, new[] { refreshTokenClaim, refreshTokenEndDateClaim });

            var loginResult = new LoginResult
            {
                AccessToken = accessToken
            };

            return new BaseResult<LoginResult>(loginResult);
        }

        public async Task<BaseResult<RegisterResult>> RegisterAsync(RegisterModel registerModel)
        {
            var user = new ApplicationUser
            {
                UserName = registerModel.UserName,
                Email = registerModel.Email
            };

            var result = await _userManager.CreateAsync(user, registerModel.Password);

            var registerResult = new RegisterResult
            {
                UserDto = user.Adapt<UserDto>()
            };

            return result.Succeeded
                ? new BaseResult<RegisterResult>(registerResult)
                : new BaseResult<RegisterResult>(result.Errors.First().Description);
        }

        public async Task<BaseResult<AccessTokenResult>> RevokeRefreshTokenAsync(RefreshTokenModel refreshTokenModel)
        {
            var identityResult = await _userHelper.RevokeRefreshTokenAsync(refreshTokenModel.RefreshToken);

            return identityResult
                ? new BaseResult<AccessTokenResult>(new AccessTokenResult())
                : new BaseResult<AccessTokenResult>("refreshToken veritabanında bulunamadı");
        }
    }
}
