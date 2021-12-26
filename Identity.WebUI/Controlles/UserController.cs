using Identity.Entities.Models;
using Identity.WebUI.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Identity.WebUI.Controlles
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    UserName = applicationUserViewModel.UserName,
                    Email = applicationUserViewModel.Email
                };
                IdentityResult result = await _userManager.CreateAsync(appUser, applicationUserViewModel.Password);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            return View();
        }
    }
}
