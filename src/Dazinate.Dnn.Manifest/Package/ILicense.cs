using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package
{
    public interface ILicense : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string SourceFile { get; set; }
        string Contents { get; set; }

        bool IsEmpty();
    }
}