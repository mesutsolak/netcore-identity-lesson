using Microsoft.AspNetCore.Mvc;

namespace Identity.WebUI.Controlles
{
    public sealed class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
