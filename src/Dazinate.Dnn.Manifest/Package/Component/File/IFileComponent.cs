using Dazinate.Dnn.Manifest.Package.Component.Shared.File;

namespace Dazinate.Dnn.Manifest.Package.Component.File
{
    public interface IFileComponent : IComponent
    {
        IFilesList Files { get; }

        string BasePath { get; set; }

    }
}