using Csla;
using Dazinate.Dnn.Manifest.Base;
using Dazinate.Dnn.Manifest.Writer;

namespace Dazinate.Dnn.Manifest.Package.Component
{
    public interface IComponentsList : IBusinessListBase<IComponent>, IVisitable<IManifestXmlWriterVisitor>
    {
       
    }
}