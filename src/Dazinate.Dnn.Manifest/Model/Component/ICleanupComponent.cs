using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.FilesList;
using Dazinate.Dnn.Manifest.Model.NodesList;

namespace Dazinate.Dnn.Manifest.Model.Component
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