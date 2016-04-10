using Csla;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.EventAttribute
{
    public interface IEventAttribute : IBusinessBase, IVisitable<IManifestXmlWriterVisitor>
    {
        string Name { get; set; }
        string Value { get; set; }

    }
}