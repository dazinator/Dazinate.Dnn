using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.ModulePermission;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IFileComponent : IComponent
    {
        IFilesList Files { get; }

        string BasePath { get; }

    }
}