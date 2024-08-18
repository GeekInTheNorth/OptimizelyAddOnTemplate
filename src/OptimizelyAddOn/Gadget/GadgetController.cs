using EPiServer;
using EPiServer.Core;
using EPiServer.Framework.ClientResources;
using EPiServer.Shell.ViewComposition;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OptimizelyAddOn.Gadget;

[Authorize(Policy = OptimizelyAddOnConstants.AuthorizationPolicy)]
[IFrameComponent(
    Url = "/optimizely-addon/gadget/index/",
    Title = "Example Gadget",
    Description = "An example gadget for the CMS Editor Interface.",
    Categories = "content",
    PlugInAreas = "/episerver/cms/assets",
    MinHeight = 200,
    MaxHeight = 800,
    ReloadOnContextChange = true)]
public sealed class GadgetController : Controller
{
    private readonly IContentLoader _contentLoader;

    private readonly ICspNonceService _cspNonceService;

    public GadgetController(IContentLoader contentLoader, ICspNonceService cspNonceService)
    {
        _contentLoader = contentLoader;
        _cspNonceService = cspNonceService;
    }

    [HttpGet]
    [Route("~/optimizely-addon/gadget/index")]
    public IActionResult Index()
    {
        var pageData = GetPageData(Request);
        var model = new GadgetViewModel
        {
            Page = pageData,
            ContentId = Request.Query["Id"].ToString(),
            Nonce = _cspNonceService.GetNonce()
        };

        return View("~/Views/OptimizelyAddOn/Gadget/Index.cshtml", model);
    }

    private PageData? GetPageData(HttpRequest request)
    {
        var contentReferenceValue = request.Query["Id"].ToString() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(contentReferenceValue))
        {
            return null;
        }

        var contentReference = new ContentReference(contentReferenceValue);
        if (_contentLoader.TryGet<PageData>(contentReference, out var pageData))
        {
            return pageData;
        }

        return null;
    }
}