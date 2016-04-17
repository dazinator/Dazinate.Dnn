using Dazinate.Dnn.Manifest.Model.FilesList;

namespace Dazinate.Dnn.Manifest.Model.Component
{
    public interface IFileComponent : IComponent
    {
        IFilesList Files { get; }

        string BasePath { get; set; }

    }
}