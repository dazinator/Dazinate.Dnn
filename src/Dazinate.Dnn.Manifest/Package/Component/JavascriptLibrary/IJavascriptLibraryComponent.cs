namespace Dazinate.Dnn.Manifest.Package.Component.JavascriptLibrary
{
    public interface IJavascriptLibraryComponent : IComponent
    {
        string LibraryName { get; set; }
        string FileName { get; set; }
        string PreferredScriptLocation { get; set; }
        string CdnPath { get; set; }
        string ObjectName { get; set; }

    }
}