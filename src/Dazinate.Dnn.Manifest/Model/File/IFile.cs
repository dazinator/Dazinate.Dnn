using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.File
{
    public interface IFile : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Path { get; set; }
        string Name { get; set; }
    }
}