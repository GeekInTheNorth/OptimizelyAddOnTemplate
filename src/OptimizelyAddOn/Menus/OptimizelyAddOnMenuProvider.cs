using EPiServer.Shell.Navigation;

namespace OptimizelyAddOn.Menus;

[MenuProvider]
public sealed class OptimizelyAddOnMenuProvider : IMenuProvider
{
    public IEnumerable<MenuItem> GetMenuItems()
    {
        // Module Selector Example
        // If you are using the module selector, then you will need nested menu items so as to avoid a load error on the ui.
        yield return CreateMenuItem("Example Module Selector Menu", "/global/example.menu.one", "/optimizely-addon/administration/module.selector.example/", SortIndex.Last + 10);
        yield return CreateMenuItem("Module Selector First Nested Item", "/global/example.menu.one/child.one", "/optimizely-addon/administration/module.selector.example/#child-one", SortIndex.Last + 11);
        yield return CreateMenuItem("Module Selector Second Nested Item", "/global/example.menu.one/child.two", "/optimizely-addon/administration/module.selector.example/#child-two", SortIndex.Last + 12);

        // Simple Menu Example
        yield return CreateMenuItem("Example Single Menu", "/global/cms/example.menu.two", "/optimizely-addon/administration/simple.example/", SortIndex.Last + 20);

        // Nested Menu Example
        yield return CreateMenuItem("Example Nested Menu", "/global/cms/example.menu.three", "/optimizely-addon/administration/nested.example/", SortIndex.Last + 30);
        yield return CreateMenuItem("First Nested Child", "/global/cms/example.menu.three/child.one", "/optimizely-addon/administration/nested.example/#child-one", SortIndex.Last + 31);
        yield return CreateMenuItem("Second Nested Child", "/global/cms/example.menu.three/child.two", "/optimizely-addon/administration/nested.example/#child-two", SortIndex.Last + 32);
    }

    private static UrlMenuItem CreateMenuItem(string name, string path, string url, int index)
    {
        return new UrlMenuItem(name, path, url)
        {
            IsAvailable = context => true,
            SortIndex = index,
            AuthorizationPolicy = OptimizelyAddOnConstants.AuthorizationPolicy
        };
    }
}
