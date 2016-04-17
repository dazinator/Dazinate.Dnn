using Dazinate.Dnn.Manifest.Package.Component.Config;
using Dazinate.Dnn.Manifest.Package.Component.Shared.File;

namespace Dazinate.Dnn.Manifest.Package.Component.Cleanup
{
    public interface ICleanupComponent : IComponent
    {
        string Version { get; }
        string FileName { get; }
        IFilesList Files { get; }
    }

    public interface IConfigComponent : IComponent
    {
        string ConfigFile { get; }


        INodesList InstallNodes { get; }

        INodesList UninstallNodes { get; }
    }

  

}