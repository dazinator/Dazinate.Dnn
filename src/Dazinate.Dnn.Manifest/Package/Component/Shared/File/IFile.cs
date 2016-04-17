using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component.Shared.File
{
    public interface IFile : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Path { get; set; }
        string Name { get; set; }

        string SourceFileName { get; set; }
    }
}