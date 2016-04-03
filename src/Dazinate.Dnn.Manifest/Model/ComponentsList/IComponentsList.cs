using Csla;
using Dazinate.Dnn.Manifest.Model.Component;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Model.ComponentsList
{
    public interface IComponentsList : IBusinessListBase<IComponent>, IVisitable<IManifestXmlWriterVisitor>
    {
       
    }
}