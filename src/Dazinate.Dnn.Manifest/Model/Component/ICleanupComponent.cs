using Dazinate.Dnn.Manifest.Model.AssembliesList;
using Dazinate.Dnn.Manifest.Model.FilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface ICleanupComponent : IComponent
    {
        string Version { get; }
        string FileName { get; }
        IFilesList Files { get; }
    }
}