using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.JavascriptFilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IJavascriptFileComponent : IComponent
    {
        IJavascriptFilesList Files { get; }

        string LibraryFolderName { get; }

    }
}