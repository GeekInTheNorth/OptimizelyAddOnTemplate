using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OptimizelyAddOn.Administration;

[Authorize(Policy = OptimizelyAddOnConstants.AuthorizationPolicy)]
public sealed class AdministrationController : Controller
{
    [HttpGet]
    [Route("~/optimizely-addon/administration/module.selector.example")]
    public IActionResult ModuleSelectorMenuExample()
    {
        var model = new AdministrationViewModel
        {
            Title = "Hello World!",
            Subtitle = "This is a module selector example."
        };

        return View("~/Views/OptimizelyAddOn/Administration/Index.cshtml", model);
    }

    [HttpGet]
    [Route("~/optimizely-addon/administration/simple.example")]
    public IActionResult SimpleMenuExample()
    {
        var model = new AdministrationViewModel
        {
            Title = "Hello World!",
            Subtitle = "This is a simple menu example."
        };

        return View("~/Views/OptimizelyAddOn/Administration/Index.cshtml", model);
    }

    [HttpGet]
    [Route("~/optimizely-addon/administration/nested.example")]
    public IActionResult NestedMenuExample()
    {
        var model = new AdministrationViewModel
        {
            Title = "Hello World!",
            Subtitle = "This is a nested menu example."
        };

        return View("~/Views/OptimizelyAddOn/Administration/Index.cshtml", model);
    }

    [HttpGet]
    [Route("~/optimizely-addon/administration/public/list")]
    [AllowAnonymous]
    public IActionResult PublicList()
    {
        var model = new List<string> { "foo", "boo" };

        return Json(model);
    }

    [HttpGet]
    [Route("~/optimizely-addon/administration/private/list")]
    public IActionResult PrivateList()
    {
        var model = new List<string> { "foo", "boo" };

        return Json(model);
    }
}