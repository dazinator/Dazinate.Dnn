using Dazinate.Dnn.Manifest.Model.JavascriptFilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
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