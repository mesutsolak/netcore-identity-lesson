using Identity.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Identity.WebUI.Controlles
{
    [Authorize]
    public class BaseController : Controller
    {
        public JsonResponseModel jsonResponseModel = new JsonResponseModel();
        protected List<string> ModelStateErrors => ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
    }
}
