using Csla;
using Dazinate.Dnn.Manifest.Base;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File
{
    public interface IFile : IBusinessBase, IVisitable<IManifestVisitor>
    {
        string Path { get; set; }
        string Name { get; set; }

        string SourceFileName { get; set; }
    }
}