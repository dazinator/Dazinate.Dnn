using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface ILicense : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string SourceFile { get; set; }
        string Contents { get; set; }

        bool IsEmpty();
    }
}