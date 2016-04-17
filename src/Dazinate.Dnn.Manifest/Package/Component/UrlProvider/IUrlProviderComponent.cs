namespace Dazinate.Dnn.Manifest.Package.Component.UrlProvider
{
    public interface IUrlProviderComponent : IComponent
    {
     
        string Name { get; set; }
      
        string Type { get; set; }

        string SettingsControlSource { get; set; }

        bool RedirectAllUrls { get; set; }

        bool ReplaceAllUrls { get; set; }

        bool RewriteAllUrls { get; set; }

        string DesktopModule { get; set; }

    }
}