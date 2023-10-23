using Identity.Core.Helpers.Abstract;
using Identity.Domain.Constants;
using Identity.Domain.Models.Authorize;
using Identity.WebUI.ViewModels.Authorize;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Identity.WebUI.Controlles
{
    [AllowAnonymous]
    public sealed class AuthorizeController : BaseController
    {
        private readonly IAuthorizeHelper _authorizeHelper;

        public AuthorizeController(IServiceProvider serviceProvider)
        {
            _authorizeHelper = serviceProvider.GetRequiredService<IAuthorizeHelper>();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var registerResult = await _authorizeHelper.RegisterAsync(new RegisterModel
            {
                Email = registerViewModel.Email,
                UserName = registerViewModel.UserName,
                Password = registerViewModel.Password
            });

            if (registerResult.IsSuccess)
            {
                jsonResponseModel.Url = Url.Action("Index", "Home");
            }
            else
            {
                jsonResponseModel.Errors = registerResult.Messages;
            }

            return Json(jsonResponseModel);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            TempData["returnUrl"] = returnUrl;

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(ActionName.Index, ControllerName.Home);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                jsonResponseModel.Errors = ModelStateErrors;
                return Json(jsonResponseModel);
            }

            var loginModel = new LoginModel
            {
                Email = loginViewModel.Email,
                Password = loginViewModel.Password
            };

            var loginResult = await _authorizeHelper.LoginAsync(loginModel);

            if (loginResult.IsSuccess)
            {
                var returnUrl = TempData["returnUrl"]?.ToString() ?? string.Empty;
                jsonResponseModel.Url = string.IsNullOrWhiteSpace(returnUrl) ? Url.Action(ActionName.Index, ControllerName.Home) : returnUrl;
            }
            else
            {
                jsonResponseModel.Errors = loginResult.Messages;
            }

            return Json(jsonResponseModel);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var logoutResult = await _authorizeHelper.LogoutAsync();

            if (logoutResult.IsSuccess)
                jsonResponseModel.Url = Url.Action("Login", "Authorize");
            else
                jsonResponseModel.Errors = logoutResult.Messages;

            return Json(jsonResponseModel);
        }
    }
}
