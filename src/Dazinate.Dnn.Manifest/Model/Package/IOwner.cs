using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.Package
{
    public interface IOwner : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Name { get; set; }
        string Organisation { get; set; }
        string Url { get; set; }
        string Email { get; set; }

    }
}