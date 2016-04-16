namespace Dazinate.Dnn.Manifest.Model.Component
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