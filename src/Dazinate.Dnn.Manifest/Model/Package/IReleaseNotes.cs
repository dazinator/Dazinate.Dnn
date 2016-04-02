using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Package
{
    public interface IReleaseNotes : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string SourceFile { get; set; }
        string Contents { get; set; }
        bool IsEmpty();
    }
}