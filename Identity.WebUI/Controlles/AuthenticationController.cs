using Identity.Core.Helpers.Abstract;
using Identity.Domain.Models.Authentication;
using Identity.WebUI.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Identity.WebUI.Controlles
{
    public sealed class AuthenticationController : Controller
    {
        private readonly IAuthenticationHelper _authenticationHelper;

        public AuthenticationController(IServiceProvider serviceProvider)
        {
            _authenticationHelper = serviceProvider.GetRequiredService<IAuthenticationHelper>();
        }

        [HttpGet]
        public IActionResult IsAuthentication()
        {
            return Ok(User.Identity.IsAuthenticated);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var result = await _authenticationHelper.RegisterAsync(new RegisterModel
            {
                Email = registerViewModel.Email,
                Password = registerViewModel.Password
            });

            return result.Success ? Ok(result.Data) : Ok(result.Message);
        }
    }
}
