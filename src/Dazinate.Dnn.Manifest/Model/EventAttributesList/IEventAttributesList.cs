using Csla;
using Dazinate.Dnn.Manifest.Model.EventAttribute;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.EventAttributesList
{
    public interface IEventAttributesList : IBusinessListBase<IEventAttribute>, IVisitable<IManifestXmlWriterVisitor>
    {

    }
}