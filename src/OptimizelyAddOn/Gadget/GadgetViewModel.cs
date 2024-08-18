using EPiServer.Core;

namespace OptimizelyAddOn.Gadget;

public sealed class GadgetViewModel
{
    public string Title { get; set; } = "Example Gadget";

    public PageData? Page { get; set; } = null;

    public string? ContentId { get; set; }
    
    public string? Nonce { get; set; }
}
