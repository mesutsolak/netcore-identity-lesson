using Identity.Core.Helpers.Abstract;
using Identity.Data.Dtos.User;
using Identity.Data.Entities;
using Identity.Domain.Constants;
using Identity.Domain.Models.Authorize;
using Identity.Domain.Results;
using Identity.Domain.Results.Authorize;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.Core.Helpers.Concrete
{
    public sealed class AuthorizeHelper : BaseHelper, IAuthorizeHelper
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthorizeHelper(IServiceProvider serviceProvider)
        {
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
        }

        public async Task<BaseResult<LoginResult>> LoginAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByEmailAsync(loginModel.Email);

            if (user is null)
                return new BaseResult<LoginResult>(MessageText.UserNotFoundByEmail);

            //İlgili kullanıcıya dair önceden oluşturulmuş bir Cookie varsa siliyoruz.
            await _signInManager.SignOutAsync();

            var signInResult = await _signInManager.PasswordSignInAsync(user, loginModel.Password, loginModel.Persistent, loginModel.Lock);

            if (signInResult.Succeeded)
            {
                //Önceki hataları girişler neticesinde +1 arttırılmış tüm değerleri 0(sıfır)a çekiyoruz.
                var resetAccessFailedResult = await _userManager.ResetAccessFailedCountAsync(user);

                return resetAccessFailedResult.Succeeded
                    ? new BaseResult<LoginResult>()
                    : new BaseResult<LoginResult>(resetAccessFailedResult.Errors.Select(x => x.Description));
            }

            //Eğer ki başarısız bir account girişi söz konusu ise AccessFailedCount kolonundaki değer +1 arttırılacaktır. 
            var accessFailedResult = await _userManager.AccessFailedAsync(user);

            if (!accessFailedResult.Succeeded)
                return new BaseResult<LoginResult>(accessFailedResult.Errors.Select(x => x.Description));

            //Kullanıcının yapmış olduğu başarısız giriş deneme adedini alıyoruz.
            var failCount = await _userManager.GetAccessFailedCountAsync(user);

            if (failCount == AuthorizeConstant.FailLoginAttemptCount)
            {
                // Eğer ki başarısız giriş denemesi 3'ü bulduysa ilgili kullanıcının hesabını kilitliyoruz.
                var setLockoutEndDateResult = await _userManager.SetLockoutEndDateAsync(user, new DateTimeOffset(DateTime.Now.AddMinutes(1)));

                return setLockoutEndDateResult.Succeeded
                    ? new BaseResult<LoginResult>(MessageText.LockoutEndDateFaided)
                    : new BaseResult<LoginResult>(accessFailedResult.Errors.Select(x => x.Description));
            }

            return signInResult.IsLockedOut
                        ? new BaseResult<LoginResult>(MessageText.LockoutEndDateFaided)
                        : new BaseResult<LoginResult>(MessageText.SignInFailed);
        }

        public async Task<BaseResult<LogoutResult>> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return new BaseResult<LogoutResult>();
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
                : new BaseResult<RegisterResult>(result.Errors.Select(x => x.Description));
        }
    }
}
