using Csla;

namespace Dazinate.Dnn.Manifest
{
    public interface IReleaseNotes : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string SourceFile { get; set; }
        string Contents { get; set; }
        bool IsEmpty();
    }
}